using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;
using MicroServicioUsuario.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MicroServicioUsuario.Tests.Controllers;

// Cases map to the control-flow graph of UserController.Update(User)
// (docs/testing/usuarios/control-flow-graphs.md, lines 180-216).
// CP1-CP4 cover the four independent paths derived from V(G) = 4.
// CA5-CA7 are additional cases that do not add independent paths.
public class UserControllerUpdateTests
{
    // CP1: invalid ModelState -> 400 BadRequest and the service is not called.
    [Fact]
    public async Task Update_WhenModelStateIsInvalid_ReturnsBadRequestWithoutCallingService()
    {
        var service = new FakeUserService();
        var controller = CreateController(service);
        controller.ModelState.AddModelError("Username", "El nombre de usuario es obligatorio.");

        var result = await controller.Update(CreateUser(10, "ana"));

        var response = Assert.IsType<BadRequestObjectResult>(result);
        var modelErrors = Assert.IsType<SerializableError>(response.Value);
        Assert.Contains("Username", modelErrors.Keys);
        Assert.Equal(0, service.UpdateCallCount);
    }

    // CP2: valid model and service succeeds -> 200 OK with message and the updated user.
    [Fact]
    public async Task Update_WhenServiceSucceeds_ReturnsOkWithMessageAndUser()
    {
        var user = CreateUser(10, "ana");
        var service = new FakeUserService
        {
            UpdateResult = Result<int>.Success(10)
        };
        var controller = CreateController(service);

        var result = await controller.Update(user);

        var response = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Usuario actualizado exitosamente",
            ReadProperty<string>(response.Value!, "message"));
        Assert.Same(user, ReadProperty<User>(response.Value!, "data"));
        Assert.Equal(1, service.UpdateCallCount);
        Assert.Same(user, service.UpdatedUser);
    }

    // CP3: service fails with "No se encontró" -> 404 NotFound with the user id.
    [Fact]
    public async Task Update_WhenUserIsNotFound_ReturnsNotFound()
    {
        var service = new FakeUserService
        {
            UpdateResult = Result<int>.Failure("No se encontró el usuario")
        };
        var controller = CreateController(service);

        var result = await controller.Update(CreateUser(10, "ana"));

        var response = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Usuario con ID 10 no encontrada",
            ReadProperty<string>(response.Value!, "message"));
        Assert.Equal(1, service.UpdateCallCount);
    }

    // CP4: service fails with any other error -> 500 with the fixed message and the errors.
    [Fact]
    public async Task Update_WhenAnotherErrorOccurs_ReturnsInternalServerErrorWithMessageAndErrors()
    {
        var service = new FakeUserService
        {
            UpdateResult = Result<int>.Failure("Error de conexión")
        };
        var controller = CreateController(service);

        var result = await controller.Update(CreateUser(10, "ana"));

        var response = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, response.StatusCode);
        Assert.Equal("Error al actualizar el usuario",
            ReadProperty<string>(response.Value!, "message"));
        var errors = ReadProperty<List<string>>(response.Value!, "error");
        Assert.Equal("Error de conexión", Assert.Single(errors));
    }

    // CA5: same not-found path as CP3 -> the message uses the id of the received user.
    [Fact]
    public async Task Update_WhenUserIsNotFound_MessageUsesReceivedUserId()
    {
        var service = new FakeUserService
        {
            UpdateResult = Result<int>.Failure("No se encontró el usuario")
        };
        var controller = CreateController(service);

        var result = await controller.Update(CreateUser(77, "luis"));

        var response = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Usuario con ID 77 no encontrada",
            ReadProperty<string>(response.Value!, "message"));
    }

    // CA6: same other-error path as CP4 with several errors -> all errors preserved in order.
    [Fact]
    public async Task Update_WhenAnotherErrorsOccurWithSeveralErrors_ReturnsAllErrorsInOrder()
    {
        var service = new FakeUserService
        {
            UpdateResult = Result<int>.Failure("Error A", "Error B")
        };
        var controller = CreateController(service);

        var result = await controller.Update(CreateUser(10, "ana"));

        var response = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, response.StatusCode);
        var errors = ReadProperty<List<string>>(response.Value!, "error");
        Assert.Equal(new[] { "Error A", "Error B" }, errors);
    }

    // CA7: same not-found path as CP3 with mixed errors -> NotFound if any error contains "No se encontró".
    [Fact]
    public async Task Update_WhenAnyErrorContainsNotFoundText_ReturnsNotFound()
    {
        var service = new FakeUserService
        {
            UpdateResult = Result<int>.Failure("Error de conexión", "No se encontró el usuario")
        };
        var controller = CreateController(service);

        var result = await controller.Update(CreateUser(10, "ana"));

        Assert.IsType<NotFoundObjectResult>(result);
    }

    private static UserController CreateController(FakeUserService service) =>
        new(service, null!, null!, null!);

    private static User CreateUser(int id, string username) => new()
    {
        Id = id,
        Username = username,
        FirstName = "Nombre",
        LastName = "Apellido",
        Email = $"{username}@example.com",
        Role = "Admin"
    };

    private static T ReadProperty<T>(object value, string propertyName)
    {
        var property = value.GetType().GetProperty(propertyName);
        Assert.NotNull(property);
        return Assert.IsType<T>(property!.GetValue(value));
    }

    private sealed class FakeUserService : IRepositoryService<User>
    {
        public Result<int> UpdateResult { get; init; } = Result<int>.Success(1);
        public int UpdateCallCount { get; private set; }
        public User? UpdatedUser { get; private set; }

        public Task<Result<int>> Update(User model)
        {
            UpdateCallCount++;
            UpdatedUser = model;
            return Task.FromResult(UpdateResult);
        }

        public Task<Result<IEnumerable<User>>> GetAll() =>
            throw new NotSupportedException();

        public Task<Result<User>> GetById(int id) =>
            throw new NotSupportedException();

        public Task<Result<int>> Delete(int id) =>
            throw new NotSupportedException();

        public Task<Result<int>> Insert(User model) =>
            throw new NotSupportedException();

        public Task<Result<IEnumerable<User>>> Search(string property) =>
            throw new NotSupportedException();
    }
}
