using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingCart.Abstractions.Hangfire
{
    public class HangFireActivatorMyJob : JobActivator, IHangFireActivatorMyJob
    {
        private readonly IServiceProvider _serviceProvider;

        public HangFireActivatorMyJob(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await Run();
        }

        public async Task Run()
        {
            using IServiceScope scope = this._serviceProvider.CreateScope();
            var myJobService = scope.ServiceProvider.GetRequiredService<IJob>();
            await myJobService.RunAsync();
        }
    }
}
