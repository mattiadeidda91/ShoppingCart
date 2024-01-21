using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ShoppingCart.Abstractions.Dapper.DbContext
{
    public class ShoppingCartContext
    {
        private readonly string? connString;
        public ShoppingCartContext(IConfiguration configuration)
        {
            connString = configuration != null
                ? configuration.GetConnectionString("SqlConnection")
                : throw new ArgumentNullException(nameof(configuration));
        }
        public IDbConnection CreateConnection() => new SqlConnection(connString);
    }
}
