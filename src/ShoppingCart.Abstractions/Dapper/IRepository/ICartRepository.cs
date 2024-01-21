using ShoppingCart.Abstractions.Dapper.Entities;

namespace ShoppingCart.Abstractions.Dapper.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<IEnumerable<Cart>> GetCartByUserIdAsync(int userId);
    }
}
