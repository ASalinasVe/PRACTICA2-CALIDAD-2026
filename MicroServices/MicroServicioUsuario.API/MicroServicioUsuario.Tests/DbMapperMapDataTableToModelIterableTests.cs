using System.Data;
using MicroServicoUser.Inf.Persistence.Database;

namespace MicroServicioUsuario.Tests;

public class DbMapperMapDataTableToModelIterableTests
{
    [Fact]
    public void MapDataTableToModelIterable_WhenTableHasNoRows_ReturnsEmptySequence()
    {
        var dataTable = CreateDataTable();

        var result = DbMapper.MapDataTableToModelIterable<TestModel>(dataTable);

        Assert.Empty(result);
    }

    [Fact]
    public void MapDataTableToModelIterable_WhenTableHasOneRow_ReturnsMappedModel()
    {
        var dataTable = CreateDataTable();
        dataTable.Rows.Add(17, "Ana");

        var result = Assert.Single(DbMapper.MapDataTableToModelIterable<TestModel>(dataTable));

        Assert.Equal(17, result.Id);
        Assert.Equal("Ana", result.Name);
    }

    [Fact]
    public void MapDataTableToModelIterable_WhenTableHasMultipleRows_ReturnsAllModelsInOrder()
    {
        var dataTable = CreateDataTable();
        dataTable.Rows.Add(17, "Ana");
        dataTable.Rows.Add(23, "Luis");

        var result = DbMapper.MapDataTableToModelIterable<TestModel>(dataTable).ToArray();

        Assert.Collection(result,
            model =>
            {
                Assert.Equal(17, model.Id);
                Assert.Equal("Ana", model.Name);
            },
            model =>
            {
                Assert.Equal(23, model.Id);
                Assert.Equal("Luis", model.Name);
            });
    }

    private static DataTable CreateDataTable()
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add(nameof(TestModel.Id), typeof(int));
        dataTable.Columns.Add(nameof(TestModel.Name), typeof(string));
        return dataTable;
    }

    private sealed class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
