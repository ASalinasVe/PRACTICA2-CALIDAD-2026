using MicroServicioUser.Dom.Patterns;

namespace MicroServicioUsuario.Tests;

public class ResultFailureFactoryTests
{
    [Fact]
    public void Failure_WithOneError_ReturnsFailureWithThatError()
    {
        var result = Result.Failure("Error de conexión");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal("Error de conexión", Assert.Single(result.Errors));
    }

    [Fact]
    public void Failure_WithMultipleErrors_ReturnsThemInOrder()
    {
        var result = Result.Failure(
            "Error de conexión",
            "Tiempo de espera agotado");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(
            new[] { "Error de conexión", "Tiempo de espera agotado" },
            result.Errors);
    }

    [Fact]
    public void Failure_WithNoErrors_ReturnsEmptyErrorList()
    {
        var result = Result.Failure();

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Empty(result.Errors);
    }
}