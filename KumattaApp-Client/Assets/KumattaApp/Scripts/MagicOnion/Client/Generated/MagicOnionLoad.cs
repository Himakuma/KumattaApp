using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

public class MagicOnionLoad 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void RegisterResolvers()
    {
        StaticCompositeResolver.Instance.Register(
            MagicOnion.Resolvers.MagicOnionResolver.Instance,
            MessagePack.Resolvers.GeneratedResolver.Instance,
            StandardResolver.Instance
        );

        MessagePackSerializer.DefaultOptions = MessagePackSerializer.DefaultOptions
            .WithResolver(StaticCompositeResolver.Instance);
    }
}
