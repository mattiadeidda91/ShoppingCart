using ShoppingCart.Abstractions.Dapper.Entities;

namespace ShoppingCart.Abstractions.Dapper.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUsersByProductIdAsync(int productId);
        Task<(IEnumerable<User?>, IEnumerable<Product?>)> GetUserAndProductsAsync();
    }
}
