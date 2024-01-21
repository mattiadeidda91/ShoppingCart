using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Abstractions.Models.Dtos
{
    public class UserDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Lastname { get; set; }

        [Required]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Email field is invalid.")]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? City { get; set; }
    }
}
