namespace ShoppingCart.Abstractions.Dapper.Interfaces
{
    public interface IDapperDataAccess
    {
        IEnumerable<TEntity> Query<TEntity>(string sqlQuery, object? param = null);
        TEntity? QuerySingleOrDefault<TEntity>(string sqlQuery, object? param = null);
        TEntity? QueryFirstOrDefault<TEntity>(string sqlQuery, object? param = null);
        int Execute(string sqlQuery, object? param = null);
        int ExecuteScalar(string sqlQuery, object? param = null);

        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sqlQuery, object? param = null);
        Task<TEntity?> QuerySingleOrDefaultAsync<TEntity>(string sqlQuery, object? param = null);
        Task<TEntity?> QueryFirstOrDefaultAsync<TEntity>(string sqlQuery, object? param = null);
        Task<int> ExecuteAsync(string sqlQuery, object? param = null);
        Task<int> ExecuteScalarAsync(string sqlQuery, object? param = null);
        Task<(IEnumerable<TEntity1?>, IEnumerable<TEntity2?>)> QueryMultipleAsync<TEntity1, TEntity2>(string sqlQuery, object? param = null);
    }
}
