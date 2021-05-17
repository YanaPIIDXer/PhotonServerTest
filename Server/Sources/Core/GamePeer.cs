using System.Collections.Generic;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using GameState;
using Common.Packet;
using Common.Stream;

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
    /// <param name="SendPacket">送信パケット</param>
    public void SendEvent(IPacket SendPacket)
    {
        DictionaryStreamWriter Writer = new DictionaryStreamWriter();
        SendPacket.Serialize(Writer);
        var Data = new EventData((byte)SendPacket.PacketID, Writer.Dest);
        SendEvent(Data, new SendParameters());
    }

    protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
    {
    }

    protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
    {
        CurrentState.OnRecvOperation((EPacketID)operationRequest.OperationCode, operationRequest.Parameters);
    }
}
