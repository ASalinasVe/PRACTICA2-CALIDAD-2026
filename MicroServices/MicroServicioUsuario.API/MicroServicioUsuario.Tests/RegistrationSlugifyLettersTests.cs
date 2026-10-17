using System.Reflection;
using MicroServicoUser.Inf.Repository;

namespace MicroServicioUsuario.Tests;

public class RegistrationSlugifyLettersTests
{
    private static readonly MethodInfo SlugifyLettersMethod = FindSlugifyLettersMethod();

    [Fact]
    public void SlugifyLetters_WithEmptyText_ReturnsEmptyString()
    {
        Assert.Equal(string.Empty, InvokeSlugifyLetters(string.Empty));
    }

    [Fact]
    public void SlugifyLetters_WithNumbersAndSymbols_ReturnsEmptyString()
    {
        Assert.Equal(string.Empty, InvokeSlugifyLetters("123!"));
    }

    [Fact]
    public void SlugifyLetters_WithAccentedUppercaseText_ReturnsLowercaseAsciiLetters()
    {
        Assert.Equal("abc", InvokeSlugifyLetters("ÁBC"));
    }

    private static string InvokeSlugifyLetters(string text)
    {
        return Assert.IsType<string>(SlugifyLettersMethod.Invoke(null, new object?[] { text }));
    }

    private static MethodInfo FindSlugifyLettersMethod()
    {
        const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Static;
        var types = new[] { typeof(Registration) }
            .Concat(typeof(Registration).GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic));

        return types
            .SelectMany(type => type.GetMethods(flags))
            .Single(method => method.Name.Contains("SlugifyLetters", StringComparison.Ordinal));
    }
}
