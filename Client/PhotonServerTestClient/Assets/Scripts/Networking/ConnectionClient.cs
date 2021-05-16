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
        /// パケットを受信した
        /// </summary>
        /// <param name="RecvPacket">受信したパケット</param>
        public delegate void OnRecvPacket(object RecvPacket);

        /// <summary>
        /// パケットハンドラ
        /// </summary>
        /// <typeparam name="EPacketID">パケットＩＤ</typeparam>
        /// <typeparam name="OnRecvPackett">ハンドリング用delegate</typeparam>
        private static Dictionary<EPacketID, OnRecvPacket> PacketHandlers = new Dictionary<EPacketID, OnRecvPacket>();

        void Awake()
        {
            GameObject.DontDestroyOnLoad(gameObject);
            _Instance = this;
            RegisterPacketType.RegisterPackets((customType, code, serializeMethod, deserializeMethod) =>
            {
                return PhotonPeer.RegisterType(customType, code, serializeMethod.Invoke, deserializeMethod.Invoke);
            });
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
        /// パケットハンドラ追加
        /// </summary>
        /// <param name="ID">パケットＩＤ</param>
        /// <param name="Handler">ハンドリング用delegate</param>
        public static void AddPacketHandler(EPacketID ID, OnRecvPacket Handler)
        {
            if (!PacketHandlers.ContainsKey(ID))
            {
                PacketHandlers.Add(ID, Handler);
            }
            else
            {
                PacketHandlers[ID] += Handler;
            }
        }

        /// <summary>
        /// リクエスト送信
        /// </summary>
        /// <param name="SendPacket">パケット</param>
        public void SendRequest(Packet SendPacket)
        {
            var Data = SendPacket.MakeSendData();
            if (!Peer.OpCustom(Data.SendCode, Data.SendDictionary, false))
            {
                DebugReturn(DebugLevel.ERROR, string.Format("SendRequest Failed. Code:{0}", Data.SendCode));
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
            EPacketID Id = (EPacketID)eventData.Code;
            if (PacketHandlers.ContainsKey(Id))
            {
                PacketHandlers[Id].Invoke(eventData.Parameters[0]);
            }
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
            EPacketID Id = (EPacketID)operationResponse.OperationCode;
            if (PacketHandlers.ContainsKey(Id))
            {
                PacketHandlers[Id].Invoke(operationResponse.Parameters[0]);
            }
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
    }
}
