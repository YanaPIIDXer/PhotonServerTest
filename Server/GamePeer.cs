using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

public class GamePeer : ClientPeer
{
    public GamePeer(InitRequest initRequest) : base(initRequest)
    {
    }

    protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
    {
    }

    protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
    {
    }
}
