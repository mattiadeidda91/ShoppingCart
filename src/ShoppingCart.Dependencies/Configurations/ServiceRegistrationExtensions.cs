using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Abstractions.Dapper;
using ShoppingCart.Abstractions.Dapper.DbContext;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using ShoppingCart.Abstractions.Dapper.IRepository;
using ShoppingCart.Abstractions.Hangfire;
using ShoppingCart.Abstractions.Services;
using ShoppingCart.Dependencies.Configurations.Hangfire;
using ShoppingCart.Dependencies.Jobs;
using ShoppingCart.Dependencies.Services;
using ShoppingCart.Sql.Dapper.Repository;

namespace ShoppingCart.Dependencies.Configurations
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services)
        {
            //Repositories
            _ = services.AddSingleton<ShoppingCartContext>();
            _ = services.AddSingleton<IDapperDataAccess, DapperDataAccess>();
            _ = services.AddScoped<IUserRepository, UserRepository>();
            _ = services.AddScoped<IProductRepository, ProductRepository>();
            _ = services.AddScoped<ICartRepository, CartRepository>();
            _ = services.AddScoped<IUserProductRepository, UserProductRepository>();

            //Services
            _ = services.AddScoped<IEmailService, EmailService>();

            //Hangfire
            _ = services.AddScoped<IJob, MyJob>();
            _ = services.AddScoped<IHangFireActivatorMyJob, HangFireActivatorMyJob>();

            return services;
        }
    }
}
