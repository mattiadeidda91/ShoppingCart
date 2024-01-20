using ShoppingCart.Abstractions.Dapper.Entities;

namespace ShoppingCart.Abstractions.Dapper.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool Exists(string lastname);
    }
}
