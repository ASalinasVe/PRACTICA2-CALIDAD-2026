using MicroServicoUser.Inf.Persistence.Database;
using MicroServicoUser.Inf.Repository;
using MicroServicioUser.Dom.Entities;

namespace MicroServicioUsuario.Tests;

// Cases map to the control-flow graph of UserRepository.GetById(int)
// (docs/testing/usuarios/control-flow-graphs.md, lines 164-194).
// The method body has V(G) = 2: CP1 (normal return) and CP2 (catch).
// CA3 is an additional data-level case: the query returns no rows, which the method
// handles through FirstOrDefault without adding a branch.
public class UserRepositoryGetByIdTests
{
    private const int UserId = 10;

    // CP1: the query returns the active user -> Success wrapping that same user; the id is passed to the query.
    [Fact]
    public async Task GetById_WhenActiveUserExists_ReturnsSuccessWithUser()
    {
        var user = CreateUser(UserId);
        var connection = new FakeConnectionManager
        {
            Rows = new List<User> { user }
        };
        var repository = new UserRepository(connection);

        var result = await repository.GetById(UserId);

        Assert.True(result.IsSuccess);
        Assert.Same(user, result.Value);
        Assert.Empty(result.Errors);
        Assert.Equal(1, connection.QueryCallCount);
        Assert.Contains("WHERE id = @Id AND status = TRUE", connection.ReceivedQuery);
        Assert.Equal(UserId, connection.ReceivedModelId);
    }

    // CP2: the query throws -> Failure with the exception message; the exception does not escape.
    [Fact]
    public async Task GetById_WhenQueryThrows_ReturnsFailureWithExceptionMessage()
    {
        var connection = new FakeConnectionManager
        {
            ExceptionToThrow = new InvalidOperationException("Conexión caída")
        };
        var repository = new UserRepository(connection);

        var result = await repository.GetById(UserId);

        Assert.True(result.IsFailure);
        Assert.Null(result.Value);
        Assert.Equal("Conexión caída", Assert.Single(result.Errors));
        Assert.Equal(1, connection.QueryCallCount);
    }

    // CA3: same normal path as CP1 with no matching active user -> Success with a null value.
    [Fact]
    public async Task GetById_WhenNoActiveUserMatches_ReturnsSuccessWithNull()
    {
        var connection = new FakeConnectionManager
        {
            Rows = new List<User>()
        };
        var repository = new UserRepository(connection);

        var result = await repository.GetById(UserId);

        Assert.True(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Empty(result.Errors);
    }

    private static User CreateUser(int id) => new()
    {
        Id = id,
        Username = "ale.salinas",
        PasswordHash = "hash-de-prueba-123",
        FirstName = "Alejandra",
        LastName = "Salinas",
        Email = "ale.salinas@example.com",
        Role = "Admin",
        CreatedBy = 1,
        Status = true,
        FirstLogin = 0
    };

    private sealed class FakeConnectionManager : IMySqlConnectionManager
    {
        public IEnumerable<User> Rows { get; init; } = new List<User>();
        public Exception? ExceptionToThrow { get; init; }

        public int QueryCallCount { get; private set; }
        public string? ReceivedQuery { get; private set; }
        public int? ReceivedModelId { get; private set; }

        public IEnumerable<T> ExecuteParameterizedQuery<T>(string query, T model) where T : new()
        {
            QueryCallCount++;
            ReceivedQuery = query;
            ReceivedModelId = (model as User)?.Id;

            if (ExceptionToThrow is not null)
                throw ExceptionToThrow;

            return (IEnumerable<T>)(object)Rows;
        }

        public IEnumerable<T> ExecuteQuery<T>(string query) where T : new() =>
            throw new NotSupportedException();

        public IEnumerable<TOut> ExecuteParameterizedQuery<TOut, TParam>(string query, TParam model)
            where TOut : new() =>
            throw new NotSupportedException();

        public int ExecuteParameterizedNonQuery<T>(string query, T model) where T : new() =>
            throw new NotSupportedException();
    }
}
