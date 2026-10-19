using MicroServicioUser.Dom.Patterns;

namespace MicroServicioUsuario.Tests;

public class ResultGenericFailureFactoryTests
{
    [Fact]
    public void Failure_WithOneError_ReturnsFailureWithThatErrorAndDefaultValue()
    {
        var result = Result<string>.Failure("Error de conexión");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Null(result.Value);
        Assert.Equal("Error de conexión", Assert.Single(result.Errors));
    }

    [Fact]
    public void Failure_WithMultipleErrors_ReturnsThemInOrder()
    {
        var result = Result<string>.Failure(
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
        var result = Result<string>.Failure();

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Failure_WithValueType_ReturnsDefaultValueOfT()
    {
        var result = Result<int>.Failure("Error de validación");

        Assert.False(result.IsSuccess);
        Assert.Equal(0, result.Value);
    }

    [Fact]
    public void Failure_CopiesErrorsFromInputArray()
    {
        var errors = new[] { "Error original" };

        var result = Result<int>.Failure(errors);
        errors[0] = "Error modificado";

        Assert.Equal("Error original", Assert.Single(result.Errors));
    }
}
