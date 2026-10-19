using MicroServicoUser.Inf.Repository;

namespace MicroServicioUsuario.Tests;

public class RegistrationHelpersMd5HexTests
{
    [Fact]
    public void Md5Hex_WithEmptyInput_ReturnsKnownMd5Digest()
    {
        var hash = RegistrationHelpers.Md5Hex(string.Empty);

        Assert.Equal("d41d8cd98f00b204e9800998ecf8427e", hash);
    }

    [Fact]
    public void Md5Hex_WithAbcInput_ReturnsKnownMd5Digest()
    {
        var hash = RegistrationHelpers.Md5Hex("abc");

        Assert.Equal("900150983cd24fb0d6963f7d28e17f72", hash);
    }
}
