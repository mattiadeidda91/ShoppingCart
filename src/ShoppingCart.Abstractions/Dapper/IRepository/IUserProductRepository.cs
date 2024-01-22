using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;

namespace ShoppingCart.Abstractions.Dapper.IRepository
{
    public interface IUserProductRepository : IRepository<UserProduct>
    {
        Task<IEnumerable<UserProduct>> GetAsync();
        Task<User> GetByUserIdAsync(int userId);
        Task<int> InsertAsync(int userId, int productId);
        Task<int> DeleteAsync(int userId, int productId);
    }
}
