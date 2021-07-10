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
        string host = "ローカルのIP";
        int port = 5001;
        var channel = new Channel(host, port, ChannelCredentials.Insecure);
        //var channel = new Channel(host, port, new SslCredentials());

        var client = MagicOnionClient.Create<IMyFirstService>(channel);
        var result = await client.SumAsync(100, 23);

        var textUI = FindObjectOfType<TextMeshProUGUI>();
        textUI.text = $"Result: {result}";
    }
}
