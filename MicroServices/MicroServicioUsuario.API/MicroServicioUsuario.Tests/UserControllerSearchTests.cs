using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;
using MicroServicioUsuario.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MicroServicioUsuario.Tests.Controllers;

// Cases map to the control-flow graph of UserController.Search(string)
// (docs/testing/usuarios/control-flow-graphs.md, lines 125-140).
// CP1-CP2 cover the two independent paths derived from V(G) = 2.
// CA3-CA4 are additional data-level cases that do not add independent paths.
public class UserControllerSearchTests
{
    // CP1: Search succeeds -> 200 OK with the users returned by the service; the term is forwarded.
    [Fact]
    public async Task Search_WhenServiceSucceeds_ReturnsOkWithUsers()
    {
        var users = new List<User> { CreateUser(1, "ana") };
        var service = new FakeUserService
        {
            SearchResult = Result<IEnumerable<User>>.Success(users)
        };
        var controller = CreateController(service);

        var result = await controller.Search("ana");

        var response = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Same(users, response.Value);
        Assert.Equal(1, service.SearchCallCount);
        Assert.Equal("ana", service.SearchRequestedTerm);
    }

    // CP2: Search fails -> 500 with the fixed message and the service errors.
    [Fact]
    public async Task Search_WhenServiceFails_ReturnsInternalServerErrorWithMessageAndErrors()
    {
        var service = new FakeUserService
        {
            SearchResult = Result<IEnumerable<User>>.Failure("Error de conexión")
        };
        var controller = CreateController(service);

        var result = await controller.Search("ana");

        var response = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, response.StatusCode);
        Assert.Equal("Error al buscar usuario",
            ReadProperty<string>(response.Value!, "message"));
        var errors = ReadProperty<List<string>>(response.Value!, "error");
        Assert.Equal("Error de conexión", Assert.Single(errors));
        Assert.Equal(1, service.SearchCallCount);
    }

    // CA3: same failure path as CP2, with several errors -> all errors preserved in order.
    [Fact]
    public async Task Search_WhenServiceFailsWithSeveralErrors_ReturnsAllErrorsInOrder()
    {
        var service = new FakeUserService
        {
            SearchResult = Result<IEnumerable<User>>.Failure("Error A", "Error B")
        };
        var controller = CreateController(service);

        var result = await controller.Search("ana");

        var response = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, response.StatusCode);
        var errors = ReadProperty<List<string>>(response.Value!, "error");
        Assert.Equal(new[] { "Error A", "Error B" }, errors);
    }

    // CA4: same success path as CP1 -> the term reaches the service unchanged (no trimming or normalization).
    // Characterization: the controller does not transform the search term.
    [Fact]
    public async Task Search_WhenTermHasSpaces_ForwardsTermUnchanged()
    {
        var service = new FakeUserService
        {
            SearchResult = Result<IEnumerable<User>>.Success(new List<User>())
        };
        var controller = CreateController(service);

        var result = await controller.Search("  ana  ");

        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal("  ana  ", service.SearchRequestedTerm);
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
        public Result<IEnumerable<User>> SearchResult { get; init; } =
            Result<IEnumerable<User>>.Success(new List<User>());
        public int SearchCallCount { get; private set; }
        public string? SearchRequestedTerm { get; private set; }

        public Task<Result<IEnumerable<User>>> Search(string property)
        {
            SearchCallCount++;
            SearchRequestedTerm = property;
            return Task.FromResult(SearchResult);
        }

        public Task<Result<IEnumerable<User>>> GetAll() =>
            throw new NotSupportedException();

        public Task<Result<User>> GetById(int id) =>
            throw new NotSupportedException();

        public Task<Result<int>> Delete(int id) =>
            throw new NotSupportedException();

        public Task<Result<int>> Insert(User model) =>
            throw new NotSupportedException();

        public Task<Result<int>> Update(User model) =>
            throw new NotSupportedException();
    }
}
