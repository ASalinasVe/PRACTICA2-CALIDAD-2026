using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MicroServicoUser.Inf.Persistence.Database;

namespace MicroServicioUsuario.Tests;

public class MySqlConnectionManagerExecuteNonQueryTests
{
    [Fact]
    public void ExecuteNonQuery_WithUpdateQuery_ReturnsAffectedRowCount()
    {
        const string query = "UPDATE Users SET Name = 'Ana' WHERE Id = 17";
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:MysqlUserServicioDB"] = "Server=localhost;Database=usuarios;User ID=test;Password=test;"
            })
            .Build();
        var connectionManager = new TestableMySqlConnectionManager(configuration);

        var affectedRows = connectionManager.ExecuteNonQuery(query);

        Assert.Equal(query, connectionManager.ReceivedCommandText);
        Assert.Equal(0, connectionManager.ReceivedParameterCount);
        Assert.Equal(1, affectedRows);
    }

    private sealed class TestableMySqlConnectionManager(IConfiguration configuration)
        : MySqlConnectionManager(configuration)
    {
        public string? ReceivedCommandText { get; private set; }
        public int ReceivedParameterCount { get; private set; }

        protected override int ExecuteCommand(MySqlCommand command)
        {
            ReceivedCommandText = command.CommandText;
            ReceivedParameterCount = command.Parameters.Count;
            return 1;
        }
    }
}
