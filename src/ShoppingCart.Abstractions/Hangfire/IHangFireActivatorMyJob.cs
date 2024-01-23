using Hangfire;
using System.Threading.Tasks;

namespace ShoppingCart.Abstractions.Hangfire
{
    public interface IHangFireActivatorMyJob
    {
        Task Run(IJobCancellationToken token);
        Task Run();
    }
}