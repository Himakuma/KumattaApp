using MagicOnion;

namespace MyApp.Shared
{
    public interface IMyFirstService : IService<IMyFirstService>
    {
        UnaryResult<SumAsyncResult> SumAsync(SumAsyncParam param);
    }
}