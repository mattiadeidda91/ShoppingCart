using System.ComponentModel;

namespace ShoppingCart.Abstractions.Dapper.Entities
{
    public class User
    {
        [Description("ignore")]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
    }
}
