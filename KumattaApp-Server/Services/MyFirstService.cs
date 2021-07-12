using MagicOnion;
using MagicOnion.Server;
using MyApp.Shared;
using System;

namespace KumattaAppServer.Services
{
    public class MyFirstService : ServiceBase<IMyFirstService>, IMyFirstService
    {
        public async UnaryResult<SumAsyncResult> SumAsync(SumAsyncParam param)
        {
            Console.WriteLine($"Received:{param.X}, {param.Y}");

            var result = new SumAsyncResult() 
            {
                Value = param.X + param.Y
            };
            return result;
        }
    }
}
