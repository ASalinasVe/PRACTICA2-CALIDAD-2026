using MicroServicioUser.Dom.Entities;
using MicroServicoUser.Inf.Persistence.Database;
using MicroServicoUser.Inf.Repository;

namespace MicroServicioUsuario.Tests;

public class UserRepositoryGetAllTests
{
    [Fact]
    public async Task GetAll_WhenQuerySucceeds_ReturnsActiveUsers()
    {
        var activeUser = CreateUser(id: 17, status: true);
        var database = new FakeMySqlConnectionManager { QueryResults = new[] { activeUser } };
        var repository = new UserRepository(database);

        var result = await repository.GetAll();

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        var returnedUser = Assert.Single(result.Value);
        Assert.Equal(activeUser.Id, returnedUser.Id);
        Assert.True(returnedUser.Status);
        Assert.Equal(1, database.QueryCallCount);
        Assert.Contains("WHERE status = TRUE", database.Query!);
        Assert.Contains("ORDER BY id DESC", database.Query!);
    }

    [Fact]
    public async Task GetAll_WhenQueryThrows_ReturnsFailureWithExceptionMessage()
    {
        var database = new FakeMySqlConnectionManager
        {
            ExceptionToThrow = new InvalidOperationException("Error de conexi\u00F3n")
        };
        var repository = new UserRepository(database);

        var result = await repository.GetAll();

        Assert.True(result.IsFailure);
        Assert.Equal("Error de conexi\u00F3n", Assert.Single(result.Errors));
        Assert.Null(result.Value);
        Assert.Equal(1, database.QueryCallCount);
        Assert.Contains("WHERE status = TRUE", database.Query!);
    }

    private static User CreateUser(int id, bool status) => new()
    {
        Id = id,
        Username = $"usuario{id}",
        PasswordHash = "hash",
        FirstName = "Ana",
        LastName = "Perez",
        Email = $"usuario{id}@example.com",
        Role = "User",
        Status = status
    };

    private sealed class FakeMySqlConnectionManager : IMySqlConnectionManager
    {
        public IEnumerable<User> QueryResults { get; init; } = Array.Empty<User>();
        public Exception? ExceptionToThrow { get; init; }
        public int QueryCallCount { get; private set; }
        public string? Query { get; private set; }

        public IEnumerable<T> ExecuteQuery<T>(string query) where T : new()
        {
            QueryCallCount++;
            Query = query;

            if (ExceptionToThrow is not null)
            {
                throw ExceptionToThrow;
            }

            return (IEnumerable<T>)(object)QueryResults;
        }

        public IEnumerable<T> ExecuteParameterizedQuery<T>(string query, T model) where T : new() =>
            throw new NotSupportedException();

        public IEnumerable<TOut> ExecuteParameterizedQuery<TOut, TParam>(string query, TParam model)
            where TOut : new() =>
            throw new NotSupportedException();

        public int ExecuteParameterizedNonQuery<T>(string query, T model) where T : new() =>
            throw new NotSupportedException();
    }
}
