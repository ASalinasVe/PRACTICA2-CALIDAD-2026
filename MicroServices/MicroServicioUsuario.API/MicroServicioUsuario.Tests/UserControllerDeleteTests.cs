using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;
using MicroServicioUsuario.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MicroServicioUsuario.Tests.Controllers;

public class UserControllerDeleteTests
{
    [Fact]
    public async Task Delete_WhenSuccessful_ReturnsOk()
    {
        var service = new FakeUserService
        {
            DeleteResult = Result<int>.Success(1)
        };
        var controller = CreateController(service);

        var result = await controller.Delete(10);

        var response = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Usuario con ID 10 eliminada exitosamente",
            ReadProperty<string>(response.Value!, "message"));
        AssertDeleteCalledOnceWithId(service, 10);
    }

    [Fact]
    public async Task Delete_WhenUserIsNotFound_ReturnsNotFound()
    {
        var service = new FakeUserService
        {
            DeleteResult = Result<int>.Failure("No se encontró el usuario")
        };
        var controller = CreateController(service);

        var result = await controller.Delete(10);

        var response = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Usuario con ID 10 no encontrada",
            ReadProperty<string>(response.Value!, "message"));
        AssertDeleteCalledOnceWithId(service, 10);
    }

    [Fact]
    public async Task Delete_WhenAnotherErrorOccurs_ReturnsInternalServerError()
    {
        var service = new FakeUserService
        {
            DeleteResult = Result<int>.Failure("Error de conexión")
        };
        var controller = CreateController(service);

        var result = await controller.Delete(10);

        var response = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, response.StatusCode);
        Assert.Equal("Error al eliminar el usario",
            ReadProperty<string>(response.Value!, "message"));

        var errors = ReadProperty<List<string>>(response.Value!, "error");
        Assert.Equal("Error de conexión", Assert.Single(errors));
        AssertDeleteCalledOnceWithId(service, 10);
    }

    private static UserController CreateController(FakeUserService service) =>
        new(service, null!, null!, null!);

    private static void AssertDeleteCalledOnceWithId(
        FakeUserService service, int expectedId)
    {
        Assert.Equal(1, service.DeleteCallCount);
        Assert.Equal(expectedId, service.DeletedUserId);
    }

    private static T ReadProperty<T>(object value, string propertyName)
    {
        var property = value.GetType().GetProperty(propertyName);
        Assert.NotNull(property);
        return Assert.IsType<T>(property!.GetValue(value));
    }

    private sealed class FakeUserService : IRepositoryService<User>
    {
        public Result<int> DeleteResult { get; init; } = Result<int>.Success(1);
        public int DeleteCallCount { get; private set; }
        public int? DeletedUserId { get; private set; }

        public Task<Result<int>> Delete(int id)
        {
            DeleteCallCount++;
            DeletedUserId = id;
            return Task.FromResult(DeleteResult);
        }

        public Task<Result<IEnumerable<User>>> GetAll() =>
            throw new NotSupportedException();

        public Task<Result<int>> Insert(User model) =>
            throw new NotSupportedException();

        public Task<Result<int>> Update(User model) =>
            throw new NotSupportedException();

        public Task<Result<User>> GetById(int id) =>
            throw new NotSupportedException();

        public Task<Result<IEnumerable<User>>> Search(string property) =>
            throw new NotSupportedException();
    }
}