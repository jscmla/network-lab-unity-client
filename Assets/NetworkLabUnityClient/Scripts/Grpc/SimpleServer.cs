using System.Threading.Tasks;
using UnityEngine;
using Grpc.Core;
using Simple.Grpc;

public class SimpleServer : MonoBehaviour
{
    class SimpleServiceImpl : SimpleService.SimpleServiceBase
    {
        public override Task<SimpleResponse> SimpleSend(SimpleRequest request, ServerCallContext context)
        {
            var text = "This is TEST!";

            return Task.FromResult(new SimpleResponse { Text = text });
        }
    }

    void Start()
    {
        Server server = new Server
        {
            Services = { SimpleService.BindService(new SimpleServiceImpl()) },
            Ports = { new ServerPort("localhost", 50051, ServerCredentials.Insecure) }
        };
        server.Start();
        Debug.Log("Start server.");
    }
}
