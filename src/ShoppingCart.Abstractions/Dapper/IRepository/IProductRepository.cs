using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;

namespace ShoppingCart.Abstractions.Dapper.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductByUserIdAsync(int userId);
        Task<(IEnumerable<User?>, IEnumerable<Product?>)> GetUserAndProductsAsync();
    }
}
