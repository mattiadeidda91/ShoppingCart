using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShoppingCart.Abstractions.Configurations;
using ShoppingCart.Abstractions.Services;
using System.Net;
using System.Net.Mail;

namespace ShoppingCart.Dependencies.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions emailOptions;
        private readonly ILogger<EmailService> logger;

        public EmailService(IOptions<EmailOptions> emailOptions, ILogger<EmailService>? logger) 
        {
            this.emailOptions = emailOptions != null ? emailOptions.Value : throw new ArgumentNullException(nameof(emailOptions));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendEmail(string subject, string body, List<string> toEmails, List<string>? ccEmails = null, List<string>? bccEmails = null)
        {
            ArgumentNullException.ThrowIfNull(emailOptions.Credentials);

            var credentials = new NetworkCredential(emailOptions.Credentials.Email, emailOptions.Credentials.Password);

            var serverSmtp = new SmtpClient
            {
                Host = emailOptions.Host,
                Port = emailOptions.Port,
                EnableSsl = emailOptions.EnableSsl,
                Credentials = credentials
            };

            var email = new MailMessage
            {
                From = new MailAddress(emailOptions.Credentials.Email!),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            AddRecipients(email.To, toEmails);
            AddRecipients(email.CC, ccEmails);
            AddRecipients(email.Bcc, bccEmails);

            try
            {
                await serverSmtp.SendMailAsync(email);

                logger.LogInformation($"Email '{subject}' sent to {string.Join(',',toEmails)}");
            }
            catch (SmtpException ex)
            {
                logger.LogError(ex.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            finally
            {
                email.Dispose();
            }
        }

        private void AddRecipients(MailAddressCollection recipients, IEnumerable<string>? emails)
        {
            if (emails != null)
            {
                foreach (var email in emails)
                {
                    recipients.Add(new MailAddress(email));
                }
            }
        }
    }
}
