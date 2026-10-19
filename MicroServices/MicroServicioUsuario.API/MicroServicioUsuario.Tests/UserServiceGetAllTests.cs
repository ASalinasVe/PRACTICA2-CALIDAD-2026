using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;

namespace MicroServicioUsuario.Tests;

// Cases map to the control-flow graph of UserService.GetAll()
// (docs/testing/usuarios/control-flow-graphs.md, lines 48-51).
// The method has no decisions: V(G) = 1, a single path (CP1).
// CA2 is an additional data-level case on the same path.
public class UserServiceGetAllTests
{
    // CP1: the repository succeeds -> the service returns the same result, untouched, after one call.
    [Fact]
    public async Task GetAll_WhenRepositorySucceeds_ReturnsSameResult()
    {
        var users = new List<User> { CreateUser(1), CreateUser(2) };
        var repository = new FakeRepository
        {
            GetAllResult = Result<IEnumerable<User>>.Success(users)
        };
        var service = new UserService(repository);

        var result = await service.GetAll();

        Assert.True(result.IsSuccess);
        Assert.Same(users, result.Value);
        Assert.Empty(result.Errors);
        Assert.Equal(1, repository.GetAllCallCount);
    }

    // CA2: same path as CP1 with a failing repository -> the failure and its errors pass through unchanged.
    [Fact]
    public async Task GetAll_WhenRepositoryFails_ReturnsSameFailureAndErrors()
    {
        var repository = new FakeRepository
        {
            GetAllResult = Result<IEnumerable<User>>.Failure("Error A", "Error B")
        };
        var service = new UserService(repository);

        var result = await service.GetAll();

        Assert.True(result.IsFailure);
        Assert.Null(result.Value);
        Assert.Equal(new[] { "Error A", "Error B" }, result.Errors);
        Assert.Equal(1, repository.GetAllCallCount);
    }

    private static User CreateUser(int id) => new()
    {
        Id = id,
        Username = $"user{id}",
        PasswordHash = "hash-de-prueba-123",
        FirstName = "Nombre",
        LastName = "Apellido",
        Email = $"user{id}@example.com",
        Role = "Admin"
    };

    private sealed class FakeRepository : IRepository
    {
        public Result<IEnumerable<User>> GetAllResult { get; init; } =
            Result<IEnumerable<User>>.Success(new List<User>());

        public int GetAllCallCount { get; private set; }

        public Task<Result<IEnumerable<User>>> GetAll()
        {
            GetAllCallCount++;
            return Task.FromResult(GetAllResult);
        }

        public Task<Result<int>> Delete(int id) =>
            throw new NotSupportedException();

        public Task<Result<int>> Insert(User model) =>
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
