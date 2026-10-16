namespace MicroServicoUser.Inf.Persistence.Database;

public interface IMySqlConnectionManager
{
    IEnumerable<T> ExecuteQuery<T>(string query) where T : new();

    IEnumerable<T> ExecuteParameterizedQuery<T>(string query, T model) where T : new();

    IEnumerable<TOut> ExecuteParameterizedQuery<TOut, TParam>(string query, TParam model)
        where TOut : new();

    int ExecuteParameterizedNonQuery<T>(string query, T model) where T : new();
}
