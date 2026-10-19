using MicroServicioUser.Dom.Patterns;

namespace MicroServicioUsuario.Tests;

public class ResultSuccessFactoryTests
{
    [Fact]
    public void Success_ReturnsSuccessWithNoErrors()
    {
        var result = Result.Success();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Success_ReturnsNewInstanceOnEachCall()
    {
        var first = Result.Success();
        var second = Result.Success();

        Assert.NotSame(first, second);
    }

    [Fact]
    public void Success_DoesNotShareErrorListBetweenResults()
    {
        var first = Result.Success();
        var second = Result.Success();

        Assert.NotSame(first.Errors, second.Errors);
    }
}
