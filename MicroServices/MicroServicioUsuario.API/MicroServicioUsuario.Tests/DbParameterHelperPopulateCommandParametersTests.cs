using MicroServicoUser.Inf.Persistence.Database;

namespace MicroServicioUsuario.Tests;

// Cases map to the control-flow graph of DbParameterHelper.PopulateCommandParameters<T>
// (docs/testing/usuarios/control-flow-graphs.md, lines 10-41).
// CP1-CP5 cover the five independent paths derived from V(G) = 5.
// CA6-CA8 are additional data-level cases that do not add independent paths.
public class DbParameterHelperPopulateCommandParametersTests
{
    // CP1: model == null -> early return without reflection.
    [Fact]
    public void PopulateCommandParameters_WhenModelIsNull_ReturnsCommandWithoutParameters()
    {
        const string query = "SELECT * FROM Usuario WHERE Id = @Id";

        var command = DbParameterHelper.PopulateCommandParameters<TestModel>(query, null);

        Assert.Equal(query, command.CommandText);
        Assert.Empty(command.Parameters);
    }

    // CP2: model != null and the query has no @placeholders -> the parameter loop is skipped.
    [Fact]
    public void PopulateCommandParameters_WhenQueryHasNoPlaceholders_ReturnsCommandWithoutParameters()
    {
        const string query = "SELECT 1";

        var command = DbParameterHelper.PopulateCommandParameters(query, new TestModel { Id = 17 });

        Assert.Equal(query, command.CommandText);
        Assert.Empty(command.Parameters);
    }

    // CP3: placeholder matches a property and the value is not null -> value is kept as-is.
    [Fact]
    public void PopulateCommandParameters_WhenPropertyValueIsNotNull_AddsParameterWithModelValue()
    {
        var command = DbParameterHelper.PopulateCommandParameters(
            "SELECT * FROM Usuario WHERE Id = @Id",
            new TestModel { Id = 17 });

        var parameter = Assert.Single(command.Parameters.Cast<MySql.Data.MySqlClient.MySqlParameter>());
        Assert.Equal("@Id", parameter.ParameterName);
        Assert.Equal(17, parameter.Value);
    }

    // CP4: placeholder matches a property whose value is null -> DBNull.Value is used.
    [Fact]
    public void PopulateCommandParameters_WhenPropertyValueIsNull_AddsParameterWithDbNullValue()
    {
        var command = DbParameterHelper.PopulateCommandParameters(
            "UPDATE Usuario SET Name = @Name WHERE Id = @Id",
            new TestModel { Id = 17, Name = null });

        var parameter = Assert.Single(command.Parameters.Cast<MySql.Data.MySqlClient.MySqlParameter>(),
            p => p.ParameterName == "@Name");
        Assert.Equal(DBNull.Value, parameter.Value);
    }

    // CP5: placeholder without a matching property -> FirstOrDefault returns null, nothing is added.
    [Fact]
    public void PopulateCommandParameters_WhenPlaceholderHasNoMatchingProperty_DoesNotAddParameter()
    {
        var command = DbParameterHelper.PopulateCommandParameters(
            "SELECT * FROM Usuario WHERE Unknown = @Unknown",
            new TestModel { Id = 17 });

        Assert.Empty(command.Parameters);
    }

    // CA6: Distinct() collapses repeated placeholders into a single parameter.
    [Fact]
    public void PopulateCommandParameters_WhenPlaceholderIsRepeated_AddsSingleParameter()
    {
        var command = DbParameterHelper.PopulateCommandParameters(
            "SELECT * FROM Usuario WHERE Id = @Id OR Parent = @Id",
            new TestModel { Id = 17 });

        var parameter = Assert.Single(command.Parameters.Cast<MySql.Data.MySqlClient.MySqlParameter>());
        Assert.Equal("@Id", parameter.ParameterName);
        Assert.Equal(17, parameter.Value);
    }

    // CA7: several placeholders -> the loop iterates repeatedly, keeps query order and skips unmatched names.
    [Fact]
    public void PopulateCommandParameters_WhenSeveralPlaceholdersExist_AddsOnlyMatchingParametersInQueryOrder()
    {
        var command = DbParameterHelper.PopulateCommandParameters(
            "INSERT INTO Usuario (Name, Unknown, Id) VALUES (@Name, @Unknown, @Id)",
            new TestModel { Id = 17, Name = "Ana" });

        var parameters = command.Parameters.Cast<MySql.Data.MySqlClient.MySqlParameter>().ToArray();
        Assert.Collection(parameters,
            first =>
            {
                Assert.Equal("@Name", first.ParameterName);
                Assert.Equal("Ana", first.Value);
            },
            second =>
            {
                Assert.Equal("@Id", second.ParameterName);
                Assert.Equal(17, second.Value);
            });
    }

    // CA8: characterization of the ordinal, case-sensitive comparison p.Name == modelPropName.
    [Fact]
    public void PopulateCommandParameters_WhenPlaceholderDiffersOnlyByCase_DoesNotAddParameter()
    {
        var command = DbParameterHelper.PopulateCommandParameters(
            "SELECT * FROM Usuario WHERE Id = @id",
            new TestModel { Id = 17 });

        Assert.Empty(command.Parameters);
    }

    private sealed class TestModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
