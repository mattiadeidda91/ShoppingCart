using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using static ShoppingCart.Abstractions.Dapper.DataAccessGlobalConst;

namespace ShoppingCart.Sql.Dapper.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDapperDataAccess dapperDataAccess)
           : base(TableNameUsers, dapperDataAccess)
        { }

        public async Task<(IEnumerable<User?>, IEnumerable<Product?>)> GetUserAndProductsAsync()
        {
            var result = await dapperDataAccess.QueryMultipleAsync<User, Product>($@"
                    SELECT * FROM {TableNameUsers}; 
                    SELECT * FROM {TableNameProducts}");

            return result;
        }

        public async Task<IEnumerable<User>> GetUsersByProductIdAsync(int productId)
        {
            var query = $@"
                SELECT u.*
                FROM {TableNameUsers} u
                INNER JOIN {TableNameUserProducts} up ON u.Id = up.UserId
                WHERE up.ProductId = @ProductId;";

            var users = await dapperDataAccess.QueryAsync<User>(query, new { ProductId = productId });

            return users;
        }
    }
}
