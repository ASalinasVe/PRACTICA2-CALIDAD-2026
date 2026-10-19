using System.Reflection;
using MicroServicoUser.Inf.Repository;

namespace MicroServicioUsuario.Tests;

public class RegistrationRemoveDiacriticsTests
{
    private static readonly MethodInfo RemoveDiacriticsMethod = FindRemoveDiacriticsMethod();

    [Fact]
    public void RemoveDiacritics_WithWhitespace_ReturnsEmptyString()
    {
        Assert.Equal(string.Empty, InvokeRemoveDiacritics(" "));
    }

    [Fact]
    public void RemoveDiacritics_WithUnaccentedCharacter_PreservesCharacter()
    {
        Assert.Equal("a", InvokeRemoveDiacritics("a"));
    }

    [Fact]
    public void RemoveDiacritics_WithAccentedCharacter_RemovesDiacritic()
    {
        Assert.Equal("a", InvokeRemoveDiacritics("á"));
    }

    [Fact]
    public void RemoveDiacritics_WithStandaloneCombiningMark_RemovesCharacter()
    {
        Assert.Equal(string.Empty, InvokeRemoveDiacritics("\u0301"));
    }

    private static string InvokeRemoveDiacritics(string text)
    {
        return Assert.IsType<string>(RemoveDiacriticsMethod.Invoke(null, new object?[] { text }));
    }

    private static MethodInfo FindRemoveDiacriticsMethod()
    {
        const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Static;
        var types = new[] { typeof(Registration) }
            .Concat(typeof(Registration).GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic));

        return types
            .SelectMany(type => type.GetMethods(flags))
            .Single(method => method.Name.Contains("RemoveDiacritics", StringComparison.Ordinal));
    }
}
