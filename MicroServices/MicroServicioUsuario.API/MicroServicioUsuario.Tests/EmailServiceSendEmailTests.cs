using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Interfaces;

namespace MicroServicioUsuario.Tests;

public class EmailServiceSendEmailTests
{
    private const string ToEmail = "ale.salinas@example.com";
    private const string Subject = "Prueba de correo";
    private const string Body = "<p>Contenido</p>";

    [Fact]
    public async Task SendEmailAsync_WhenHtmlIsOmitted_UsesTrueAndReturnsResult()
    {
        var emailService = new FakeEmailService
        {
            ResultToReturn = true
        };
        var service = new EmailService(emailService);

        var result = await service.SendEmailAsync(ToEmail, Subject, Body);

        Assert.True(result);
        AssertEmailCall(emailService, expectedIsHtml: true);
    }

    [Fact]
    public async Task SendEmailAsync_WhenHtmlIsFalse_ForwardsFalseAndReturnsResult()
    {
        var emailService = new FakeEmailService
        {
            ResultToReturn = false
        };
        var service = new EmailService(emailService);

        var result = await service.SendEmailAsync(
            ToEmail, Subject, Body, isHtml: false);

        Assert.False(result);
        AssertEmailCall(emailService, expectedIsHtml: false);
    }

    private static void AssertEmailCall(
        FakeEmailService emailService, bool expectedIsHtml)
    {
        Assert.Equal(1, emailService.CallCount);
        Assert.Equal(ToEmail, emailService.ReceivedToEmail);
        Assert.Equal(Subject, emailService.ReceivedSubject);
        Assert.Equal(Body, emailService.ReceivedBody);
        Assert.Equal(expectedIsHtml, emailService.ReceivedIsHtml);
    }

    private sealed class FakeEmailService : IEmailService
    {
        public bool ResultToReturn { get; init; }

        public int CallCount { get; private set; }
        public string? ReceivedToEmail { get; private set; }
        public string? ReceivedSubject { get; private set; }
        public string? ReceivedBody { get; private set; }
        public bool ReceivedIsHtml { get; private set; }

        public Task<bool> SendEmailAsync(
            string toEmail, string subject, string body, bool isHtml = true)
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