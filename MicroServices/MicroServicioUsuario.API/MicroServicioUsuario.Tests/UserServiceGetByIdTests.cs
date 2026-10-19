using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;

namespace MicroServicioUsuario.Tests;

public class UserServiceGetByIdTests
{
    [Fact]
    public async Task GetById_WhenUserIsFound_ReturnsSameUser()
    {
        var user = CreateUser();
        var repository = new FakeRepository
        {
            GetByIdResult = Result<User?>.Success(user)
        };
        var service = new UserService(repository);

        var result = await service.GetById(10);

        Assert.True(result.IsSuccess);
        Assert.Same(user, result.Value);
        Assert.Empty(result.Errors);
        AssertRepositoryCalledOnce(repository, 10);
    }

    [Fact]
    public async Task GetById_WhenNoUserIsFound_ReturnsSuccessfulNull()
    {
        var repository = new FakeRepository
        {
            GetByIdResult = Result<User?>.Success(null)
        };
        var service = new UserService(repository);

        var result = await service.GetById(10);

        Assert.True(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Empty(result.Errors);
        AssertRepositoryCalledOnce(repository, 10);
    }

    [Fact]
    public async Task GetById_WhenRepositoryFails_ReturnsFailure()
    {
        var repository = new FakeRepository
        {
            GetByIdResult = Result<User?>.Failure("Error de conexión")
        };
        var service = new UserService(repository);

        var result = await service.GetById(10);

        Assert.True(result.IsFailure);
        Assert.Equal("Error de conexión", Assert.Single(result.Errors));
        AssertRepositoryCalledOnce(repository, 10);
    }

    private static void AssertRepositoryCalledOnce(FakeRepository repository, int id)
    {
        Assert.Equal(1, repository.GetByIdCallCount);
        Assert.Equal(id, repository.RequestedId);
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
        Status = true,
        FirstLogin = 0
    };

    private sealed class FakeRepository : IRepository
    {
        public Result<User?> GetByIdResult { get; init; } =
            Result<User?>.Success(null);

        public int GetByIdCallCount { get; private set; }
        public int? RequestedId { get; private set; }

        public Task<Result<User?>> GetById(int id)
        {
            GetByIdCallCount++;
            RequestedId = id;
            return Task.FromResult(GetByIdResult);
        }

        public Task<Result<int>> Delete(int id) =>
            throw new NotSupportedException();

        public Task<Result<IEnumerable<User>>> GetAll() =>
            throw new NotSupportedException();

        public Task<Result<int>> Insert(User model) =>
            throw new NotSupportedException();

        public Task<Result<int>> Update(User model) =>
            throw new NotSupportedException();

        public Task<Result<IEnumerable<User>>> Search(string property) =>
            throw new NotSupportedException();

        public Task<Result<User?>> GetByUsername(string username) =>
            throw new NotSupportedException();
    }
}