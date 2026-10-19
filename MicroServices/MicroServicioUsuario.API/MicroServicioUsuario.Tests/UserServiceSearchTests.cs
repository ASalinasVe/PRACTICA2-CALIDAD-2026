using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;

namespace MicroServicioUsuario.Tests;

// Cases map to the control-flow graph of UserService.Search(string)
// (docs/testing/usuarios/control-flow-graphs.md, lines 53-56).
// The method has no decisions: V(G) = 1, a single path (CP1).
// CA2 is an additional data-level case on the same path.
public class UserServiceSearchTests
{
    // CP1: the repository succeeds -> the service returns the same result, and the term is forwarded once.
    [Fact]
    public async Task Search_WhenRepositorySucceeds_ReturnsSameResultAndForwardsTerm()
    {
        var users = new List<User> { CreateUser(1) };
        var repository = new FakeRepository
        {
            SearchResult = Result<IEnumerable<User>>.Success(users)
        };
        var service = new UserService(repository);

        var result = await service.Search("ana");

        Assert.True(result.IsSuccess);
        Assert.Same(users, result.Value);
        Assert.Empty(result.Errors);
        Assert.Equal(1, repository.SearchCallCount);
        Assert.Equal("ana", repository.RequestedTerm);
    }

    // CA2: same path as CP1 with a failing repository -> the failure and its errors pass through unchanged.
    [Fact]
    public async Task Search_WhenRepositoryFails_ReturnsSameFailureAndErrors()
    {
        var repository = new FakeRepository
        {
            SearchResult = Result<IEnumerable<User>>.Failure("Error A", "Error B")
        };
        var service = new UserService(repository);

        var result = await service.Search("ana");

        Assert.True(result.IsFailure);
        Assert.Null(result.Value);
        Assert.Equal(new[] { "Error A", "Error B" }, result.Errors);
        Assert.Equal(1, repository.SearchCallCount);
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
        public Result<IEnumerable<User>> SearchResult { get; init; } =
            Result<IEnumerable<User>>.Success(new List<User>());

        public int SearchCallCount { get; private set; }
        public string? RequestedTerm { get; private set; }

        public Task<Result<IEnumerable<User>>> Search(string property)
        {
            SearchCallCount++;
            RequestedTerm = property;
            return Task.FromResult(SearchResult);
        }

        public Task<Result<int>> Delete(int id) =>
            throw new NotSupportedException();

        public Task<Result<IEnumerable<User>>> GetAll() =>
            throw new NotSupportedException();

        public Task<Result<int>> Insert(User model) =>
            throw new NotSupportedException();

        public Task<Result<int>> Update(User model) =>
            throw new NotSupportedException();

        public Task<Result<User?>> GetById(int id) =>
            throw new NotSupportedException();

        public Task<Result<User?>> GetByUsername(string username) =>
            throw new NotSupportedException();
    }
}
