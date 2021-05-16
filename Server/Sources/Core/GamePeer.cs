using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using Common.Packet;
using System;
using System.Reactive.Subjects;
using State;
using System.Reactive;
using Character.Player;
using System.Collections.Generic;

public class GamePeer : ClientPeer
{
    /// <summary>
    /// プレイヤーキャラ
    /// </summary>
    public Player PlayerCharacter { get; private set; }

    /// <summary>
    /// パケットを受信した
    /// </summary>
    /// <param name="RecvPacket">受信したパケット</param>
    public delegate void OnRecvRequest(object RecvPacket);

    /// <summary>
    /// リクエストハンドラ
    /// </summary>
    /// <typeparam name="EPacketID">パケットＩＤ</typeparam>
    /// <typeparam name="OnRecvRequest">ハンドリング用delegate</typeparam>
    private Dictionary<EPacketID, OnRecvRequest> RequestHandlers = new Dictionary<EPacketID, OnRecvRequest>();

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

    /// <summary>
    /// リクエストハンドラ追加
    /// </summary>
    /// <param name="ID">パケットＩＤ</param>
    /// <param name="Handler">ハンドリング用delegate</param>
    public void AddRequestHandler(EPacketID ID, OnRecvRequest Handler)
    {
        if (!RequestHandlers.ContainsKey(ID))
        {
            RequestHandlers.Add(ID, Handler);
        }
        else
        {
            RequestHandlers[ID] += Handler;
        }
    }

    protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
    {
        OnDisconnectedSubject.OnNext(reasonCode);
    }

    protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
    {
        EPacketID ID = (EPacketID)operationRequest.OperationCode;
        if (RequestHandlers.ContainsKey(ID))
        {
            RequestHandlers[ID].Invoke(operationRequest.Parameters[0]);
        }
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
    /// <param name="SendPacket">送信するパケット</param>
    public void SendEventPacket(Packet SendPacket)
    {
        var Data = SendPacket.MakeSendData();
        SendEvent(new EventData(Data.SendCode, Data.SendDictionary), new SendParameters());
    }

    /// <summary>
    /// レスポンスパケット送信
    /// </summary>
    /// <param name="SendPacket">送信するパケット</param>
    public void SendResponsePacket(Packet SendPacket)
    {
        var Data = SendPacket.MakeSendData();
        var Response = new OperationResponse(Data.SendCode, Data.SendDictionary);
        SendOperationResponse(Response, new SendParameters());
    }
}
