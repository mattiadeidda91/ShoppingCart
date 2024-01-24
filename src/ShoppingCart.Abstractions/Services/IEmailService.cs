namespace ShoppingCart.Abstractions.Services
{
    public interface IEmailService
    {
        Task SendEmail(string subject, string body, List<string> toEmails, List<string>? ccEmails = null, List<string>? bccEmails = null);
    }
}
