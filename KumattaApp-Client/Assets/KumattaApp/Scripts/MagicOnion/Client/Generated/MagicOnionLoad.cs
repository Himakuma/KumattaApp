using Grpc.Core;
using MagicOnion.Unity;
using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

public class MagicOnionLoad
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void RegisterResolvers()
    {
        StaticCompositeResolver.Instance.Register(
            MagicOnion.Resolvers.MagicOnionResolver.Instance,
            MessagePack.Resolvers.GeneratedResolver.Instance,
            StandardResolver.Instance
        );

        MessagePackSerializer.DefaultOptions = MessagePackSerializer.DefaultOptions
            .WithResolver(StaticCompositeResolver.Instance);
    }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void OnRuntimeInitialize()
    {
        // gRPCの初期化
        GrpcChannelProviderHost.Initialize(new DefaultGrpcChannelProvider(new[]
        {
            // キープアライブを5秒単位に送信（デフォルト2時間）
            new ChannelOption("grpc.keepalive_time_ms", 5000),

            // キープアライブのpingタイムアウトを5秒（デフォルト20秒）
            new ChannelOption("grpc.keepalive_timeout_ms", 5000),
        }));
    }
}
