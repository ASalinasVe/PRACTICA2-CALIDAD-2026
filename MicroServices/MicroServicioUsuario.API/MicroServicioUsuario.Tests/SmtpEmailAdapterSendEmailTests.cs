using MicroServicoUser.Inf.EmailAdapters;

namespace MicroServicioUsuario.Tests;

// Cases map to the control-flow graph of SmtpEmailAdapter.SendEmailAsync(...)
// (docs/testing/usuarios/control-flow-graphs.md, section "SmtpEmailAdapter.SendEmailAsync").
// V(G) = 5. CP1 covers the empty-host path, CP2 the empty-user path and CP3/CP4 the exception path
// (with and without a FromEmail, so both outcomes of the ?? operator are exercised).
// The two success paths (send completes and returns true) need a real SMTP server with TLS,
// because EnableSsl is hard-coded and SmtpClient has no injectable transport. They are not covered here.
public class SmtpEmailAdapterSendEmailTests
{
    private const string ToEmail = "destino@example.com";
    private const string Subject = "Asunto de prueba";
    private const string Body = "<p>Contenido</p>";

    // CP1: host is blank -> returns false before building any message.
    [Fact]
    public async Task SendEmailAsync_WhenHostIsBlank_ReturnsFalse()
    {
        var adapter = CreateAdapter(new SmtpSettings
        {
            Host = "",
            User = "sistema@example.com",
            Password = "secreto",
            FromEmail = "sistema@example.com"
        });

        var result = await adapter.SendEmailAsync(ToEmail, Subject, Body);

        Assert.False(result);
    }

    // CP2: host is set but user is blank -> returns false before building any message.
    [Fact]
    public async Task SendEmailAsync_WhenUserIsBlank_ReturnsFalse()
    {
        var adapter = CreateAdapter(new SmtpSettings
        {
            Host = "127.0.0.1",
            Port = 1,
            User = "   ",
            Password = "secreto",
            FromEmail = "sistema@example.com"
        });

        var result = await adapter.SendEmailAsync(ToEmail, Subject, Body);

        Assert.False(result);
    }

    // CP3: configuration is complete and FromEmail is set; the connection is refused and the exception
    // is caught -> returns false. Exercises the non-null outcome of fromEmail ?? smtpUser.
    [Fact]
    public async Task SendEmailAsync_WhenConnectionFailsWithFromEmail_ReturnsFalse()
    {
        var adapter = CreateAdapter(new SmtpSettings
        {
            Host = "127.0.0.1",
            Port = 1,
            User = "sistema@example.com",
            Password = "secreto",
            FromEmail = "remitente@example.com"
        });

        var result = await adapter.SendEmailAsync(ToEmail, Subject, Body);

        Assert.False(result);
    }

    // CP4: same exception path as CP3 with FromEmail = null -> the sender falls back to User and the
    // connection failure is still caught. Exercises the null outcome of fromEmail ?? smtpUser.
    [Fact]
    public async Task SendEmailAsync_WhenConnectionFailsWithoutFromEmail_ReturnsFalse()
    {
        var adapter = CreateAdapter(new SmtpSettings
        {
            Host = "127.0.0.1",
            Port = 1,
            User = "sistema@example.com",
            Password = "secreto",
            FromEmail = null!
        });

        var result = await adapter.SendEmailAsync(ToEmail, Subject, Body);

        Assert.False(result);
    }

    private static SmtpEmailAdapter CreateAdapter(SmtpSettings settings) => new(settings);
}
