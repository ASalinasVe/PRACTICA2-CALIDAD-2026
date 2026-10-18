using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Interfaces;

namespace MicroServicioUsuario.Tests;

public class LoginServiceChangePasswordFirstLoginTests
{
    private const int UserId = 10;
    private const string CurrentPassword = "Actual123!";
    private const string NewPassword = "Nueva456!";

    [Fact]
    public async Task ChangePasswordFirstLogin_WhenSuccessful_ReturnsSuccess()
    {
        var login = new FakeLogin
        {
            ResultToReturn = (true, null)
        };
        var service = new LoginService(login);

        var result = await service.ChangePasswordFirstLogin(
            UserId, CurrentPassword, NewPassword);

        Assert.True(result.ok);
        Assert.Null(result.error);
        AssertLoginCall(login);
    }

    [Fact]
    public async Task ChangePasswordFirstLogin_WhenItFails_ReturnsError()
    {
        var login = new FakeLogin
        {
            ResultToReturn = (false, "Contraseña actual incorrecta")
        };
        var service = new LoginService(login);

        var result = await service.ChangePasswordFirstLogin(
            UserId, CurrentPassword, NewPassword);

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

        public Task<(bool ok, string? error)> ChangePasswordFirstLogin(
            int userId, string currentPassword, string newPassword)
        {
            CallCount++;
            ReceivedUserId = userId;
            ReceivedCurrentPassword = currentPassword;
            ReceivedNewPassword = newPassword;

            return Task.FromResult(ResultToReturn);
        }

        public Task<(bool ok, string? error)> ChangePassword(
            int userId, string currentPassword, string newPassword) =>
            throw new NotSupportedException();

        public Task<(bool ok, int? userId, string? role, string? error, bool isFirstLogin)>
            ValidateLogin(string username, string plainPassword) =>
            throw new NotSupportedException();
    }
}