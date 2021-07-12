using UnityEngine;
using Grpc.Core;
using MagicOnion.Client;
using MyApp.Shared;
using TMPro;

public class KumattaAppCall : MonoBehaviour
{

    void Start()
    {
        CallTest();
    }

    public async void CallTest()
    {
        string host = "localhost";
        int port = 5001;
        var channel = new Channel(host, port, ChannelCredentials.Insecure);
        //var channel = new Channel(host, port, new SslCredentials());

        var param = new SumAsyncParam()
        {
            X = 100,
            Y = 23
        };

        var client = MagicOnionClient.Create<IMyFirstService>(channel);
        var result = await client.SumAsync(param);

        var textUI = FindObjectOfType<TextMeshProUGUI>();
        textUI.text = $"Result: {result.Value}";
    }
}
