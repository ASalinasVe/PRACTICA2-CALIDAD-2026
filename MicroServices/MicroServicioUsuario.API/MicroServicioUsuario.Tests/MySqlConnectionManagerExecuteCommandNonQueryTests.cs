using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MicroServicoUser.Inf.Persistence.Database;

namespace MicroServicioUsuario.Tests;

public class MySqlConnectionManagerExecuteCommandNonQueryTests
{
    [Fact]
    public void ExecuteCommand_WithParameterizedUpdate_ReturnsAffectedRowCount()
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

        Assert.Equal(1, affectedRows);
        Assert.True(connectionManager.OpenConnectionWasCalled);
        Assert.True(connectionManager.CommandExecutionWasCalled);
        Assert.True(connectionManager.ConnectionWasAssignedBeforeExecution);
        Assert.Equal(query, connectionManager.ReceivedCommandText);
        Assert.Equal(1, connectionManager.ReceivedParameterCount);
        Assert.Equal("@Id", connectionManager.ReceivedParameterName);
        Assert.Equal(17, connectionManager.ReceivedParameterValue);
    }

    private sealed class TestableMySqlConnectionManager(IConfiguration configuration)
        : MySqlConnectionManager(configuration)
    {
        private MySqlConnection? _openedConnection;

        public bool OpenConnectionWasCalled { get; private set; }
        public bool CommandExecutionWasCalled { get; private set; }
        public bool ConnectionWasAssignedBeforeExecution { get; private set; }
        public string? ReceivedCommandText { get; private set; }
        public int ReceivedParameterCount { get; private set; }
        public string? ReceivedParameterName { get; private set; }
        public object? ReceivedParameterValue { get; private set; }

        protected override void OpenConnection(MySqlConnection connection)
        {
            _openedConnection = connection;
            OpenConnectionWasCalled = true;
        }

        protected override int ExecuteMySqlCommand(MySqlCommand command)
        {
            CommandExecutionWasCalled = true;
            ConnectionWasAssignedBeforeExecution = ReferenceEquals(command.Connection, _openedConnection);
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
