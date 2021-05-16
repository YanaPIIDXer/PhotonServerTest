using System.Collections.Generic;
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

    /// <summary>
    /// イベント送信
    /// </summary>
    /// <param name="Code">イベントコード</param>
    /// <param name="Params">パラメータ</param>
    public void SendEvent(EEventCode Code, Dictionary<byte, object> Params)
    {
        var Data = new EventData((byte)Code, Params);
        SendEvent(Data, new SendParameters());
    }

    /// <summary>
    /// イベント送信
    /// </summary>
    /// <param name="Code">イベントコード</param>
    public void SendEvent(EEventCode Code)
    {
        SendEvent(Code, new Dictionary<byte, object>());
    }

    protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
    {
    }

    protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
    {
        CurrentState.OnRecvOperation((EOperationCode)operationRequest.OperationCode, operationRequest.Parameters);
    }
}
