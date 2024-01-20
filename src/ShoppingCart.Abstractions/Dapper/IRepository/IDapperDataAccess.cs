namespace ShoppingCart.Abstractions.Dapper.Interfaces
{
    public interface IDapperDataAccess
    {
        IEnumerable<TEntity> Query<TEntity>(string sqlQuery, object? param = null);
        TEntity? QuerySingleOrDefault<TEntity>(string sqlQuery, object? param = null);
        TEntity? QueryFirstOrDefault<TEntity>(string sqlQuery, object? param = null);
        int Execute(string sqlQuery, object? param = null);
        int ExecuteScalar(string sqlQuery, object? param = null);
    }
}
