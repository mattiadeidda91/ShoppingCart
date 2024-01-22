using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingCart.Dependencies.Jobs;
using System.Diagnostics.CodeAnalysis;

namespace ShoppingCart.Dependencies.Configurations.Hangfire
{
    public static class HangfireIEndpointConventionBuilderExtensions
    {
        /// <summary>
        /// Map and use Hangfire Dashboard
        /// </summary>
        /// <param name="endpoints"></param>
        /// <param name="requiredScope">Required scope for user authorization</param>
        /// <param name="allowAnonymousInDevelopment">Set Dashboard AllowAnonymous access in Development environment</param>
        public static IEndpointConventionBuilder MapAndUseHangfireDashboard([NotNull] this IEndpointRouteBuilder endpoints,
                                                                            [NotNull] string requiredScope,
                                                                            [NotNull] bool allowAnonymousInDevelopment)
        {
            var serviceProvider = endpoints.ServiceProvider;
            var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();

            var allowAnonymous = env.IsDevelopment() && allowAnonymousInDevelopment;

            var dashboardOptions = new DashboardOptions
            {
                Authorization = new[] { new DashboardAuthorizationFilter(allowAnonymous, requiredScope) },
                DisplayStorageConnectionString = false,
                AppPath = null,
                StatsPollingInterval = 60000
            };

            var endpoint = endpoints.MapHangfireDashboard("/jobs", dashboardOptions);

            if (allowAnonymous)
                endpoint = endpoint.AllowAnonymous();

            return endpoint;
        }

        public static void ConfigureJob()
        {
            RecurringJob.AddOrUpdate($"my-job-{Guid.NewGuid()}", () => MyJob.YourMethod(), Cron.Minutely, new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });

        }
    }

    internal class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly bool allowAnonymous;
        private readonly string requiredScope;

        public DashboardAuthorizationFilter(bool allowAnonymous, string requiredScope)
        {
            this.allowAnonymous = allowAnonymous;
            this.requiredScope = requiredScope;

        }

        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            if (allowAnonymous)
                return true;
            else
            {
                if (httpContext.User.Identity is null)
                    return false;
                if (!httpContext.User.Identity.IsAuthenticated)
                    return false;
                var userScopes = httpContext.User.FindFirst("scope")?.Value;
                return userScopes?.Split(' ').Contains(requiredScope) ?? false;
            }
        }
    }

}
