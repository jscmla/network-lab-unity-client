using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class HelloPUN : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        // Connect by PhotonServerSettings.asset
        PhotonNetwork.ConnectUsingSettings();
    }

    // Try to connect to game server
    public override void OnConnectedToMaster()
    {
        // Create or Join to room "HelloPUN"
        PhotonNetwork.JoinOrCreateRoom("HelloPUN", new RoomOptions(), TypedLobby.Default);
    }

    // Instantiate Object
    public override void OnJoinedRoom()
    {
        var v = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate("PUNCube", v, Quaternion.identity);
    }
}
