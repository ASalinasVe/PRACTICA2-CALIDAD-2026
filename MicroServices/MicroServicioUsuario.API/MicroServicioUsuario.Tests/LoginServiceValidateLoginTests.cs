using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Interfaces;

namespace MicroServicioUsuario.Tests;

// Cases map to the control-flow graph of LoginService.ValidateLogin(string, string)
// (docs/testing/usuarios/control-flow-graphs.md, lines 24-28).
// The method has no decisions: V(G) = 1, a single path (CP1).
// CA2 is an additional data-level case on the same path.
// This tests the App-layer delegate; LoginValidateLoginTests covers the Inf-layer Login class.
public class LoginServiceValidateLoginTests
{
    private const string Username = "ana";
    private const string Password = "Clave123!";

    // CP1: ILogin succeeds -> the full tuple is returned unchanged and the arguments are forwarded once.
    [Fact]
    public async Task ValidateLogin_WhenSuccessful_ReturnsSameTupleAndForwardsArguments()
    {
        var login = new FakeLogin
        {
            ResultToReturn = (true, 42, "admin", null, false)
        };
        var service = new LoginService(login);

        var result = await service.ValidateLogin(Username, Password);

        Assert.True(result.ok);
        Assert.Equal(42, result.userId);
        Assert.Equal("admin", result.role);
        Assert.Null(result.error);
        Assert.False(result.isFirstLogin);
        Assert.Equal(1, login.CallCount);
        Assert.Equal(Username, login.ReceivedUsername);
        Assert.Equal(Password, login.ReceivedPassword);
    }

    // CA2: same path as CP1 with a rejected login -> the error and the null fields pass through unchanged.
    [Fact]
    public async Task ValidateLogin_WhenRejected_ReturnsSameError()
    {
        var login = new FakeLogin
        {
            ResultToReturn = (false, null, null, "Contraseña incorrecta.", false)
        };
        var service = new LoginService(login);

        var result = await service.ValidateLogin(Username, Password);

        Assert.False(result.ok);
        Assert.Null(result.userId);
        Assert.Null(result.role);
        Assert.Equal("Contraseña incorrecta.", result.error);
        Assert.False(result.isFirstLogin);
        Assert.Equal(1, login.CallCount);
    }

    private sealed class FakeLogin : ILogin
    {
        public (bool ok, int? userId, string? role, string? error, bool isFirstLogin) ResultToReturn { get; init; }

        public int CallCount { get; private set; }
        public string? ReceivedUsername { get; private set; }
        public string? ReceivedPassword { get; private set; }

        public Task<(bool ok, int? userId, string? role, string? error, bool isFirstLogin)> ValidateLogin(
            string username, string plainPassword)
        {
            CallCount++;
            ReceivedUsername = username;
            ReceivedPassword = plainPassword;

            return Task.FromResult(ResultToReturn);
        }

        public Task<(bool ok, string? error)> ChangePassword(int userId, string currentPassword, string newPassword) =>
            throw new NotSupportedException();

        public Task<(bool ok, string? error)> ChangePasswordFirstLogin(int userId, string currentPassword, string newPassword) =>
            throw new NotSupportedException();
    }
}
