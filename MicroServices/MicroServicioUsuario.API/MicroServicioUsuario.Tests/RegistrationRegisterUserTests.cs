using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;
using MicroServicoUser.Inf.Repository;

namespace MicroServicioUsuario.Tests;

public class RegistrationRegisterUserTests
{
    [Fact]
    public async Task RegisterUser_WhenFirstNameIsMissing_ReturnsRequiredDataFailure()
    {
        var repository = new FakeUserRepository();
        var registration = new Registration(repository);

        var result = await registration.RegisterUser("", "Lopez", "ana@example.com", "Admin", 7);

        AssertRequiredDataFailure(result, repository);
    }

    [Fact]
    public async Task RegisterUser_WhenLastNameIsMissing_ReturnsRequiredDataFailure()
    {
        var repository = new FakeUserRepository();
        var registration = new Registration(repository);

        var result = await registration.RegisterUser("Ana", "", "ana@example.com", "Admin", 7);

        AssertRequiredDataFailure(result, repository);
    }

    [Fact]
    public async Task RegisterUser_WhenEmailIsMissing_ReturnsRequiredDataFailure()
    {
        var repository = new FakeUserRepository();
        var registration = new Registration(repository);

        var result = await registration.RegisterUser("Ana", "Lopez", "", "Admin", 7);

        AssertRequiredDataFailure(result, repository);
    }

    [Fact]
    public async Task RegisterUser_WhenFirstNameHasNoAsciiLetters_UsesEmailUsername()
    {
        var repository = new FakeUserRepository();
        var registration = new Registration(repository);

        var result = await registration.RegisterUser("123", "Lopez", "ana@example.com", "Admin", 7);

        AssertSuccessfulRegistration(result, "ana", "1", repository);
        Assert.Equal("ana", Assert.Single(repository.UsernamesChecked));
    }

    [Fact]
    public async Task RegisterUser_WhenLastNameHasNoAsciiLetters_UsesEmailUsername()
    {
        var repository = new FakeUserRepository();
        var registration = new Registration(repository);

        var result = await registration.RegisterUser("Ana", "123", "maria@example.com", "Admin", 7);

        AssertSuccessfulRegistration(result, "maria", "1", repository);
        Assert.Equal("maria", Assert.Single(repository.UsernamesChecked));
    }

    [Fact]
    public async Task RegisterUser_WhenUsernameIsAvailableAndRoleIsKnown_InsertsUser()
    {
        var repository = new FakeUserRepository();
        var registration = new Registration(repository);

        var result = await registration.RegisterUser("Ana", "Lopez", "ana@example.com", "Admin", 7);

        AssertSuccessfulRegistration(result, "a_lopez", "1", repository);
        Assert.Equal("a_lopez", Assert.Single(repository.UsernamesChecked));
    }

    [Fact]
    public async Task RegisterUser_WhenEmailLocalPartHasNoAsciiLetters_UsesDefaultUsername()
    {
        var repository = new FakeUserRepository();
        var registration = new Registration(repository);

        var result = await registration.RegisterUser("123", "Lopez", "123@example.com", "Admin", 7);

        AssertSuccessfulRegistration(result, "user", "1", repository);
        Assert.Equal("user", Assert.Single(repository.UsernamesChecked));
    }

    [Fact]
    public async Task RegisterUser_WhenFirstUsernameIsTaken_UsesNextSuffix()
    {
        var repository = new FakeUserRepository
        {
            ExistingUsernames = new HashSet<string>(StringComparer.Ordinal) { "a_lopez" }
        };
        var registration = new Registration(repository);

        var result = await registration.RegisterUser("Ana", "Lopez", "ana@example.com", "Admin", 7);

        AssertSuccessfulRegistration(result, "a_lopez2", "1", repository);
        Assert.Collection(repository.UsernamesChecked,
            username => Assert.Equal("a_lopez", username),
            username => Assert.Equal("a_lopez2", username));
    }

    [Fact]
    public async Task RegisterUser_WhenRoleIsUnknown_UsesZeroRoleCode()
    {
        var repository = new FakeUserRepository();
        var registration = new Registration(repository);

        var result = await registration.RegisterUser("Ana", "Lopez", "ana@example.com", "rolDesconocido", 7);

        AssertSuccessfulRegistration(result, "a_lopez", "0", repository);
    }

    [Fact]
    public async Task RegisterUser_WhenInsertFails_ReturnsInsertError()
    {
        var repository = new FakeUserRepository
        {
            InsertResult = Result<int>.Failure("Error al crear usuario")
        };
        var registration = new Registration(repository);

        var result = await registration.RegisterUser("Ana", "Lopez", "ana@example.com", "Admin", 7);

        Assert.False(result.ok);
        Assert.Null(result.generatedUsername);
        Assert.Null(result.generatedPassword);
        Assert.Equal("Error al crear usuario", result.error);
        Assert.Equal(1, repository.InsertCallCount);
        Assert.Equal("a_lopez", Assert.IsType<User>(repository.InsertedUser).Username);
    }

    private static void AssertRequiredDataFailure(
        (bool ok, string? generatedUsername, string? generatedPassword, string? error) result,
        FakeUserRepository repository)
    {
        Assert.False(result.ok);
        Assert.Null(result.generatedUsername);
        Assert.Null(result.generatedPassword);
        Assert.Equal("Faltan datos obligatorios.", result.error);
        Assert.Empty(repository.UsernamesChecked);
        Assert.Equal(0, repository.InsertCallCount);
    }

    private static void AssertSuccessfulRegistration(
        (bool ok, string? generatedUsername, string? generatedPassword, string? error) result,
        string expectedUsername,
        string expectedRoleCode,
        FakeUserRepository repository)
    {
        Assert.True(result.ok);
        Assert.Equal(expectedUsername, result.generatedUsername);
        Assert.Null(result.error);
        Assert.NotNull(result.generatedPassword);
        Assert.Equal(10, result.generatedPassword.Length);
        Assert.Equal(1, repository.InsertCallCount);

        var insertedUser = Assert.IsType<User>(repository.InsertedUser);
        Assert.Equal(expectedUsername, insertedUser.Username);
        Assert.Equal(expectedRoleCode, insertedUser.Role);
        Assert.Equal(7, insertedUser.CreatedBy);
        Assert.Equal(RegistrationHelpers.Md5Hex(result.generatedPassword), insertedUser.PasswordHash);
    }

    private sealed class FakeUserRepository : IRepository
    {
        public HashSet<string> ExistingUsernames { get; init; } = new(StringComparer.Ordinal);
        public Result<int> InsertResult { get; init; } = Result<int>.Success(1);
        public List<string> UsernamesChecked { get; } = new();
        public int InsertCallCount { get; private set; }
        public User? InsertedUser { get; private set; }

        public Task<Result<User?>> GetByUsername(string username)
        {
            UsernamesChecked.Add(username);
            User? existingUser = ExistingUsernames.Contains(username) ? CreateExistingUser(username) : null;
            return Task.FromResult(Result<User?>.Success(existingUser));
        }

        public Task<Result<int>> Insert(User model)
        {
            InsertCallCount++;
            InsertedUser = model;
            return Task.FromResult(InsertResult);
        }

        public Task<Result<int>> Delete(int id) => throw new NotSupportedException();

        public Task<Result<IEnumerable<User>>> GetAll() => throw new NotSupportedException();

        public Task<Result<int>> Update(User model) => throw new NotSupportedException();

        public Task<Result<IEnumerable<User>>> Search(string property) => throw new NotSupportedException();

        public Task<Result<User?>> GetById(int id) => throw new NotSupportedException();

        private static User CreateExistingUser(string username) => new()
        {
            Username = username,
            PasswordHash = "existing-hash",
            FirstName = "Existing",
            LastName = "User",
            Email = "existing@example.com",
            Role = "1"
        };
    }
}
