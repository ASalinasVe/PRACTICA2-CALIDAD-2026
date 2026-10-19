using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;

namespace MicroServicioUsuario.Tests;

public class UserServiceInsertTests
{
    [Fact]
    public async Task Insert_WhenRepositorySucceeds_UpdatesFieldsAndReturnsSuccess()
    {
        var repository = new FakeRepository
        {
            InsertResult = Result<int>.Success(1)
        };
        var service = new UserService(repository);
        var user = CreateUser();

        var before = DateTime.Now;
        var result = await service.Insert(user);
        var after = DateTime.Now;

        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
        Assert.Empty(result.Errors);
        AssertUserFieldsWereUpdated(user, before, after);
        Assert.Equal(1, repository.InsertCallCount);
        Assert.Same(user, repository.InsertedUser);
    }

    [Fact]
    public async Task Insert_WhenRepositoryFails_UpdatesFieldsAndReturnsFailure()
    {
        var repository = new FakeRepository
        {
            InsertResult = Result<int>.Failure("Error de conexión")
        };
        var service = new UserService(repository);
        var user = CreateUser();

        var before = DateTime.Now;
        var result = await service.Insert(user);
        var after = DateTime.Now;

        Assert.True(result.IsFailure);
        Assert.Equal("Error de conexión", Assert.Single(result.Errors));
        AssertUserFieldsWereUpdated(user, before, after);
        Assert.Equal(1, repository.InsertCallCount);
        Assert.Same(user, repository.InsertedUser);
    }

    private static void AssertUserFieldsWereUpdated(
        User user, DateTime before, DateTime after)
    {
        Assert.True(user.CreatedDate >= before);
        Assert.True(user.CreatedDate <= after);
        Assert.True(user.LastUpdate >= before);
        Assert.True(user.LastUpdate <= after);
        Assert.True(user.Status);
        Assert.Equal(1, user.FirstLogin);

        Assert.Equal("ale.salinas", user.Username);
        Assert.Equal("Alejandra", user.FirstName);
        Assert.Equal("Salinas", user.LastName);
        Assert.Equal("ale.salinas@example.com", user.Email);
        Assert.Equal("Admin", user.Role);
    }

    private static User CreateUser() => new()
    {
        Id = 10,
        Username = "ale.salinas",
        PasswordHash = "hash-de-prueba-123",
        FirstName = "Alejandra",
        LastName = "Salinas",
        Email = "ale.salinas@example.com",
        Role = "Admin",
        CreatedBy = 1,
        CreatedDate = new DateTime(2025, 1, 1, 10, 0, 0),
        LastUpdate = new DateTime(2025, 1, 2, 11, 0, 0),
        Status = false,
        FirstLogin = 0
    };

    private sealed class FakeRepository : IRepository
    {
        public Result<int> InsertResult { get; init; } = Result<int>.Success(1);
        public int InsertCallCount { get; private set; }
        public User? InsertedUser { get; private set; }

        public Task<Result<int>> Insert(User model)
        {
            InsertCallCount++;
            InsertedUser = model;
            return Task.FromResult(InsertResult);
        }

        public Task<Result<int>> Delete(int id) =>
            throw new NotSupportedException();

        public Task<Result<IEnumerable<User>>> GetAll() =>
            throw new NotSupportedException();

        public Task<Result<int>> Update(User model) =>
            throw new NotSupportedException();

        public Task<Result<IEnumerable<User>>> Search(string property) =>
            throw new NotSupportedException();

        public Task<Result<User?>> GetById(int id) =>
            throw new NotSupportedException();

        public Task<Result<User?>> GetByUsername(string username) =>
            throw new NotSupportedException();
    }
}