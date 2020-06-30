using UnityEngine;
using Grpc.Core;
using Simple.Grpc;

public class SimpleClient : MonoBehaviour
{
    void Start()
    {
        Channel channel = new Channel("localhost:50051", ChannelCredentials.Insecure);
        SimpleService.SimpleServiceClient client = new SimpleService.SimpleServiceClient(channel);

        var response = client.SimpleSend(new SimpleRequest());
        Debug.Log("text: " + response.Text);
    }
}
