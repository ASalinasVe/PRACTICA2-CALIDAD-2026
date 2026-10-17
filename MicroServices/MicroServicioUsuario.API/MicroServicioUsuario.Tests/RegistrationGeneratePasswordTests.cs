using MicroServicoUser.Inf.Repository;

namespace MicroServicioUsuario.Tests;

public class RegistrationGeneratePasswordTests
{
    private const string AllowedPasswordCharacters = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789!@#$%";

    [Fact]
    public async Task GeneratePassword_WithZeroLength_ReturnsEmptyString()
    {
        var registration = new Registration(null!);

        var password = await registration.GeneratePassword(0);

        Assert.Empty(password);
    }

    [Fact]
    public async Task GeneratePassword_WithLengthOne_ReturnsCharacterFromAllowedAlphabet()
    {
        var registration = new Registration(null!);

        var password = await registration.GeneratePassword(1);

        Assert.Equal(1, password.Length);
        Assert.Contains(password[0].ToString(), AllowedPasswordCharacters);
    }
}
