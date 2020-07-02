using UnityEngine;
using Grpc.Core;
using Simplechat;

public class SimpleChatCsClient : MonoBehaviour
{
    void Start()
    {
        connectServer();
    }

    // TODO: channelを正常に閉じる処理を記載する
    async void connectServer()
    {
        Channel channel = new Channel("localhost:50051", ChannelCredentials.Insecure);
        SimpleChat.SimpleChatClient client = new SimpleChat.SimpleChatClient(channel);

        var stream = client.SendMessage();
        await stream.RequestStream.WriteAsync(new SendRequest { Id = "2", Name = "jscmla", Content = "Hello from Unity" });
        await stream.RequestStream.CompleteAsync();
        var res = await stream.ResponseAsync;

        channel.ShutdownAsync().Wait();
        Debug.Log(channel.State);
    }
}
