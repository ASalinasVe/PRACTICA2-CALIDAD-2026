using MicroServicioUser.Dom.Patterns;

namespace MicroServicioUsuario.Tests;

public class ResultGenericSuccessFactoryTests
{
    [Fact]
    public void Success_WithValue_ReturnsSuccessWithThatValueAndNoErrors()
    {
        var result = Result<string>.Success("Usuario creado");

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal("Usuario creado", result.Value);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Success_WithValueType_ReturnsThatValue()
    {
        var result = Result<int>.Success(42);

        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void Success_WithNullReferenceValue_ReturnsSuccessWithNullValue()
    {
        var result = Result<string?>.Success(null);

        Assert.True(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Success_KeepsSameInstanceOfReferenceValue()
    {
        var value = new List<string> { "Ana" };

        var result = Result<List<string>>.Success(value);

        Assert.Same(value, result.Value);
    }
}
