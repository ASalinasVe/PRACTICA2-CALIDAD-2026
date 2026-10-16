using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;
using MicroServicoUser.Inf.Repository;

namespace MicroServicioUsuario.Tests;

public class LoginValidateLoginTests
{
    private const string Username = "ana";
    private const string CorrectPassword = "abc";

    [Fact]
    public async Task ValidateLogin_WhenLookupFails_ReturnsLookupError()
    {
        var repository = new StubUserRepository(Result<User?>.Failure("Usuario no encontrado"));
        var login = new Login(repository);

        var result = await login.ValidateLogin(Username, CorrectPassword);

        AssertFailure(result, "Usuario no encontrado");
        Assert.Equal(Username, repository.RequestedUsername);
        Assert.Equal(1, repository.LookupCallCount);
    }

    [Fact]
    public async Task ValidateLogin_WhenUserIsInactive_ReturnsInactiveError()
    {
        var lookup = Result<User?>.Success(CreateUser(status: false));
        // TC2 supplies the error because ValidateLogin reads Errors.First() on this path.
        lookup.Errors.Add("Cuenta inactiva");
        var repository = new StubUserRepository(lookup);
        var login = new Login(repository);

        var result = await login.ValidateLogin(Username, CorrectPassword);

        AssertFailure(result, "Cuenta inactiva");
        Assert.Equal(1, repository.LookupCallCount);
    }

    [Fact]
    public async Task ValidateLogin_WhenPasswordIsIncorrect_ReturnsPasswordError()
    {
        var repository = new StubUserRepository(Result<User?>.Success(CreateUser()));
        var login = new Login(repository);

        var result = await login.ValidateLogin(Username, "wrong");

        AssertFailure(result, "Contrase\u00F1a incorrecta.");
        Assert.Equal(1, repository.LookupCallCount);
    }

    [Fact]
    public async Task ValidateLogin_WhenRoleIsText_ReturnsOriginalRoleAndFirstLogin()
    {
        var repository = new StubUserRepository(Result<User?>.Success(CreateUser(role: "admin", firstLogin: 0)));
        var login = new Login(repository);

        var result = await login.ValidateLogin(Username, CorrectPassword);

        AssertSuccess(result, "admin", isFirstLogin: true);
    }

    [Fact]
    public async Task ValidateLogin_WhenRoleCodeIsKnown_ReturnsMappedRole()
    {
        var repository = new StubUserRepository(Result<User?>.Success(CreateUser(role: "1", firstLogin: 1)));
        var login = new Login(repository);

        var result = await login.ValidateLogin(Username, CorrectPassword);

        AssertSuccess(result, AuthenticationRoles.CodeToRole[1], isFirstLogin: false);
    }

    [Fact]
    public async Task ValidateLogin_WhenRoleCodeIsUnknown_KeepsOriginalCode()
    {
        var repository = new StubUserRepository(Result<User?>.Success(CreateUser(role: "999", firstLogin: 0)));
        var login = new Login(repository);

        var result = await login.ValidateLogin(Username, CorrectPassword);

        AssertSuccess(result, "999", isFirstLogin: true);
    }

    private static User CreateUser(bool status = true, string role = "admin", int firstLogin = 0) => new()
    {
        Id = 42,
        Username = Username,
        PasswordHash = "900150983cd24fb0d6963f7d28e17f72", // MD5 of "abc"
        FirstName = "Ana",
        LastName = "Perez",
        Email = "ana@example.com",
        Role = role,
        Status = status,
        FirstLogin = firstLogin
    };

    private static void AssertFailure(
        (bool ok, int? userId, string? role, string? error, bool isFirstLogin) result,
        string expectedError)
    {
        Assert.False(result.ok);
        Assert.Null(result.userId);
        Assert.Null(result.role);
        Assert.Equal(expectedError, result.error);
        Assert.False(result.isFirstLogin);
    }

    private static void AssertSuccess(
        (bool ok, int? userId, string? role, string? error, bool isFirstLogin) result,
        string expectedRole,
        bool isFirstLogin)
    {
        Assert.True(result.ok);
        Assert.Equal(42, result.userId);
        Assert.Equal(expectedRole, result.role);
        Assert.Null(result.error);
        Assert.Equal(isFirstLogin, result.isFirstLogin);
    }

    private sealed class StubUserRepository : IRepository
    {
        private readonly Result<User?> _lookupResult;

        public StubUserRepository(Result<User?> lookupResult) => _lookupResult = lookupResult;

        public string? RequestedUsername { get; private set; }
        public int LookupCallCount { get; private set; }

        public Task<Result<User?>> GetByUsername(string username)
        {
            RequestedUsername = username;
            LookupCallCount++;
            return Task.FromResult(_lookupResult);
        }

        public Task<Result<User?>> GetById(int id) => throw new NotSupportedException();
        public Task<Result<IEnumerable<User>>> GetAll() => throw new NotSupportedException();
        public Task<Result<int>> Insert(User model) => throw new NotSupportedException();
        public Task<Result<int>> Update(User model) => throw new NotSupportedException();
        public Task<Result<int>> Delete(int id) => throw new NotSupportedException();
        public Task<Result<IEnumerable<User>>> Search(string property) => throw new NotSupportedException();
    }
}
