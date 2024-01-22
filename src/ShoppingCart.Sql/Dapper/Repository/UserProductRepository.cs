using Dapper;
using Microsoft.Extensions.Logging;
using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using ShoppingCart.Abstractions.Dapper.IRepository;
using System.Reflection;
using static ShoppingCart.Abstractions.Dapper.DataAccessGlobalConst;

#pragma warning disable CS8603 // Possible null reference return.

namespace ShoppingCart.Sql.Dapper.Repository
{
    public class UserProductRepository : Repository<UserProduct>, IUserProductRepository
    {
        private readonly ILogger<UserProductRepository> logger;
        public UserProductRepository(IDapperDataAccess dapperDataAccess, ILogger<UserProductRepository> logger)
            : base(TableNameUserProducts, dapperDataAccess)
        {
            this.logger = logger;

            CustomColumnsMapping();
        }

        public async Task<int> DeleteAsync(int userId, int productId)
        {
            var connection = dapperDataAccess.GetConnection();

            try
            {
                return await connection.ExecuteAsync($@"DELETE FROM {TableNameUserProducts} WHERE UserID=@UserId AND ProductID=@ProductId",
                    new { UserId = userId, ProductId = productId });
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message);
                return default;
            }
            finally
            {
                connection.Close();
                connection?.Dispose();
            }
        }

        public async Task<IEnumerable<UserProduct>> GetAsync()
        {
            var connection = dapperDataAccess.GetConnection();

            try
            {
                var query = @"
                        SELECT
                        up.UserID,
                        up.ProductID,
                        u.ID AS User_Id, u.Name AS User_Name, u.Lastname, u.Email, u.Phone, u.City,
                        p.ID AS Product_Id, p.Name AS Product_Name, p.Description, p.Category, p.Price
                        FROM UserProducts up
                        INNER JOIN Users u ON up.UserID = u.ID
                        INNER JOIN Products p ON up.ProductID = p.ID;";

                var result = await connection.QueryAsync<UserProduct, User, Product, UserProduct>(query,
                    map: (userProduct, user, product) =>
                    {
                        userProduct.User = user;
                        userProduct.Product = product;
                        return userProduct;

                    }, splitOn: "User_Id,Product_Id");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return Enumerable.Empty<UserProduct>();
            }
            finally
            {
                connection.Close();
                connection?.Dispose();
            }
        }

        public async Task<User> GetByUserIdAsync(int userId)
        {
            using var connection = dapperDataAccess.GetConnection();

            try
            {
                var usersDictionary = new Dictionary<int, User>();

                var query = @"
                        SELECT
                        up.UserID,
                        up.ProductID,
                        u.ID AS User_Id, u.Name AS User_Name, u.Lastname, u.Email, u.Phone, u.City,
                        p.ID AS Product_Id, p.Name AS Product_Name, p.Description, p.Category, p.Price
                        FROM UserProducts up
                        INNER JOIN Users u ON up.UserID = u.ID
                        INNER JOIN Products p ON up.ProductID = p.ID
                        WHERE up.UserID = @UserId;";

                var result = await connection.QueryAsync<User, Product, User>(
                    query,
                    (user, product) =>
                    {
                        if (!usersDictionary.TryGetValue(user.Id, out var usertEntry))
                        {
                            usertEntry = user;
                            usertEntry.Products = new List<Product>();
                            usersDictionary.Add(user.Id, usertEntry);
                        }

                        usertEntry!.Products!.Add(product);
                        return usertEntry;
                    },
                    new { UserId = userId },
                    splitOn: "Product_Id"
                );

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public async Task<int> InsertAsync(int userId, int productId)
        {
            var connection = dapperDataAccess.GetConnection();

            try
            {
                var query = $@"INSERT INTO {TableNameUserProducts} ([UserID], [ProductID]) VALUES({userId},{productId});
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                var result = await connection.ExecuteAsync(query);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return default;
            }
            finally
            {
                connection.Close();
                connection?.Dispose();
            }
        }

        private void CustomColumnsMapping()
        {
            var columnMaps = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
            {
                // Column => Property
                ["User_Id"] = "Id",
                ["User_Name"] = "Name",
                ["Product_Id"] = "Id",
                ["Product_Name"] = "Name"
            };

            var mapper = new Func<Type, string, PropertyInfo>((type, columnName) =>
                columnMaps.ContainsKey(columnName) ?
                type.GetProperty(columnMaps[columnName]) :
                type.GetProperty(columnName)
            );

            // Notify Dapper to use these mappings.
            SqlMapper.SetTypeMap(typeof(Product), new CustomPropertyTypeMap(typeof(Product), (type, columnName) => mapper(type, columnName)));
            SqlMapper.SetTypeMap(typeof(User), new CustomPropertyTypeMap(typeof(User), (type, columnName) => mapper(type, columnName)));
        }
    }
}

#pragma warning restore CS8603 // Possible null reference return.
