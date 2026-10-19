using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Interfaces;

namespace MicroServicioUsuario.Tests;

// Cases map to the control-flow graph of RegistrationService.RegisterUser(...)
// (docs/testing/usuarios/control-flow-graphs.md, lines 874-935).
// The method body has V(G) = 2 (CP1, CP2) and the fire-and-forget callback
// has its own graph with V(G) = 2 (CP3, CP4). Both units are tested separately.
public class RegistrationServiceRegisterUserTests
{
    private static readonly TimeSpan CallbackTimeout = TimeSpan.FromSeconds(5);

    // CP1 (body, failure path): registration fails -> the error is returned and no email is scheduled.
    [Fact]
    public async Task RegisterUser_WhenRegistrationFails_ReturnsFailureAndDoesNotSendEmail()
    {
        var registration = new FakeRegistration
        {
            RegisterResult = (false, null, null, "Correo duplicado")
        };
        var emailService = new FakeEmailService();
        var service = CreateService(registration, emailService);

        var result = await service.RegisterUser("Ana", "Pérez", "ana@example.com", "Estudiante", 1);

        Assert.False(result.ok);
        Assert.Null(result.generatedUsername);
        Assert.Null(result.generatedPassword);
        Assert.Equal("Correo duplicado", result.error);
        Assert.Equal(0, emailService.SendCallCount);
    }

    // CP2 (body, success path): registration succeeds -> credentials are returned and an email is scheduled.
    [Fact]
    public async Task RegisterUser_WhenRegistrationSucceeds_ReturnsCredentialsAndSchedulesEmail()
    {
        var registration = new FakeRegistration
        {
            RegisterResult = (true, "ana.perez", "Pass123!", null)
        };
        var emailService = new FakeEmailService();
        var service = CreateService(registration, emailService);

        var result = await service.RegisterUser("Ana", "Pérez", "ana@example.com", "Estudiante", 1);

        Assert.True(result.ok);
        Assert.Equal("ana.perez", result.generatedUsername);
        Assert.Equal("Pass123!", result.generatedPassword);
        Assert.Null(result.error);
        Assert.Equal(1, registration.RegisterCallCount);
        await emailService.WaitForSendAsync();
        Assert.Equal(1, emailService.SendCallCount);
    }

    // CP3 (callback, normal path): the scheduled task sends the credentials to the registered address.
    [Fact]
    public async Task RegisterUser_ScheduledEmail_SendsCredentialsToRegisteredAddress()
    {
        var registration = new FakeRegistration
        {
            RegisterResult = (true, "ana.perez", "Pass123!", null)
        };
        var emailService = new FakeEmailService();
        var service = CreateService(registration, emailService);

        await service.RegisterUser("Ana", "Pérez", "ana@example.com", "Estudiante", 1);
        await emailService.WaitForSendAsync();

        Assert.Equal("ana@example.com", emailService.LastToEmail);
        Assert.True(emailService.LastIsHtml);
        Assert.Contains("ana.perez", emailService.LastBody);
        Assert.Contains("Pass123!", emailService.LastBody);
        Assert.Contains("Ana", emailService.LastBody);
    }

    // CP4 (callback, exception path): the email fails -> the caller still gets success and no exception escapes.
    [Fact]
    public async Task RegisterUser_WhenScheduledEmailFails_StillReturnsSuccess()
    {
        var registration = new FakeRegistration
        {
            RegisterResult = (true, "ana.perez", "Pass123!", null)
        };
        var emailService = new FakeEmailService
        {
            ThrowOnSend = new InvalidOperationException("SMTP no disponible")
        };
        var service = CreateService(registration, emailService);

        var result = await service.RegisterUser("Ana", "Pérez", "ana@example.com", "Estudiante", 1);

        Assert.True(result.ok);
        Assert.Equal("ana.perez", result.generatedUsername);
        Assert.Null(result.error);
        await emailService.WaitForSendAsync();
        Assert.Equal(1, emailService.SendCallCount);
    }

    private static RegistrationService CreateService(FakeRegistration registration, FakeEmailService emailService) =>
        new(registration, new EmailService(emailService));

    private sealed class FakeRegistration : IRegistration
    {
        public (bool ok, string? generatedUsername, string? generatedPassword, string? error) RegisterResult { get; init; }
        public int RegisterCallCount { get; private set; }

        public Task<(bool ok, string? generatedUsername, string? generatedPassword, string? error)> RegisterUser(
            string firstname, string lastName, string email, string role, int createdBy)
        {
            RegisterCallCount++;
            return Task.FromResult(RegisterResult);
        }

        public Task<string> GeneratePassword(int lenght) =>
            throw new NotSupportedException();
    }

    private sealed class FakeEmailService : IEmailService
    {
        private readonly TaskCompletionSource _sent = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public Exception? ThrowOnSend { get; init; }
        public int SendCallCount { get; private set; }
        public string? LastToEmail { get; private set; }
        public string? LastBody { get; private set; }
        public bool LastIsHtml { get; private set; }

        public Task<bool> SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)
        {
            SendCallCount++;
            LastToEmail = toEmail;
            LastBody = body;
            LastIsHtml = isHtml;
            _sent.TrySetResult();

            if (ThrowOnSend is not null)
                throw ThrowOnSend;

            return Task.FromResult(true);
        }

        public Task WaitForSendAsync() => _sent.Task.WaitAsync(CallbackTimeout);
    }
}
