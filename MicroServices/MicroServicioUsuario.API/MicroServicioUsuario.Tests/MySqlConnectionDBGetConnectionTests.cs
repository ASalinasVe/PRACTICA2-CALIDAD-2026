using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using MicroServicoUser.Inf.Persistence;

namespace MicroServicioUsuario.Tests;

public class MySqlConnectionDBGetConnectionTests
{
    [Fact]
    public void GetConnection_WithConfiguredConnectionString_ReturnsClosedMySqlConnection()
    {
        const string connectionString = "Server=localhost;Database=usuarios;User ID=test;Password=test;";
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:MysqlUserServicioDB"] = connectionString
            })
            .Build();
        var connectionDb = new MySqlConnectionDB(configuration);

        using var connection = Assert.IsType<MySqlConnection>(connectionDb.GetConnection());

        var configuredConnection = new MySqlConnectionStringBuilder(connection.ConnectionString);
        Assert.Equal("localhost", configuredConnection.Server);
        Assert.Equal("usuarios", configuredConnection.Database);
        Assert.Equal("test", configuredConnection.UserID);
        Assert.Equal("test", configuredConnection.Password);
        Assert.Equal(ConnectionState.Closed, connection.State);
    }
}
