using MicroServicioUser.Dom.Rules;

namespace MicroServicioUsuario.Tests;

public class PasswordValidatorTests
{
    [Fact]
    public void Validate_WhenPasswordIsWhitespace_ReturnsEmptyPasswordError()
    {
        var result = PasswordValidator.Validate(" ");

        Assert.False(result.ok);
        Assert.Equal("La contraseña no puede estar vacía.", result.error);
    }

    [Fact]
    public void Validate_WhenPasswordDoesNotMeetRequirements_ReturnsRequirementsError()
    {
        var result = PasswordValidator.Validate("abc123");

        Assert.False(result.ok);
        Assert.Equal(
            "La contraseña debe tener al menos 8 caracteres e incluir al menos una mayúscula, una minúscula, un número y un carácter especial.",
            result.error);
    }

    [Fact]
    public void Validate_WhenPasswordMeetsRequirements_ReturnsSuccess()
    {
        var result = PasswordValidator.Validate("Abcdef1!");

        Assert.True(result.ok);
        Assert.Null(result.error);
    }
}
