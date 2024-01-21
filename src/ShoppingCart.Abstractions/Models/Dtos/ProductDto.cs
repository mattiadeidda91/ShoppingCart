using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Abstractions.Models.Dtos
{
    public class ProductDto
    {
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public string? Category { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
