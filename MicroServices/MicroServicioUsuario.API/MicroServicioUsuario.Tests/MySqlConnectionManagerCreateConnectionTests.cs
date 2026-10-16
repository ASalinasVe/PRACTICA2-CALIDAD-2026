using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection;
using MicroServicoUser.Inf.Persistence.Database;

namespace MicroServicioUsuario.Tests;

public class MySqlConnectionManagerCreateConnectionTests
{
    [Fact]
    public void CreateConnection_WithConfiguredConnectionString_ReturnsClosedMySqlConnection()
    {
        const string connectionString = "Server=localhost;Database=usuarios;User ID=test;Password=test;";
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:MysqlUserServicioDB"] = connectionString
            })
            .Build();
        var connectionManager = new MySqlConnectionManager(configuration);
        var createConnection = typeof(MySqlConnectionManager).GetMethod(
            "CreateConnection",
            BindingFlags.Instance | BindingFlags.NonPublic);

        Assert.NotNull(createConnection);

        using var connection = Assert.IsType<MySqlConnection>(createConnection.Invoke(connectionManager, null));

        var configuredConnection = new MySqlConnectionStringBuilder(connection.ConnectionString);
        Assert.Equal("localhost", configuredConnection.Server);
        Assert.Equal("usuarios", configuredConnection.Database);
        Assert.Equal("test", configuredConnection.UserID);
        Assert.Equal("test", configuredConnection.Password);
        Assert.Equal(ConnectionState.Closed, connection.State);
    }
}
