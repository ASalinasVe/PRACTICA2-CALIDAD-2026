using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;
using MicroServicioUsuario.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MicroServicioUsuario.Tests.Controllers;

public class UserControllerCreateTests
{
    [Fact]
    public async Task Create_WhenModelStateIsInvalid_ReturnsBadRequest()
    {
        var service = new FakeUserService(Result<int>.Success(42));
        var controller = CreateController(service);
        var user = CreateUser();

        // La llamada directa al controlador no ejecuta
        // automáticamente la validación de ASP.NET Core.
        controller.ModelState.AddModelError("Username", "Requerido");

        var result = await controller.Create(user);

        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, response.StatusCode);

        var errors = Assert.IsType<SerializableError>(response.Value);
        var messages = Assert.IsType<string[]>(errors["Username"]);

        Assert.Equal(new[] { "Requerido" }, messages);
        Assert.Equal(0, service.InsertCallCount);
        Assert.Null(service.LastInsertedUser);
    }

    [Fact]
    public async Task Create_WhenInsertSucceeds_ReturnsCreatedUser()
    {
        var service = new FakeUserService(Result<int>.Success(42));
        var controller = CreateController(service);
        var user = CreateUser();

        var result = await controller.Create(user);

        var response = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, response.StatusCode);
        Assert.Equal(nameof(UserController.Select), response.ActionName);

        Assert.NotNull(response.RouteValues);
        Assert.Equal(42, Assert.IsType<int>(response.RouteValues!["id"]));

        Assert.Equal(42, ReadProperty<int>(response.Value!, "id"));
        Assert.Equal(
            "Usuario creado exitosamente",
            ReadProperty<string>(response.Value!, "message"));

        Assert.Same(user, ReadProperty<User>(response.Value!, "data"));
        Assert.Equal(1, service.InsertCallCount);
        Assert.Same(user, service.LastInsertedUser);
    }

    [Fact]
    public async Task Create_WhenInsertFails_ReturnsInternalServerError()
    {
        var service = new FakeUserService(
            Result<int>.Failure("Error de conexión"));

        var controller = CreateController(service);
        var user = CreateUser();

        var result = await controller.Create(user);

        var response = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, response.StatusCode);

        Assert.Equal(
            "Error al crear el usuario",
            ReadProperty<string>(response.Value!, "message"));

        var errors = ReadProperty<List<string>>(response.Value!, "error");
        Assert.Equal(new[] { "Error de conexión" }, errors);

        Assert.Equal(1, service.InsertCallCount);
        Assert.Same(user, service.LastInsertedUser);
    }

    private static UserController CreateController(
        IRepositoryService<User> service) =>
        new(
            service: service,
            loginService: null!,
            registrationService: null!,
            jwtService: null!);

    private static User CreateUser() => new()
    {
        Id = 10,
        Username = "ale.salinas",
        PasswordHash = "hash-de-prueba-123",
        FirstName = "Alejandra",
        SecondName = null,
        LastName = "Salinas",
        SecondLastName = null,
        Email = "ale.salinas@example.com",
        Role = "Admin",
        CreatedBy = 1,
        CreatedDate = new DateTime(2025, 1, 1, 10, 0, 0),
        LastUpdate = new DateTime(2025, 1, 2, 11, 0, 0),
        Status = false,
        FirstLogin = 0
    };

    // Permite comprobar las propiedades de las respuestas anónimas.
    private static T ReadProperty<T>(object value, string name)
    {
        var property = value.GetType().GetProperty(name);
        Assert.NotNull(property);

        return Assert.IsType<T>(property!.GetValue(value));
    }

    private sealed class FakeUserService : IRepositoryService<User>
    {
        private readonly Result<int> _insertResult;

        public int InsertCallCount { get; private set; }
        public User? LastInsertedUser { get; private set; }

        public FakeUserService(Result<int> insertResult)
        {
            _insertResult = insertResult;
        }

        public Task<Result<int>> Insert(User model)
        {
            InsertCallCount++;
            LastInsertedUser = model;
            return Task.FromResult(_insertResult);
        }

        public Task<Result<IEnumerable<User>>> GetAll() =>
            throw new NotSupportedException();

        public Task<Result<int>> Update(User model) =>
            throw new NotSupportedException();

        public Task<Result<int>> Delete(int id) =>
            throw new NotSupportedException();

        public Task<Result<User>> GetById(int id) =>
            throw new NotSupportedException();

        public Task<Result<IEnumerable<User>>> Search(string property) =>
            throw new NotSupportedException();
    }
}