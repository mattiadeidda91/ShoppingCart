namespace ShoppingCart.Abstractions.Dapper.Entities
{
    public class Cart
    {  
        public int Id { get; set; }
        public User? User { get; set; }
    }
}
