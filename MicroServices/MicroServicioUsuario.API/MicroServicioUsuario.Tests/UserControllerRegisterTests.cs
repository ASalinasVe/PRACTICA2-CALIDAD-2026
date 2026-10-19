using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Entities;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;
using MicroServicioUsuario.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MicroServicioUsuario.Tests.Controllers;

// Cases map to the control-flow graph of UserController.Register(RegisterDTO)
// (docs/testing/usuarios/control-flow-graphs.md, lines 79-83).
// The method has no decisions: V(G) = 1, a single path (CP1).
// CA2 is an additional data-level case on the same path.
// RegistrationService is a concrete class, so it is exercised with a fake IRegistration
// and a fake IEmailService. The email is scheduled only on the success path.
public class UserControllerRegisterTests
{
    // CP1: registration succeeds -> 200 OK with Ok = true and Error = null; inputs are forwarded.
    [Fact]
    public async Task Register_WhenRegistrationSucceeds_ReturnsOkWithOkTrueAndNullError()
    {
        var registration = new FakeRegistration
        {
            RegisterResult = (true, "ana.perez", "Pass123!", null)
        };
        var controller = CreateController(registration);

        var result = await controller.Register(CreateDto());

        var response = Assert.IsType<OkObjectResult>(result.Result);
        Assert.True(ReadProperty<bool>(response.Value!, "Ok"));
        Assert.Null(ReadProperty(response.Value!, "Error"));
        Assert.Equal(1, registration.RegisterCallCount);
        Assert.Equal("Ana", registration.LastFirstName);
        Assert.Equal("ana@example.com", registration.LastEmail);
        Assert.Equal("Estudiante", registration.LastRole);
        Assert.Equal(7, registration.LastCreatedBy);
    }

    // CA2: same path as CP1 with a failing registration -> 200 OK with Ok = false and the error message.
    [Fact]
    public async Task Register_WhenRegistrationFails_ReturnsOkWithOkFalseAndError()
    {
        var registration = new FakeRegistration
        {
            RegisterResult = (false, null, null, "Correo duplicado")
        };
        var controller = CreateController(registration);

        var result = await controller.Register(CreateDto());

        var response = Assert.IsType<OkObjectResult>(result.Result);
        Assert.False(ReadProperty<bool>(response.Value!, "Ok"));
        Assert.Equal("Correo duplicado", ReadProperty<string>(response.Value!, "Error"));
        Assert.Equal(1, registration.RegisterCallCount);
    }

    private static UserController CreateController(FakeRegistration registration)
    {
        var registrationService = new RegistrationService(registration, new EmailService(new FakeEmailService()));
        return new(null!, null!, registrationService, null!);
    }

    private static RegisterDTO CreateDto() => new()
    {
        FirstName = "Ana",
        LastName = "Pérez",
        Email = "ana@example.com",
        Role = "Estudiante",
        CreatedBy = 7
    };

    private static object? ReadProperty(object value, string propertyName) =>
        value.GetType().GetProperty(propertyName)?.GetValue(value);

    private static T ReadProperty<T>(object value, string propertyName)
    {
        var property = value.GetType().GetProperty(propertyName);
        Assert.NotNull(property);
        return Assert.IsType<T>(property!.GetValue(value));
    }

    private sealed class FakeRegistration : IRegistration
    {
        public (bool ok, string? generatedUsername, string? generatedPassword, string? error) RegisterResult { get; init; }
        public int RegisterCallCount { get; private set; }
        public string? LastFirstName { get; private set; }
        public string? LastEmail { get; private set; }
        public string? LastRole { get; private set; }
        public int LastCreatedBy { get; private set; }

        public Task<(bool ok, string? generatedUsername, string? generatedPassword, string? error)> RegisterUser(
            string firstname, string lastName, string email, string role, int createdBy)
        {
            RegisterCallCount++;
            LastFirstName = firstname;
            LastEmail = email;
            LastRole = role;
            LastCreatedBy = createdBy;
            return Task.FromResult(RegisterResult);
        }

        public Task<string> GeneratePassword(int lenght) =>
            throw new NotSupportedException();
    }

    private sealed class FakeEmailService : IEmailService
    {
        public Task<bool> SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true) =>
            Task.FromResult(true);
    }
}
