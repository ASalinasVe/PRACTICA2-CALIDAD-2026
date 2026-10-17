using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using MicroServicoUser.Inf.Persistence.Database;

namespace MicroServicioUsuario.Tests;

public class MySqlConnectionManagerExecuteParameterizedQueryWithSeparateTypesTests
{
    [Fact]
    public void ExecuteParameterizedQuery_WithSeparateParameterAndResultTypes_ReturnsMappedRow()
    {
        const string query = "SELECT Id, Name FROM Users WHERE Id = @Id";
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:MysqlUserServicioDB"] = "Server=localhost;Database=usuarios;User ID=test;Password=test;"
            })
            .Build();
        var connectionManager = new TestableMySqlConnectionManager(configuration);

        var result = Assert.Single(connectionManager.ExecuteParameterizedQuery<UserRow, QueryParameters>(
            query,
            new QueryParameters { Id = 17 }));

        Assert.Equal(query, connectionManager.ReceivedCommandText);
        Assert.Equal(1, connectionManager.ReceivedParameterCount);
        Assert.Equal("@Id", connectionManager.ReceivedParameterName);
        Assert.Equal(17, connectionManager.ReceivedParameterValue);
        Assert.Equal(17, result.Id);
        Assert.Equal("Ana", result.Name);
    }

    private sealed class TestableMySqlConnectionManager(IConfiguration configuration)
        : MySqlConnectionManager(configuration)
    {
        public string? ReceivedCommandText { get; private set; }
        public int ReceivedParameterCount { get; private set; }
        public string? ReceivedParameterName { get; private set; }
        public object? ReceivedParameterValue { get; private set; }

        protected override DataTable LoadDataTable(MySqlCommand command)
        {
            ReceivedCommandText = command.CommandText;
            ReceivedParameterCount = command.Parameters.Count;

            if (ReceivedParameterCount > 0)
            {
                var parameter = (MySqlParameter)command.Parameters[0];
                ReceivedParameterName = parameter.ParameterName;
                ReceivedParameterValue = parameter.Value;
            }

            var dataTable = new DataTable();
            dataTable.Columns.Add(nameof(UserRow.Id), typeof(int));
            dataTable.Columns.Add(nameof(UserRow.Name), typeof(string));
            dataTable.Rows.Add(17, "Ana");

            return dataTable;
        }
    }

    private sealed class QueryParameters
    {
        public int Id { get; set; }
    }

    private sealed class UserRow
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
