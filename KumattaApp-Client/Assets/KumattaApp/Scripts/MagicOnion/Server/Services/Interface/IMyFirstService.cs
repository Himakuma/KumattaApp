using MagicOnion;
using MessagePack;

namespace MyApp.Shared
{
    public interface IMyFirstService : IService<IMyFirstService>
    {
        UnaryResult<SumAsyncResult> SumAsync(SumAsyncParam param);
    }
}