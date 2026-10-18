using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUsuario.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroServicioUsuario.Tests.Controllers;

public class UserControllerChangePasswordFirstLoginTests
{
    [Theory]
    [InlineData(true, null)]
    [InlineData(false, "Contraseña actual incorrecta")]
    public async Task ChangePasswordFirstLogin_ReturnsTheLoginResult(
        bool expectedOk,
        string? expectedError)
    {
        const int userId = 10;
        const string currentPassword = "Actual123!";
        const string newPassword = "Nueva456!";

        var fakeLogin = new FakeLogin(expectedOk, expectedError);

        var controller = new UserController(
            service: null!,
            loginService: new LoginService(fakeLogin),
            registrationService: null!,
            jwtService: null!);

        var result = await controller.ChangePasswordFirstLogin(
            new ChangePasswordDTO
            {
                UserId = userId,
                CurrentPassword = currentPassword,
                NewPassword = newPassword
            });

        var response = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode ?? 0);

        var payload = response.Value!;
        var payloadType = payload.GetType();

        Assert.Equal(
            expectedOk,
            (bool)payloadType.GetProperty("Ok")!.GetValue(payload)!);

        Assert.Equal(
            expectedError,
            (string?)payloadType.GetProperty("Error")!.GetValue(payload));

        Assert.Equal(1, fakeLogin.CallCount);
        Assert.Equal(userId, fakeLogin.LastUserId);
        Assert.Equal(currentPassword, fakeLogin.LastCurrentPassword);
        Assert.Equal(newPassword, fakeLogin.LastNewPassword);
    }

    private sealed class FakeLogin : ILogin
    {
        private readonly bool _ok;
        private readonly string? _error;

        public int CallCount { get; private set; }
        public int LastUserId { get; private set; }
        public string? LastCurrentPassword { get; private set; }
        public string? LastNewPassword { get; private set; }

        public FakeLogin(bool ok, string? error)
        {
            _ok = ok;
            _error = error;
        }

        public Task<(bool ok, string? error)> ChangePasswordFirstLogin(
            int userId,
            string currentPassword,
            string newPassword)
        {
            CallCount++;
            LastUserId = userId;
            LastCurrentPassword = currentPassword;
            LastNewPassword = newPassword;

            return Task.FromResult<(bool ok, string? error)>((_ok, _error));
        }

        public Task<(bool ok, string? error)> ChangePassword(
            int userId,
            string currentPassword,
            string newPassword) =>
            throw new NotSupportedException();

        public Task<(bool ok, int? userId, string? role, string? error, bool isFirstLogin)>
            ValidateLogin(string username, string plainPassword) =>
            throw new NotSupportedException();
    }
}