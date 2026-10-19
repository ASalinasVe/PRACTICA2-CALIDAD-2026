using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Interfaces;

namespace MicroServicioUsuario.Tests;

// Cases map to the control-flow graph of EmailService.SendNewUserCredentialsAsync(...)
// (docs/testing/usuarios/control-flow-graphs.md, lines 24-88).
// The method has no decisions: V(G) = 1, a single path (CP1).
// CA2 is an additional data-level case on the same path.
public class EmailServiceSendNewUserCredentialsTests
{
    private const string ToEmail = "ale.salinas@example.com";
    private const string UserName = "ale.salinas";
    private const string FirstName = "Alejandra";
    private const string Password = "Pass123!";
    private const string ExpectedSubject = "Bienvenido al Sistema de Pagos de Aranceles";

    // CP1: the email is composed and delegated once, as HTML, and the delegate's result is returned.
    [Fact]
    public async Task SendNewUserCredentialsAsync_WhenDelegateSucceeds_SendsHtmlWithCredentialsAndReturnsTrue()
    {
        var emailService = new FakeEmailService
        {
            ResultToReturn = true
        };
        var service = new EmailService(emailService);

        var result = await service.SendNewUserCredentialsAsync(ToEmail, UserName, FirstName, Password);

        Assert.True(result);
        Assert.Equal(1, emailService.CallCount);
        Assert.Equal(ToEmail, emailService.ReceivedToEmail);
        Assert.Equal(ExpectedSubject, emailService.ReceivedSubject);
        Assert.True(emailService.ReceivedIsHtml);
        Assert.Contains(FirstName, emailService.ReceivedBody);
        Assert.Contains(UserName, emailService.ReceivedBody);
        Assert.Contains(Password, emailService.ReceivedBody);
    }

    // CA2: same path as CP1 with a failing delegate -> the false result is returned unchanged.
    [Fact]
    public async Task SendNewUserCredentialsAsync_WhenDelegateFails_ReturnsFalse()
    {
        var emailService = new FakeEmailService
        {
            ResultToReturn = false
        };
        var service = new EmailService(emailService);

        var result = await service.SendNewUserCredentialsAsync(ToEmail, UserName, FirstName, Password);

        Assert.False(result);
        Assert.Equal(1, emailService.CallCount);
        Assert.True(emailService.ReceivedIsHtml);
    }

    private sealed class FakeEmailService : IEmailService
    {
        public bool ResultToReturn { get; init; }

        public int CallCount { get; private set; }
        public string? ReceivedToEmail { get; private set; }
        public string? ReceivedSubject { get; private set; }
        public string ReceivedBody { get; private set; } = string.Empty;
        public bool ReceivedIsHtml { get; private set; }

        public Task<bool> SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)
        {
            CallCount++;
            ReceivedToEmail = toEmail;
            ReceivedSubject = subject;
            ReceivedBody = body;
            ReceivedIsHtml = isHtml;

            return Task.FromResult(ResultToReturn);
        }
    }
}
