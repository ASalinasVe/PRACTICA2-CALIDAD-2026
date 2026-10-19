using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;
using MicroServicioUsuario.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MicroServicioUsuario.Tests.Controllers;

// Cases map to the control-flow graph of UserController.Select()
// (docs/testing/usuarios/control-flow-graphs.md, lines 86-101).
// CP1-CP2 cover the two independent paths derived from V(G) = 2.
// CA3-CA4 are additional data-level cases that do not add independent paths.
public class UserControllerSelectTests
{
    // CP1: GetAll succeeds -> 200 OK with the collection returned by the service.
    [Fact]
    public async Task Select_WhenGetAllSucceeds_ReturnsOkWithUsers()
    {
        var users = new List<User> { CreateUser(1, "ana"), CreateUser(2, "luis") };
        var service = new FakeUserService
        {
            GetAllResult = Result<IEnumerable<User>>.Success(users)
        };
        var controller = CreateController(service);

        var result = await controller.Select();

        var response = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Same(users, response.Value);
        Assert.Equal(1, service.GetAllCallCount);
    }

    // CP2: GetAll fails -> 500 with the fixed message and the service errors.
    [Fact]
    public async Task Select_WhenGetAllFails_ReturnsInternalServerErrorWithMessageAndErrors()
    {
        var service = new FakeUserService
        {
            GetAllResult = Result<IEnumerable<User>>.Failure("Error de conexión")
        };
        var controller = CreateController(service);

        var result = await controller.Select();

        var response = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, response.StatusCode);
        Assert.Equal("Error al obtener los usuarios",
            ReadProperty<string>(response.Value!, "message"));
        var errors = ReadProperty<List<string>>(response.Value!, "error");
        Assert.Equal("Error de conexión", Assert.Single(errors));
        Assert.Equal(1, service.GetAllCallCount);
    }

    // CA3: same failure path as CP2, with several errors -> all errors preserved in order.
    [Fact]
    public async Task Select_WhenGetAllFailsWithSeveralErrors_ReturnsAllErrorsInOrder()
    {
        var service = new FakeUserService
        {
            GetAllResult = Result<IEnumerable<User>>.Failure("Error A", "Error B")
        };
        var controller = CreateController(service);

        var result = await controller.Select();

        var response = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, response.StatusCode);
        var errors = ReadProperty<List<string>>(response.Value!, "error");
        Assert.Equal(new[] { "Error A", "Error B" }, errors);
    }

    // CA4: same success path as CP1, with no users -> 200 OK with an empty collection.
    [Fact]
    public async Task Select_WhenGetAllSucceedsWithoutUsers_ReturnsOkWithEmptyCollection()
    {
        var users = new List<User>();
        var service = new FakeUserService
        {
            GetAllResult = Result<IEnumerable<User>>.Success(users)
        };
        var controller = CreateController(service);

        var result = await controller.Select();

        var response = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Empty(Assert.IsAssignableFrom<IEnumerable<User>>(response.Value));
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
        public Result<IEnumerable<User>> GetAllResult { get; init; } =
            Result<IEnumerable<User>>.Success(new List<User>());
        public int GetAllCallCount { get; private set; }

        public Task<Result<IEnumerable<User>>> GetAll()
        {
            GetAllCallCount++;
            return Task.FromResult(GetAllResult);
        }

        public Task<Result<int>> Delete(int id) =>
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
