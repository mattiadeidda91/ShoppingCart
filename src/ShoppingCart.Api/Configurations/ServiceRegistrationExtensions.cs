using ShoppingCart.Abstractions.Dapper;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using ShoppingCart.Sql.Dapper.Repository;

namespace ShoppingCart.Api.Configurations
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services)
        {
            _ = services.AddTransient<IDapperDataAccess, DapperDataAccess>();
            _ = services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
