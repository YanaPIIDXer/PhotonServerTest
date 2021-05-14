using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using Common.Packet;

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

    /// <summary>
    /// イベントパケット送信
    /// </summary>
    /// <param name="Packet">パケット</param>
    public void SendEventPacket(EventPacket Packet)
    {
        byte Code = Packet.SendCode;
        var Params = Packet.SendParamsDictionary;
        var Data = new EventData(Code, Params);
        SendEvent(Data, new SendParameters());
    }

    /// <summary>
    /// レスポンスパケット送信
    /// </summary>
    /// <param name="Packet">パケット</param>
    public void SendResponsePacket(OperationPacket Packet)
    {
        byte Code = Packet.SendCode;
        var Params = Packet.SendParamsDictionary;
        var Response = new OperationResponse(Code, Params);
        SendOperationResponse(Response, new SendParameters());
    }
