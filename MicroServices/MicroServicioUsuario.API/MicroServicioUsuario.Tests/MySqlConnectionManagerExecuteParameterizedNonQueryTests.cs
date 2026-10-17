using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MicroServicoUser.Inf.Persistence.Database;

namespace MicroServicioUsuario.Tests;

public class MySqlConnectionManagerExecuteParameterizedNonQueryTests
{
    [Fact]
    public void ExecuteParameterizedNonQuery_WithExistingId_ReturnsAffectedRowCount()
    {
        const string query = "UPDATE Users SET Active = 0 WHERE Id = @Id";
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:MysqlUserServicioDB"] = "Server=localhost;Database=usuarios;User ID=test;Password=test;"
            })
            .Build();
        var connectionManager = new TestableMySqlConnectionManager(configuration);

        var affectedRows = connectionManager.ExecuteParameterizedNonQuery(
            query,
            new UpdateParameters { Id = 17 });

        Assert.Equal(query, connectionManager.ReceivedCommandText);
        Assert.Equal(1, connectionManager.ReceivedParameterCount);
        Assert.Equal("@Id", connectionManager.ReceivedParameterName);
        Assert.Equal(17, connectionManager.ReceivedParameterValue);
        Assert.Equal(1, affectedRows);
    }

    private sealed class TestableMySqlConnectionManager(IConfiguration configuration)
        : MySqlConnectionManager(configuration)
    {
        public string? ReceivedCommandText { get; private set; }
        public int ReceivedParameterCount { get; private set; }
        public string? ReceivedParameterName { get; private set; }
        public object? ReceivedParameterValue { get; private set; }

        protected override int ExecuteCommand(MySqlCommand command)
        {
            ReceivedCommandText = command.CommandText;
            ReceivedParameterCount = command.Parameters.Count;

            if (ReceivedParameterCount > 0)
            {
                var parameter = (MySqlParameter)command.Parameters[0];
                ReceivedParameterName = parameter.ParameterName;
                ReceivedParameterValue = parameter.Value;
            }

            return 1;
        }
    }

    private sealed class UpdateParameters
    {
        public int Id { get; set; }
    }
}
