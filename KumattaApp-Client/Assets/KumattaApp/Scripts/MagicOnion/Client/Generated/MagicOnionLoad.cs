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
        // gRPC�̏�����
        GrpcChannelProviderHost.Initialize(new DefaultGrpcChannelProvider(new[]
        {
            // �L�[�v�A���C�u��5�b�P�ʂɑ��M�i�f�t�H���g2���ԁj
            new ChannelOption("grpc.keepalive_time_ms", 5000),

            // �L�[�v�A���C�u��ping�^�C���A�E�g��5�b�i�f�t�H���g20�b�j
            new ChannelOption("grpc.keepalive_timeout_ms", 5000),
        }));
    }
}
