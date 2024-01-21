namespace ShoppingCart.Abstractions.Models.Dtos
{
    public class CartDto
    {
        public int Id { get; set; }
        public UserDto? User { get; set; }
    }
}
