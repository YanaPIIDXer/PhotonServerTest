using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using System;
using UniRx;
using Common.Packet;

namespace Game.Networking
{
    /// <summary>
    /// 接続クライアント
    /// </summary>
    public class ConnectionClient : MonoBehaviour, IPhotonPeerListener
    {
        /// <summary>
        /// Peer
        /// </summary>
        private PhotonPeer Peer = null;

        #region Singleton
        public static ConnectionClient Instance { get { return _Instance; } }
        private static ConnectionClient _Instance = null;
        #endregion

        /// <summary>
        /// 接続状態
        /// </summary>
        private static ReactiveProperty<StatusCode> ConnectionStatus = new ReactiveProperty<StatusCode>(StatusCode.Disconnect);

        /// <summary>
        /// 接続状態が更新された
        /// </summary>
        public static IObservable<StatusCode> OnConnectionStatusChanged { get { return ConnectionStatus; } }

        /// <summary>
        /// イベント受信時のSubject
        /// </summary>
        private static Subject<EventPacket> OnRecvEventSubject = new Subject<EventPacket>();

        /// <summary>
        /// イベントを受信した
        /// </summary>
        /// <value></value>
        public static IObservable<EventPacket> OnRecvEvent { get { return OnRecvEventSubject; } }

        /// <summary>
        /// レスポンス受信時のSubject
        /// </summary>
        private static Subject<OperationPacket> OnRecvResponseSubject = new Subject<OperationPacket>();

        /// <summary>
        /// レスポンスを受信した
        /// </summary>
        public static IObservable<OperationPacket> OnRecvResponse { get { return OnRecvResponseSubject; } }

        void Awake()
        {
            GameObject.DontDestroyOnLoad(gameObject);
            _Instance = this;
            RegisterCustomClasses();
        }

        void Update()
        {
            if (Peer != null)
            {
                Peer.Service();
            }
        }

        /// <summary>
        /// 接続
        /// </summary>
        public void Connect()
        {
            if (Peer != null) { return; }

            Peer = new PhotonPeer(this, ConnectionProtocol.Tcp);
            if (!Peer.Connect("127.0.0.1:4530", "TestServer"))
            {
                DebugReturn(DebugLevel.ERROR, "Connect Failed...");
                return;
            }
        }

        /// <summary>
        /// リクエスト送信
        /// </summary>
        /// <param name="Packet">パケット</param>
        public void SendRequest(OperationPacket Packet)
        {
            if (!Peer.OpCustom(Packet.SendCode, Packet.SendParamsDictionary, false))
            {
                DebugReturn(DebugLevel.ERROR, string.Format("SendRequest Failed. Code:{0}", Packet.Code.ToString()));
            }
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            string Message = string.Format("Level:{0} {1}", level.ToString(), message);
            if (level != DebugLevel.ERROR)
            {
                Debug.Log(Message);
            }
            else
            {
                Debug.LogError(Message);
            }
        }

        public void OnEvent(EventData eventData)
        {
            EventPacket Packet = new EventPacket(eventData.Code, eventData.Parameters);
            OnRecvEventSubject.OnNext(Packet);
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
            OperationPacket Packet = new OperationPacket(operationResponse.OperationCode, operationResponse.Parameters);
            OnRecvResponseSubject.OnNext(Packet);
        }

        public void OnStatusChanged(StatusCode statusCode)
        {
            switch (statusCode)
            {
                case StatusCode.Connect:

                    DebugReturn(DebugLevel.INFO, "Connection Success!!");
                    break;
            }
            ConnectionStatus.Value = statusCode;
        }

        /// <summary>
        /// カスタムクラスの登録
        /// </summary>
        private void RegisterCustomClasses()
        {
            PhotonPeer.RegisterType(typeof(Vector3), 1, SerializeMethods.SerializeVector3, SerializeMethods.DeserializeVector3);
            PhotonPeer.RegisterType(typeof(PacketPlayerList), 2, PacketPlayerList.SerializeObject, PacketPlayerList.DeserializeObject);
            PhotonPeer.RegisterType(typeof(CharacterData), 3, CharacterData.SerializeObject, CharacterData.DeserializeObject);
            PhotonPeer.RegisterType(typeof(Vec3), 4, Vec3.SerializeObject, Vec3.DeserializeObject);
        }
    }
}
