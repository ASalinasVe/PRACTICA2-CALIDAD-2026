using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Interfaces;

namespace MicroServicioUsuario.Tests;

// Cases map to the control-flow graph of LoginService.ChangePassword(int, string, string)
// (docs/testing/usuarios/control-flow-graphs.md, lines 13-16).
// The method has no decisions: V(G) = 1, a single path (CP1).
// CA2 is an additional data-level case on the same path.
public class LoginServiceChangePasswordTests
{
    private const int UserId = 10;
    private const string CurrentPassword = "Actual123!";
    private const string NewPassword = "Nueva456!";

    // CP1: ILogin succeeds -> the result is returned unchanged and the arguments are forwarded once.
    [Fact]
    public async Task ChangePassword_WhenSuccessful_ReturnsSuccessAndForwardsArguments()
    {
        var login = new FakeLogin
        {
            ResultToReturn = (true, null)
        };
        var service = new LoginService(login);

        var result = await service.ChangePassword(UserId, CurrentPassword, NewPassword);

        Assert.True(result.ok);
        Assert.Null(result.error);
        AssertLoginCall(login);
    }

    // CA2: same path as CP1 with a failing ILogin -> the error message passes through unchanged.
    [Fact]
    public async Task ChangePassword_WhenItFails_ReturnsSameError()
    {
        var login = new FakeLogin
        {
            ResultToReturn = (false, "Contraseña actual incorrecta")
        };
        var service = new LoginService(login);

        var result = await service.ChangePassword(UserId, CurrentPassword, NewPassword);

        Assert.False(result.ok);
        Assert.Equal("Contraseña actual incorrecta", result.error);
        AssertLoginCall(login);
    }

    private static void AssertLoginCall(FakeLogin login)
    {
        Assert.Equal(1, login.CallCount);
        Assert.Equal(UserId, login.ReceivedUserId);
        Assert.Equal(CurrentPassword, login.ReceivedCurrentPassword);
        Assert.Equal(NewPassword, login.ReceivedNewPassword);
    }

    private sealed class FakeLogin : ILogin
    {
        public (bool ok, string? error) ResultToReturn { get; init; }

        public int CallCount { get; private set; }
        public int ReceivedUserId { get; private set; }
        public string? ReceivedCurrentPassword { get; private set; }
        public string? ReceivedNewPassword { get; private set; }

        public Task<(bool ok, string? error)> ChangePassword(
            int userId, string currentPassword, string newPassword)
        {
            CallCount++;
            ReceivedUserId = userId;
            ReceivedCurrentPassword = currentPassword;
            ReceivedNewPassword = newPassword;

            return Task.FromResult(ResultToReturn);
        }

        public Task<(bool ok, string? error)> ChangePasswordFirstLogin(
            int userId, string currentPassword, string newPassword) =>
            throw new NotSupportedException();

        public Task<(bool ok, int? userId, string? role, string? error, bool isFirstLogin)>
            ValidateLogin(string username, string plainPassword) =>
            throw new NotSupportedException();
    }
}
