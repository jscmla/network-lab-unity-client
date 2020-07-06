using UnityEngine;
using Grpc.Core;
using Mtracker;

public class MtrackerClient : MonoBehaviour
{
    void Start()
    {
        Channel channel = new Channel("localhost:50051", ChannelCredentials.Insecure);
        MtrackerService.MtrackerServiceClient client = new MtrackerService.MtrackerServiceClient(channel);

        var loginRes = client.Login(new Mtracker.Null{});
        Debug.Log("Login: " + loginRes);

        // MEMO: cannot connect to server when use connectServer() function
        connectServer(client);

        // streaming own objecect
        //channel.ShutdownAsync();
        //connectServer(client);


        //var res = await stream; // Error
        //Debug.Log(res);
        //Debug.Log(ownObject);

        //var logoutRes = client.Logout(loginRes);
        //Debug.Log("Logout: " + logoutRes);

    }

    void Update()
    {
        
    }

    async void connectServer(MtrackerService.MtrackerServiceClient client)
    {
        Mtracker.Object mobj = new Mtracker.Object();
        mobj.User = new User();
        mobj.Position = new Position();
        mobj.Rotation = new Rotation();

        mobj.User.Id = "testuserid";
        mobj.Position.X = 0.0f;
        mobj.Position.Y = 0.0f;
        mobj.Position.Z = 0.0f;
        mobj.Rotation.X = 0.0f;
        mobj.Rotation.Y = 0.0f;
        mobj.Rotation.Z = 0.0f;

        // create stream
        var stream = client.TransportOwnObject();

        // send message
        await stream.RequestStream.WriteAsync(new OwnTrackedObject { Object = mobj });

        // tell stream completion to server
        await stream.RequestStream.CompleteAsync();
    }
}
