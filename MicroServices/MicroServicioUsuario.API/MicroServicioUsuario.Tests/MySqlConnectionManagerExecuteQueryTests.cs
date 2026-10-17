using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using MicroServicoUser.Inf.Persistence.Database;

namespace MicroServicioUsuario.Tests;

public class MySqlConnectionManagerExecuteQueryTests
{
    [Fact]
    public void ExecuteQuery_WithValidQuery_ReturnsMappedRows()
    {
        const string query = "SELECT Id, Name FROM Users";
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:MysqlUserServicioDB"] = "Server=localhost;Database=usuarios;User ID=test;Password=test;"
            })
            .Build();
        var connectionManager = new TestableMySqlConnectionManager(configuration);

        var result = Assert.Single(connectionManager.ExecuteQuery<QueryResult>(query));

        Assert.Equal(query, connectionManager.ReceivedCommandText);
        Assert.Equal(0, connectionManager.ReceivedParameterCount);
        Assert.Equal(17, result.Id);
        Assert.Equal("Ana", result.Name);
    }

    private sealed class TestableMySqlConnectionManager(IConfiguration configuration)
        : MySqlConnectionManager(configuration)
    {
        public string? ReceivedCommandText { get; private set; }
        public int ReceivedParameterCount { get; private set; }

        protected override IEnumerable<T> ExecuteCommand<T>(MySqlCommand command)
        {
            ReceivedCommandText = command.CommandText;
            ReceivedParameterCount = command.Parameters.Count;

            var dataTable = new DataTable();
            dataTable.Columns.Add(nameof(QueryResult.Id), typeof(int));
            dataTable.Columns.Add(nameof(QueryResult.Name), typeof(string));
            dataTable.Rows.Add(17, "Ana");

            return DbMapper.MapDataTableToModelIterable<T>(dataTable);
        }
    }

    private sealed class QueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
