﻿using Hangfire;
using Hangfire.RecurringJobAdmin;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using ShoppingCart.Dependencies.Configurations.Hangfire;
using System.Reflection;

namespace ShoppingCart.Dependencies.Configurations.Hangfire
{
    public static class HangfireRegistrationExtensions
    {
        public static WebApplicationBuilder AddHangfire(this WebApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.Services
                .AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseColouredConsoleLogProvider()
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(builder.Configuration.GetConnectionString("SqlConnection"),
                        new SqlServerStorageOptions
                        {
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.Zero,
                            UseRecommendedIsolationLevel = true,
                            DisableGlobalLocks = true
                        })
                    .UseRecurringJobAdmin(Assembly.GetExecutingAssembly())
                    ).AddHangfireServer();

            return builder;
        }
    }
}
