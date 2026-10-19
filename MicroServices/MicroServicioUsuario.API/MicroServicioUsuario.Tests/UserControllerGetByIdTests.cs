using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;
using MicroServicioUsuario.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MicroServicioUsuario.Tests.Controllers;

// Cases map to the control-flow graph of UserController.GetById(int)
// (docs/testing/usuarios/control-flow-graphs.md, lines 105-120).
// CP1-CP2 cover the two independent paths derived from V(G) = 2.
// CA3-CA4 are additional data-level cases that do not add independent paths.
public class UserControllerGetByIdTests
{
    // CP1: GetById succeeds -> 200 OK with the user returned by the service; id is forwarded.
    [Fact]
    public async Task GetById_WhenServiceSucceeds_ReturnsOkWithUser()
    {
        var user = CreateUser(10, "ana");
        var service = new FakeUserService
        {
            GetByIdResult = Result<User>.Success(user)
        };
        var controller = CreateController(service);

        var result = await controller.GetById(10);

        var response = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Same(user, response.Value);
        Assert.Equal(1, service.GetByIdCallCount);
        Assert.Equal(10, service.GetByIdRequestedId);
    }

    // CP2: GetById fails -> 500 with the fixed message and the service errors.
    [Fact]
    public async Task GetById_WhenServiceFails_ReturnsInternalServerErrorWithMessageAndErrors()
    {
        var service = new FakeUserService
        {
            GetByIdResult = Result<User>.Failure("Error de conexión")
        };
        var controller = CreateController(service);

        var result = await controller.GetById(10);

        var response = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, response.StatusCode);
        Assert.Equal("Error al obtener el usuario",
            ReadProperty<string>(response.Value!, "message"));
        var errors = ReadProperty<List<string>>(response.Value!, "error");
        Assert.Equal("Error de conexión", Assert.Single(errors));
        Assert.Equal(10, service.GetByIdRequestedId);
    }

    // CA3: same failure path as CP2, with several errors -> all errors preserved in order.
    [Fact]
    public async Task GetById_WhenServiceFailsWithSeveralErrors_ReturnsAllErrorsInOrder()
    {
        var service = new FakeUserService
        {
            GetByIdResult = Result<User>.Failure("Error A", "Error B")
        };
        var controller = CreateController(service);

        var result = await controller.GetById(10);

        var response = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, response.StatusCode);
        var errors = ReadProperty<List<string>>(response.Value!, "error");
        Assert.Equal(new[] { "Error A", "Error B" }, errors);
    }

    // CA4: same failure path as CP2, with the "not found" message -> still 500, never 404.
    // Characterization: the controller does not branch on "No se encontró" in GetById.
    [Fact]
    public async Task GetById_WhenUserIsNotFound_ReturnsInternalServerErrorNotNotFound()
    {
        var service = new FakeUserService
        {
            GetByIdResult = Result<User>.Failure("No se encontró el usuario")
        };
        var controller = CreateController(service);

        var result = await controller.GetById(10);

        Assert.IsNotType<NotFoundObjectResult>(result.Result);
        var response = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, response.StatusCode);
        Assert.Equal("Error al obtener el usuario",
            ReadProperty<string>(response.Value!, "message"));
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
        public Result<User> GetByIdResult { get; init; } =
            Result<User>.Success(new User());
        public int GetByIdCallCount { get; private set; }
        public int? GetByIdRequestedId { get; private set; }

        public Task<Result<User>> GetById(int id)
        {
            GetByIdCallCount++;
            GetByIdRequestedId = id;
            return Task.FromResult(GetByIdResult);
        }

        public Task<Result<IEnumerable<User>>> GetAll() =>
            throw new NotSupportedException();

        public Task<Result<int>> Delete(int id) =>
            throw new NotSupportedException();

        public Task<Result<int>> Insert(User model) =>
            throw new NotSupportedException();

        public Task<Result<int>> Update(User model) =>
            throw new NotSupportedException();

        public Task<Result<IEnumerable<User>>> Search(string property) =>
            throw new NotSupportedException();
    }
}
