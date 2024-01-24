namespace ShoppingCart.Abstractions.Configurations
{
    public class EmailOptions
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public Credentials? Credentials { get; set; }

        
    }
    public class Credentials
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
