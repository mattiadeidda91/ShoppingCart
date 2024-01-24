using Hangfire;
using Microsoft.Extensions.Logging;
using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using ShoppingCart.Abstractions.Hangfire;
using ShoppingCart.Abstractions.Services;
using System.ComponentModel;
using System.Text;
using System.Text.Json;

namespace ShoppingCart.Dependencies.Jobs
{
    public class MyJob : IJob
    {
        private readonly IUserRepository? userRepository;
        private readonly IEmailService? emailService;
        private readonly ILogger<MyJob>? logger;

        public MyJob() { }

        public MyJob(IEmailService emailService, IUserRepository userRepository, ILogger<MyJob>? logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        [DisableConcurrentExecution(int.MaxValue)]
        public async Task RunAsync()
        {
            ArgumentNullException.ThrowIfNull(emailService);
            ArgumentNullException.ThrowIfNull(userRepository);
            ArgumentNullException.ThrowIfNull(logger);

            Console.WriteLine("Hangifire job is running...");
            logger.LogInformation("Hangifire job is running...");

            //Send Emails
            var users = await userRepository.GetAllAsync();
            var usersTable = GenerateHtmlTable(users);

            await emailService.SendEmail("Shopping Cart Notification", $"Below is the list of Users:<br><br>{usersTable}<br><br> Regards.", new List<string> { "m.deidda91@libero.it" });

            Console.WriteLine("Hangifire job is finished...");
            logger.LogInformation("Hangifire job is finished...");
        }

        private static string GenerateHtmlTable(IEnumerable<User>? userList)
        {
            if (userList != null && userList.Any())
            {
                var htmlBuilder = new StringBuilder();

                htmlBuilder.Append("<table border='1'>");
                htmlBuilder.Append("<tr>");

                foreach (var property in typeof(User).GetProperties())
                {
                    //Ignore property with attribute [Description("ignore")]
                    if (property.GetCustomAttributes(typeof(DescriptionAttribute), true).Length == 0)
                    {
                        htmlBuilder.Append("<th>" + property.Name + "</th>");
                    }
                }

                htmlBuilder.Append("</tr>");

                //Fill data
                foreach (var user in userList)
                {
                    htmlBuilder.Append("<tr>");
                    foreach (var property in typeof(User).GetProperties())
                    {
                        //Ignore property with attribute [Description("ignore")]
                        if (property.GetCustomAttributes(typeof(DescriptionAttribute), true).Length == 0)
                        {
                            htmlBuilder.Append("<td>" + property.GetValue(user)?.ToString() + "</td>");
                        }
                    }

                    htmlBuilder.Append("</tr>");
                }

                htmlBuilder.Append("</table>");

                return htmlBuilder.ToString();
            }
            else
                return string.Empty;
        }
    }
}
