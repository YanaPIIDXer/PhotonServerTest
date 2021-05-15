using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using Common.Packet;
using System;
using System.Reactive.Subjects;
using State;
using System.Reactive;
using Character.Player;

public class GamePeer : ClientPeer
{
    /// <summary>
    /// プレイヤーキャラ
    /// </summary>
    public Player PlayerCharacter { get; private set; }

    /// <summary>
    /// リクエスト受信Subject
    /// </summary>
    private Subject<OperationPacket> OnRecvRequestSubject = new Subject<OperationPacket>();

    /// <summary>
    /// リクエストを受信した
    /// </summary>
    public IObservable<OperationPacket> OnRecvRequest { get { return OnRecvRequestSubject; } }

    /// <summary>
    /// アクティブステート遷移Subject
    /// </summary>
    private Subject<Unit> OnActiveStateSubject = new Subject<Unit>();

    /// <summary>
    /// アクティブステートに遷移した
    /// </summary>
    public IObservable<Unit> OnActiveState { get { return OnActiveStateSubject; } }

    /// <summary>
    /// 現在のゲームステート
    /// ※ぶっちゃけ参照生かしてるだけ
    /// </summary>
    private GameState CurrentState = null;

    /// <summary>
    /// 切断Subject
    /// </summary>
    private Subject<DisconnectReason> OnDisconnectedSubject = new Subject<DisconnectReason>();

    /// <summary>
    /// 切断された
    /// </summary>
    public IObservable<DisconnectReason> OnDisconnected { get { return OnDisconnectedSubject; } }

    public GamePeer(InitRequest initRequest) : base(initRequest)
    {
        CurrentState = new GameStateTitle(this);
    }

    protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
    {
        OnDisconnectedSubject.OnNext(reasonCode);
    }

    protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
    {
        OperationPacket Packet = new OperationPacket(operationRequest.OperationCode, operationRequest.Parameters);
        OnRecvRequestSubject.OnNext(Packet);
    }

    /// <summary>
    /// ActiveStateに遷移
    /// </summary>
    public void ToActiveState()
    {
        CurrentState = new GameStateActive(this);
        PlayerCharacter = new Player(this);
        OnActiveStateSubject.OnNext(Unit.Default);
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
