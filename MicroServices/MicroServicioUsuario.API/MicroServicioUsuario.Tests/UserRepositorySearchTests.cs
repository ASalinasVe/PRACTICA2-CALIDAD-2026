using MicroServicioUser.Dom.Entities;
using MicroServicoUser.Inf.Persistence.Database;
using MicroServicoUser.Inf.Repository;

namespace MicroServicioUsuario.Tests;

public class UserRepositorySearchTests
{
    [Fact]
    public async Task Search_WhenQuerySucceeds_ReturnsMatchingActiveUsers()
    {
        const string property = "Ana";
        var users = new[]
        {
            new User
            {
                Id = 1,
                Username = "Ana1",
                PasswordHash = "hash",
                FirstName = "Mara",
                LastName = "Perez",
                Email = "mara@example.com",
                Role = "user",
                Status = true
            },
            new User
            {
                Id = 2,
                Username = "user2",
                PasswordHash = "hash",
                FirstName = "Ana",
                LastName = "Lopez",
                Email = "ana@example.com",
                Role = "user",
                Status = true
            }
        };
        var database = new FakeMySqlConnectionManager { SearchResults = users };
        var repository = new UserRepository(database);

        var result = await repository.Search(property);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.Collection(result.Value,
            user => Assert.Equal(1, user.Id),
            user => Assert.Equal(2, user.Id));
        Assert.Equal(1, database.QueryCallCount);

        var probe = Assert.IsType<User>(database.Model);
        Assert.Equal(property, probe.Username);
        Assert.Equal(property, probe.FirstName);
        Assert.Equal(property, probe.LastName);
        Assert.Equal(property, probe.Email);
        Assert.Equal(property, probe.Role);

        var query = database.Query!;
        Assert.Contains("WHERE status = TRUE AND", query);
        Assert.Contains("username LIKE CONCAT('%', @Username, '%')", query);
        Assert.Contains("first_name LIKE CONCAT('%', @FirstName, '%')", query);
        Assert.Contains("first_last_name LIKE CONCAT('%', @LastName, '%')", query);
        Assert.Contains("email LIKE CONCAT('%', @Email, '%')", query);
        Assert.Contains("role LIKE CONCAT('%', @Role, '%')", query);
    }

    [Fact]
    public async Task Search_WhenQueryThrows_ReturnsFailureWithExceptionMessage()
    {
        const string property = "Ana";
        var database = new FakeMySqlConnectionManager
        {
            ExceptionToThrow = new InvalidOperationException("Error de conexi\u00F3n")
        };
        var repository = new UserRepository(database);

        var result = await repository.Search(property);

        Assert.True(result.IsFailure);
        Assert.Equal("Error de conexi\u00F3n", Assert.Single(result.Errors));
        Assert.Null(result.Value);
        Assert.Equal(1, database.QueryCallCount);
        Assert.Equal(property, Assert.IsType<User>(database.Model).Username);
    }

    private sealed class FakeMySqlConnectionManager : IMySqlConnectionManager
    {
        public IEnumerable<User> SearchResults { get; init; } = Array.Empty<User>();
        public Exception? ExceptionToThrow { get; init; }
        public int QueryCallCount { get; private set; }
        public string? Query { get; private set; }
        public object? Model { get; private set; }

        public IEnumerable<T> ExecuteParameterizedQuery<T>(string query, T model) where T : new()
        {
            QueryCallCount++;
            Query = query;
            Model = model;

            if (ExceptionToThrow is not null)
            {
                throw ExceptionToThrow;
            }

            return (IEnumerable<T>)(object)SearchResults;
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
