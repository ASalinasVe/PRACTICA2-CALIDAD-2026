using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;

namespace MicroServicioUsuario.Tests;

public class UserServiceUpdateTests
{
    [Fact]
    public async Task Update_WhenRepositorySucceeds_UpdatesDateAndReturnsSuccess()
    {
        var repository = new FakeRepository
        {
            UpdateResult = Result<int>.Success(1)
        };
        var service = new UserService(repository);
        var user = CreateUser();

        var before = DateTime.Now;
        var result = await service.Update(user);
        var after = DateTime.Now;

        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
        Assert.Empty(result.Errors);
        AssertUpdatedUser(user, repository, before, after);
    }

    [Fact]
    public async Task Update_WhenRepositoryFails_UpdatesDateAndReturnsFailure()
    {
        var repository = new FakeRepository
        {
            UpdateResult = Result<int>.Failure("Error de conexión")
        };
        var service = new UserService(repository);
        var user = CreateUser();

        var before = DateTime.Now;
        var result = await service.Update(user);
        var after = DateTime.Now;

        Assert.True(result.IsFailure);
        Assert.Equal("Error de conexión", Assert.Single(result.Errors));
        AssertUpdatedUser(user, repository, before, after);
    }

    private static void AssertUpdatedUser(
        User user, FakeRepository repository, DateTime before, DateTime after)
    {
        Assert.InRange(user.LastUpdate, before, after);
        Assert.Equal(1, repository.UpdateCallCount);
        Assert.Same(user, repository.UpdatedUser);

        Assert.Equal(10, user.Id);
        Assert.Equal("ale.salinas", user.Username);
        Assert.Equal("hash-de-prueba-123", user.PasswordHash);
        Assert.Equal("Alejandra", user.FirstName);
        Assert.Equal("Salinas", user.LastName);
        Assert.Equal("ale.salinas@example.com", user.Email);
        Assert.Equal("Admin", user.Role);
        Assert.Equal(1, user.CreatedBy);
        Assert.Equal(new DateTime(2025, 1, 1, 10, 0, 0), user.CreatedDate);
        Assert.False(user.Status);
        Assert.Equal(0, user.FirstLogin);
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
        public Result<int> UpdateResult { get; init; } = Result<int>.Success(1);
        public int UpdateCallCount { get; private set; }
        public User? UpdatedUser { get; private set; }

        public Task<Result<int>> Update(User model)
        {
            UpdateCallCount++;
            UpdatedUser = model;
            return Task.FromResult(UpdateResult);
        }

        public Task<Result<int>> Delete(int id) =>
            throw new NotSupportedException();

        public Task<Result<IEnumerable<User>>> GetAll() =>
            throw new NotSupportedException();

        public Task<Result<int>> Insert(User model) =>
            throw new NotSupportedException();

        public Task<Result<IEnumerable<User>>> Search(string property) =>
            throw new NotSupportedException();

        public Task<Result<User?>> GetById(int id) =>
            throw new NotSupportedException();

        public Task<Result<User?>> GetByUsername(string username) =>
            throw new NotSupportedException();
    }
}