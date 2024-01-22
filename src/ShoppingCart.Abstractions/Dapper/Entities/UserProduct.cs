namespace ShoppingCart.Abstractions.Dapper.Entities
{
    public class UserProduct
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public Product? Product { get; set;}
        public User? User { get; set;}
    }
}
