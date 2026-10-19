using MicroServicioUser.App.Services;
using MicroServicioUser.Dom.Dto;
using MicroServicioUser.Dom.Interfaces;
using MicroServicioUser.Dom.Patterns;
using MicroServicioUsuario.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroServicioUsuario.Tests.Controllers;

public class UserControllerLoginTests
{
    [Fact]
    public async Task Login_WhenCredentialsAreValid_ReturnsToken()
    {
        var login = new FakeLogin((true, 10, "Admin", null, false));
        var jwt = new FakeJwtService(Result<string>.Success("jwt-prueba"));
        var controller = CreateController(login, jwt);

        var result = await controller.Login(new LoginDTO
        {
            Username = "ale.salinas",
            Password = "Actual123!"
        });

        var httpResponse = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, httpResponse.StatusCode ?? 0);

        var response = Assert.IsType<LoginResponse>(httpResponse.Value);
        Assert.True(response.Ok);
        Assert.Equal("", response.Error);
        Assert.Equal("jwt-prueba", response.Token);
        Assert.Equal("Bearer", response.TokenType);

        Assert.Equal(1, login.CallCount);
        Assert.Equal("ale.salinas", login.LastUsername);
        Assert.Equal("Actual123!", login.LastPassword);

        Assert.Equal(1, jwt.CallCount);
        Assert.Equal(10, jwt.LastUserId);
        Assert.Equal("Admin", jwt.LastRole);
    }

    [Fact]
    public async Task Login_WhenCredentialsAreInvalid_ReturnsErrorWithoutGeneratingToken()
    {
        var login = new FakeLogin(
            (false, null, null, "Credenciales inválidas", false));
        var jwt = new FakeJwtService(Result<string>.Success("token-no-utilizado"));
        var controller = CreateController(login, jwt);

        var result = await controller.Login(new LoginDTO
        {
            Username = "ale.salinas",
            Password = "Actual123!"
        });

        var httpResponse = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, httpResponse.StatusCode ?? 0);

        var response = Assert.IsType<LoginResponse>(httpResponse.Value);
        Assert.False(response.Ok);
        Assert.Equal("Credenciales inválidas", response.Error);
        Assert.Null(response.Token);
        Assert.Null(response.TokenType);

        Assert.Equal(1, login.CallCount);
        Assert.Equal("ale.salinas", login.LastUsername);
        Assert.Equal("Actual123!", login.LastPassword);
        Assert.Equal(0, jwt.CallCount);
    }

    private static UserController CreateController(
        FakeLogin login,
        FakeJwtService jwt) =>
        new(
            service: null!,
            loginService: new LoginService(login),
            registrationService: null!,
            jwtService: jwt);

    private sealed class FakeLogin : ILogin
    {
        private readonly (
            bool ok,
            int? userId,
            string? role,
            string? error,
            bool isFirstLogin) _result;

        public int CallCount { get; private set; }
        public string? LastUsername { get; private set; }
        public string? LastPassword { get; private set; }

        public FakeLogin((
            bool ok,
            int? userId,
            string? role,
            string? error,
            bool isFirstLogin) result)
        {
            _result = result;
        }

        public Task<(
            bool ok,
            int? userId,
            string? role,
            string? error,
            bool isFirstLogin)> ValidateLogin(
                string username,
                string plainPassword)
        {
            CallCount++;
            LastUsername = username;
            LastPassword = plainPassword;
            return Task.FromResult(_result);
        }

        public Task<(bool ok, string? error)> ChangePassword(
            int userId,
            string currentPassword,
            string newPassword) =>
            throw new NotSupportedException();

        public Task<(bool ok, string? error)> ChangePasswordFirstLogin(
            int userId,
            string currentPassword,
            string newPassword) =>
            throw new NotSupportedException();
    }

    private sealed class FakeJwtService : IJwtService
    {
        private readonly Result<string> _result;

        public int CallCount { get; private set; }
        public int LastUserId { get; private set; }
        public string? LastRole { get; private set; }

        public FakeJwtService(Result<string> result)
        {
            _result = result;
        }

        public Task<Result<string>> GenerateToken(int userId, string role)
        {
            CallCount++;
            LastUserId = userId;
            LastRole = role;
            return Task.FromResult(_result);
        }
    }
}