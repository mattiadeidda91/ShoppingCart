namespace ShoppingCart.Abstractions.Dapper.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Sync

        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Dictionary<string, string> criteria);
        TEntity? GetById(int id);
        int Update(TEntity entity);
        int Insert(TEntity entity);
        int Delete(int id);

        #endregion

        #region Async

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Dictionary<string, string> criteria);
        Task<TEntity?> GetByIdAsync(int id);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> InsertAsync(TEntity entity);
        Task<int> DeleteAsync(int id);

        #endregion

    }
}
