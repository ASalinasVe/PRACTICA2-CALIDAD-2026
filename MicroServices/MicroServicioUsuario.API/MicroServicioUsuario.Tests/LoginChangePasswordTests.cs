using MicroServicoUser.Inf.Repository;
using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;
using MicroServicioUser.Dom.Rules;

namespace MicroServicioUsuario.Tests;

// Cases map to the control-flow graph of Login.ChangePassword(int, string, string)
// (docs/testing/usuarios/control-flow-graphs.md, lines 14-47).
// The graph has 6 predicates (the || in line 18 counts as two operands) and V(G) = 7.
// CP1-CP7 cover the seven independent paths, one per exit of the method.
public class LoginChangePasswordTests
{
    private const int UserId = 10;
    private const string CurrentPassword = "Actual123!";
    private const string NewPassword = "Nueva456!";
    private const string NotFoundMessage = "Usuario no encontrado.";
    private const string WrongCurrentMessage = "La contraseña actual no es correcta.";
    private const string SamePasswordMessage = "La nueva contraseña debe ser diferente a la actual.";

    // P1: GetById fails -> "Usuario no encontrado." and no update.
    [Fact]
    public async Task ChangePassword_WhenLookupFails_ReturnsNotFoundWithoutUpdating()
    {
        var repository = new FakeRepository
        {
            GetByIdResult = Result<User?>.Failure("Error de conexión")
        };
        var login = new Login(repository);

        var result = await login.ChangePassword(UserId, CurrentPassword, NewPassword);

        Assert.False(result.ok);
        Assert.Equal(NotFoundMessage, result.error);
        Assert.Equal(0, repository.UpdateCallCount);
    }

    // P2: GetById succeeds with no user (Value is null) -> the same "not found" message and no update.
    [Fact]
    public async Task ChangePassword_WhenLookupSucceedsWithoutUser_ReturnsNotFoundWithoutUpdating()
    {
        var repository = new FakeRepository
        {
            GetByIdResult = Result<User?>.Success(null)
        };
        var login = new Login(repository);

        var result = await login.ChangePassword(UserId, CurrentPassword, NewPassword);

        Assert.False(result.ok);
        Assert.Equal(NotFoundMessage, result.error);
        Assert.Equal(0, repository.UpdateCallCount);
    }

    // P3: the current password does not match the stored hash -> rejected before validating the new one.
    [Fact]
    public async Task ChangePassword_WhenCurrentPasswordIsWrong_ReturnsErrorWithoutUpdating()
    {
        var repository = new FakeRepository
        {
            GetByIdResult = Result<User?>.Success(CreateUser(CurrentPassword))
        };
        var login = new Login(repository);

        var result = await login.ChangePassword(UserId, "Incorrecta1!", NewPassword);

        Assert.False(result.ok);
        Assert.Equal(WrongCurrentMessage, result.error);
        Assert.Equal(0, repository.UpdateCallCount);
    }

    // P4: the new password breaks the complexity rules -> the validator's message is returned, no update.
    [Fact]
    public async Task ChangePassword_WhenNewPasswordIsWeak_ReturnsValidationErrorWithoutUpdating()
    {
        var repository = new FakeRepository
        {
            GetByIdResult = Result<User?>.Success(CreateUser(CurrentPassword))
        };
        var login = new Login(repository);

        var result = await login.ChangePassword(UserId, CurrentPassword, "abc");

        Assert.False(result.ok);
        Assert.Equal(PasswordValidator.Validate("abc").error, result.error);
        Assert.Equal(0, repository.UpdateCallCount);
    }

    // P5: the new password equals the current one -> rejected, no update.
    [Fact]
    public async Task ChangePassword_WhenNewPasswordEqualsCurrent_ReturnsErrorWithoutUpdating()
    {
        var repository = new FakeRepository
        {
            GetByIdResult = Result<User?>.Success(CreateUser(CurrentPassword))
        };
        var login = new Login(repository);

        var result = await login.ChangePassword(UserId, CurrentPassword, CurrentPassword);

        Assert.False(result.ok);
        Assert.Equal(SamePasswordMessage, result.error);
        Assert.Equal(0, repository.UpdateCallCount);
    }

    // P6: persistence fails -> the update errors are joined with "; ".
    [Fact]
    public async Task ChangePassword_WhenUpdateFails_ReturnsJoinedErrors()
    {
        var repository = new FakeRepository
        {
            GetByIdResult = Result<User?>.Success(CreateUser(CurrentPassword)),
            UpdateResult = Result<int>.Failure("Error A", "Error B")
        };
        var login = new Login(repository);

        var result = await login.ChangePassword(UserId, CurrentPassword, NewPassword);

        Assert.False(result.ok);
        Assert.Equal("Error A; Error B", result.error);
        Assert.Equal(1, repository.UpdateCallCount);
    }

    // P7: all checks pass -> the new hash and LastUpdate are persisted; FirstLogin is not touched.
    [Fact]
    public async Task ChangePassword_WhenAllChecksPass_PersistsNewHashAndReturnsSuccess()
    {
        var user = CreateUser(CurrentPassword);
        user.FirstLogin = 1;
        var repository = new FakeRepository
        {
            GetByIdResult = Result<User?>.Success(user),
            UpdateResult = Result<int>.Success(UserId)
        };
        var login = new Login(repository);

        var result = await login.ChangePassword(UserId, CurrentPassword, NewPassword);

        Assert.True(result.ok);
        Assert.Null(result.error);
        Assert.Equal(1, repository.UpdateCallCount);
        Assert.Same(user, repository.UpdatedUser);
        Assert.Equal(RegistrationHelpers.Md5Hex(NewPassword), user.PasswordHash);
        Assert.Equal(1, user.FirstLogin);
        Assert.True(user.LastUpdate > DateTime.MinValue);
    }

    private static User CreateUser(string password) => new()
    {
        Id = UserId,
        Username = "ale.salinas",
        PasswordHash = RegistrationHelpers.Md5Hex(password),
        FirstName = "Alejandra",
        LastName = "Salinas",
        Email = "ale.salinas@example.com",
        Role = "Admin",
        CreatedBy = 1,
        Status = true,
        FirstLogin = 0
    };

    private sealed class FakeRepository : IRepository
    {
        public Result<User?> GetByIdResult { get; init; } = Result<User?>.Success(null);
        public Result<int> UpdateResult { get; init; } = Result<int>.Success(UserId);

        public int UpdateCallCount { get; private set; }
        public User? UpdatedUser { get; private set; }

        public Task<Result<User?>> GetById(int id) => Task.FromResult(GetByIdResult);

        public Task<Result<int>> Update(User model)
        {
            UpdateCallCount++;
            UpdatedUser = model;
            return Task.FromResult(UpdateResult);
        }

        public Task<Result<int>> Delete(int id) => throw new NotSupportedException();
        public Task<Result<IEnumerable<User>>> GetAll() => throw new NotSupportedException();
        public Task<Result<int>> Insert(User model) => throw new NotSupportedException();
        public Task<Result<IEnumerable<User>>> Search(string property) => throw new NotSupportedException();
        public Task<Result<User?>> GetByUsername(string username) => throw new NotSupportedException();
    }
}
