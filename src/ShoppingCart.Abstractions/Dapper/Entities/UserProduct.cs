namespace ShoppingCart.Abstractions.Dapper.Entities
{
    public class UserProduct
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
