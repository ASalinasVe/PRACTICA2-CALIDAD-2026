using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;

namespace MicroServicioUsuario.Tests;

public class UserServiceDeleteTests
{
    [Fact]
    public async Task Delete_WhenRepositorySucceeds_ReturnsSuccess()
    {
        var repository = new FakeRepository
        {
            DeleteResult = Result<int>.Success(1)
        };
        var service = new UserService(repository);

        var result = await service.Delete(10);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
        Assert.Empty(result.Errors);
        Assert.Same(repository.DeleteResult, result);
        AssertRepositoryCalledOnce(repository, 10);
    }

    [Fact]
    public async Task Delete_WhenRepositoryFails_ReturnsFailure()
    {
        var repository = new FakeRepository
        {
            DeleteResult = Result<int>.Failure("Error de conexión")
        };
        var service = new UserService(repository);

        var result = await service.Delete(10);

        Assert.True(result.IsFailure);
        Assert.Equal("Error de conexión", Assert.Single(result.Errors));
        Assert.Same(repository.DeleteResult, result);
        AssertRepositoryCalledOnce(repository, 10);
    }

    private static void AssertRepositoryCalledOnce(FakeRepository repository, int id)
    {
        Assert.Equal(1, repository.DeleteCallCount);
        Assert.Equal(id, repository.RequestedId);
    }

    private sealed class FakeRepository : IRepository
    {
        public Result<int> DeleteResult { get; init; } = Result<int>.Success(1);
        public int DeleteCallCount { get; private set; }
        public int? RequestedId { get; private set; }

        public Task<Result<int>> Delete(int id)
        {
            DeleteCallCount++;
            RequestedId = id;
            return Task.FromResult(DeleteResult);
        }

        public Task<Result<IEnumerable<User>>> GetAll() =>
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