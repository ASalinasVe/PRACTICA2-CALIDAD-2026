using MicroServicoUser.Inf.Persistence.Database;
using MicroServicoUser.Inf.Repository;

namespace MicroServicioUsuario.Tests;

public class UserRepositoryDeleteTests
{
    [Fact]
    public async Task Delete_WhenUpdateSucceeds_ReturnsSuccessAndExecutesLogicalDelete()
    {
        const int userId = 41;
        var executor = new FakeMySqlConnectionManager { RowsAffected = 1 };
        var repository = new UserRepository(executor);

        var result = await repository.Delete(userId);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
        Assert.Empty(result.Errors);
        Assert.Equal(1, executor.CallCount);
        Assert.Contains("UPDATE user", executor.Query!);
        Assert.Contains("status = FALSE", executor.Query!);
        Assert.Contains("WHERE id = @Id", executor.Query!);
        Assert.Equal(userId, Assert.IsType<IdDto>(executor.Model).Id);
    }

    [Fact]
    public async Task Delete_WhenUpdateThrows_ReturnsFailureWithExceptionMessage()
    {
        const int userId = 41;
        var executor = new FakeMySqlConnectionManager
        {
            ExceptionToThrow = new InvalidOperationException("Error de conexi\u00F3n")
        };
        var repository = new UserRepository(executor);

        var result = await repository.Delete(userId);

        Assert.True(result.IsFailure);
        Assert.Contains("Error de conexi\u00F3n", result.Errors);
        Assert.Equal(1, executor.CallCount);
        Assert.Equal(userId, Assert.IsType<IdDto>(executor.Model).Id);
    }

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
