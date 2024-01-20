namespace ShoppingCart.Abstractions.Dapper.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Dictionary<string, string> criteria);
        void Delete(int id);
        TEntity? GetById(int id);
        int Update(TEntity entity);
        int Insert(TEntity entity);
    }
}
