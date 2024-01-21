using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using ShoppingCart.Abstractions.Dapper.IRepository;
using static ShoppingCart.Abstractions.Dapper.DataAccessGlobalConst;

namespace ShoppingCart.Sql.Dapper.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IDapperDataAccess dapperDataAccess)
           : base(TableNameProducts, dapperDataAccess)
        { }

        public async Task<(IEnumerable<User?>, IEnumerable<Product?>)> GetUserAndProductsAsync()
        {
            var result = await dapperDataAccess.QueryMultipleAsync<User, Product>($@"
                    SELECT * FROM {TableNameUsers}; 
                    SELECT * FROM {TableNameProducts}");

            return result;
        }

        public async Task<IEnumerable<Product>> GetProductByUserIdAsync(int userId)
        {
            var query = $@"
                SELECT p.*
                FROM {TableNameProducts} p
                INNER JOIN {TableNameUserProducts} up ON p.Id = up.ProductId
                WHERE up.UserId = @UserId;";

            var products = await dapperDataAccess.QueryAsync<Product>(query, new { UserId = userId });

            return products;
        }
    }
}
