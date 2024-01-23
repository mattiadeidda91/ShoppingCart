using Microsoft.Extensions.Logging;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using ShoppingCart.Abstractions.Hangfire;

namespace ShoppingCart.Dependencies.Jobs
{
    public class MyJob : IJob
    {
        private readonly IUserRepository? userRepository;
        private readonly ILogger<MyJob>? logger;

        public MyJob() { }

        public MyJob(IUserRepository userRepository, ILogger<MyJob>? logger)
        {
            this.logger = logger;
            this.userRepository = userRepository;
        }

        public async Task RunAsync()
        {
            ArgumentNullException.ThrowIfNull(userRepository);
            ArgumentNullException.ThrowIfNull(logger);

            //Do something
            var users = await userRepository.GetAllAsync();

            Console.WriteLine("Hello from Hangfire...");
        }
    }
}
