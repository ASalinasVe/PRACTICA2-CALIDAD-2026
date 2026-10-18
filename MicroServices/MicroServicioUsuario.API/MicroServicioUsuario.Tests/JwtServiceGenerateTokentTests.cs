using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MicroServicioUser.App.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MicroServicioUsuario.Tests;

public class JwtServiceGenerateTokenTests
{
    private const string Key =
        "ClaveSeguraDePruebaParaJwt2026_1234567890";
    private const string Issuer = "https://test.local";
    private const string Audience = "usuarios-test";

    [Fact]
    public async Task GenerateToken_ReturnsValidSignedTokenWithExpectedClaims()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Key"] = Key,
                ["Jwt:Issuer"] = Issuer,
                ["Jwt:Audience"] = Audience,
                ["Jwt:ExpireMinutes"] = "30"
            })
            .Build();

        var service = new JwtService(configuration);
        var before = DateTime.UtcNow;

        var result = await service.GenerateToken(10, "Admin");

        var after = DateTime.UtcNow;

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.False(string.IsNullOrWhiteSpace(result.Value));

        var handler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Issuer,
            ValidateAudience = true,
            ValidAudience = Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Key)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1)
        };

        var principal = handler.ValidateToken(
            result.Value, validationParameters, out var validatedToken);

        var jwt = Assert.IsType<JwtSecurityToken>(validatedToken);

        Assert.Contains(principal.Claims, claim =>
            claim.Value == "10" &&
            (claim.Type == ClaimTypes.NameIdentifier ||
             claim.Type == JwtRegisteredClaimNames.NameId));

        Assert.Contains(principal.Claims, claim =>
            claim.Value == "Admin" &&
            (claim.Type == ClaimTypes.Role || claim.Type == "role"));

        Assert.False(string.IsNullOrWhiteSpace(jwt.Id));
        Assert.Equal(Issuer, jwt.Issuer);
        Assert.Contains(Audience, jwt.Audiences);
        Assert.InRange(
            jwt.ValidTo,
            before.AddMinutes(29),
            after.AddMinutes(31));
    }
}