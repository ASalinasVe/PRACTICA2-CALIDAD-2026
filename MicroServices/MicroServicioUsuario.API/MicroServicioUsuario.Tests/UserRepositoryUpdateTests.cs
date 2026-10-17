using MicroServicioUser.Dom.Entities;
using MicroServicoUser.Inf.Persistence.Database;
using MicroServicoUser.Inf.Repository;

namespace MicroServicioUsuario.Tests;

public class UserRepositoryUpdateTests
{
    [Fact]
    public async Task Update_WhenDatabaseUpdatesUser_ReturnsSuccessAndAffectedRows()
    {
        var user = CreateUser();
        var database = new FakeMySqlConnectionManager { RowsAffected = 1 };
        var repository = new UserRepository(database);

        var result = await repository.Update(user);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
        Assert.Empty(result.Errors);
        Assert.Equal(1, database.CallCount);
        Assert.Contains("UPDATE user", database.Query!);
        Assert.Contains("WHERE id = @Id", database.Query!);
        Assert.Same(user, database.Model);
    }

    [Fact]
    public async Task Update_WhenDatabaseThrows_ReturnsFailureWithExceptionMessage()
    {
        var user = CreateUser();
        var database = new FakeMySqlConnectionManager
        {
            ExceptionToThrow = new InvalidOperationException("Error de conexión")
        };
        var repository = new UserRepository(database);

        var result = await repository.Update(user);

        Assert.True(result.IsFailure);
        Assert.Equal("Error de conexión", Assert.Single(result.Errors));
        Assert.Equal(1, database.CallCount);
        Assert.Same(user, database.Model);
    }

    private static User CreateUser() => new()
    {
        Id = 41,
        Username = "ana.rojas",
        PasswordHash = "password-hash",
        FirstName = "Ana",
        LastName = "Rojas",
        Email = "ana@example.com",
        Role = "1",
        CreatedBy = 7,
        FirstLogin = 0,
        Status = true
    };

    private sealed class FakeMySqlConnectionManager : IMySqlConnectionManager
    {
        public int RowsAffected { get; init; }
        public Exception? ExceptionToThrow { get; init; }
        public int CallCount { get; private set; }
        public string? Query { get; private set; }
        public object? Model { get; private set; }

        public int ExecuteParameterizedNonQuery<T>(string query, T model) where T : new()
        {
            CallCount++;
            Query = query;
            Model = model;

            if (ExceptionToThrow is not null)
            {
                throw ExceptionToThrow;
            }

            return RowsAffected;
        }

        public IEnumerable<T> ExecuteQuery<T>(string query) where T : new() =>
            throw new NotSupportedException();

        public IEnumerable<T> ExecuteParameterizedQuery<T>(string query, T model) where T : new() =>
            throw new NotSupportedException();

        public IEnumerable<TOut> ExecuteParameterizedQuery<TOut, TParam>(string query, TParam model)
            where TOut : new() =>
            throw new NotSupportedException();
    }
}
