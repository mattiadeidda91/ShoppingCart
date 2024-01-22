using Dapper;
using Microsoft.Extensions.Logging;
using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using static ShoppingCart.Abstractions.Dapper.DataAccessGlobalConst;

namespace ShoppingCart.Sql.Dapper.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly ILogger<CartRepository> logger;
        public CartRepository(IDapperDataAccess dapperDataAccess, ILogger<CartRepository> logger) 
            : base(TableNameCarts, dapperDataAccess)
        { 
            this.logger = logger;
        }

        public async Task<IEnumerable<Cart>> GetCartByUserIdAsync(int userId)
        {
            var connection = dapperDataAccess.GetConnection();

            try
            {
                string query = "SELECT C.Id, U.Name, U.Lastname, U.Email, U.Phone, U.City " +
                                "FROM Carts C " +
                                "INNER JOIN Users U ON C.UserId = U.Id " +
                                "WHERE C.UserId = @UserId";

                var carts = await connection.QueryAsync<Cart, User, Cart>(
                query,
                param: new { UserId = userId },
                map: (cart, user) =>
                    {
                        //Mapping objects
                        cart.User = user;
                        return cart;
                    },
                splitOn: "Name"); // A comma-separated string that tells Dapper when the returned columns must be mapped to the next object.


                return carts;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return Enumerable.Empty<Cart>();
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public async Task<int> InserCartAsync(int userId)
        {
            var query = $@"INSERT INTO {TableNameCarts} ([UserID]) VALUES({userId});
                        SELECT CAST(SCOPE_IDENTITY() as int);";

            return await dapperDataAccess.ExecuteScalarAsync(query);
        }
    }
}
