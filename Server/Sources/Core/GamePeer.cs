using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using GameState;
using Common.Code;

public class GamePeer : ClientPeer
{
    /// <summary>
    /// 現在のState
    /// </summary>
    private GameState.GameState CurrentState = null;

    public GamePeer(InitRequest initRequest) : base(initRequest)
    {
        CurrentState = new GameStateTitle(this);
    }

    protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
    {
    }

    protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
    {
        CurrentState.OnRecvOperation((EOperationCode)operationRequest.OperationCode, operationRequest.Parameters);
    }
}
