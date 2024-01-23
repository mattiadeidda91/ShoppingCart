namespace ShoppingCart.Abstractions.Hangfire
{
    public interface IJob
    {
        Task RunAsync();
    }
}
