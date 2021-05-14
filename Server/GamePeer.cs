using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using Common.Packet;
using System;
using System.Reactive.Subjects;

public class GamePeer : ClientPeer
{
    /// <summary>
    /// リクエスト受信Subject
    /// </summary>
    private Subject<OperationPacket> OnRecvRequestSubject = new Subject<OperationPacket>();

    /// <summary>
    /// リクエストを受信した
    /// </summary>
    public IObservable<OperationPacket> OnRecvRequest { get { return OnRecvRequestSubject; } }

    public GamePeer(InitRequest initRequest) : base(initRequest)
    {
    }

    protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
    {
    }

    protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
    {
        OperationPacket Packet = new OperationPacket(operationRequest.OperationCode, operationRequest.Parameters);
        OnRecvRequestSubject.OnNext(Packet);
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
}
