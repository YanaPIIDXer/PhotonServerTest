using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using UniRx;
using System;
using Common.Packet;
using Common.Stream;

namespace Game.Network
{
    /// <summary>
    /// ネットワークコア
    /// </summary>
    public class NetworkCore : MonoBehaviour, IPhotonPeerListener
    {
        /// <summary>
        /// Prefabのパス
        /// </summary>
        private static readonly string PrefabPath = "Prefabs/System/NetworkCore";

        /// <summary>
        /// インスタンス
        /// </summary>
        public static NetworkCore Instance
        {
            get
            {
                if (_Instance == null)
                {
                    var Prefab = Resources.Load<GameObject>(PrefabPath);
                    Debug.Assert(Prefab != null, "NetworkCore Prefab Load Failed.");

                    var Obj = Instantiate<GameObject>(Prefab);
                    Debug.Assert(Obj != null, "NetworkCore Instantiate Failed.");

                    _Instance = Obj.GetComponent<NetworkCore>();
                }
                return _Instance;
            }
        }

        private static NetworkCore _Instance = null;

        /// <summary>
        /// Peer
        /// </summary>
        private PhotonPeer Peer = null;

        /// <summary>
        /// 接続状態が変わった時のSubject
        /// </summary>
        private Subject<StatusCode> OnNetworkStatusChangedSubject = new Subject<StatusCode>();

        /// <summary>
        /// 通信状態が変わった
        /// </summary>
        public IObservable<StatusCode> OnNetworkStatusChanged { get { return OnNetworkStatusChangedSubject; } }

        /// <summary>
        /// イベントに対応したActionを保持するDictionary
        /// </summary>
        private Dictionary<byte, Action<IDictionaryStream>> EventDic = new Dictionary<byte, Action<IDictionaryStream>>();

        /// <summary>
        /// レスポンスのハンドラを保持するDictionary
        /// </summary>
        private Dictionary<byte, Action<IDictionaryStream>> ResponseHandlers = new Dictionary<byte, Action<IDictionaryStream>>();

        /// <summary>
        /// イベントのハンドラを追加
        /// </summary>
        /// <param name="PacketID">パケットＩＤ</param>
        /// <param name="Handler">ハンドラ</param>
        public void AddEventHandler(EPacketID PacketID, Action<IDictionaryStream> Handler)
        {
            EventDic.Add((byte)PacketID, Handler);
        }

        /// <summary>
        /// 接続
        /// </summary>
        /// <returns>成功したらtrue</returns>
        public bool Connect()
        {
            if (Peer != null)
            {
                Disconnect();
            }

            Peer = new PhotonPeer(this, ConnectionProtocol.Tcp);
            return Peer.Connect("127.0.0.1:4580", "TestServer");
        }

        /// <summary>
        /// 切断
        /// </summary>
        public void Disconnect()
        {
            if (Peer != null)
            {
                Peer.Disconnect();
                Peer = null;
            }
        }

        /// <summary>
        /// リクエスト送信
        /// </summary>
        /// <param name="SendPacket">送信パケット</param>
        /// <param name="ResponsePacketID">レスポンスパケットのＩＤ</param>
        /// <param name="ResponseHandler">リクエストに対応するレスポンスのハンドラ</param>
        public void SendRequest(IPacket SendPacket, EPacketID ResponsePacketID, Action<IDictionaryStream> ResponseHandler)
        {
            if (Peer == null)
            {
                Debug.LogError("Peer is null.");
                return;
            }
            byte ByteID = (byte)ResponsePacketID;
            ResponseHandlers.Add(ByteID, ResponseHandler);

            DictionaryStreamWriter Writer = new DictionaryStreamWriter();
            SendPacket.Serialize(Writer);
            Peer.OpCustom((byte)SendPacket.PacketID, Writer.Dest, false);
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
            byte PacketID = operationResponse.OperationCode;
            if (!ResponseHandlers.ContainsKey(PacketID)) { throw new Exception(string.Format("{0} に対応するハンドラがない", ((EPacketID)PacketID).ToString())); }

            DictionaryStreamReader Reader = new DictionaryStreamReader(operationResponse.Parameters);
            ResponseHandlers[PacketID]?.Invoke(Reader);
            ResponseHandlers.Remove(PacketID);
        }

        /// <summary>
        /// レスポンスの伴わないパケットを送信
        /// </summary>
        /// <param name="SendPacket">パケット</param>
        public void SendReport(IPacket SendPacket)
        {
            if (Peer == null)
            {
                Debug.LogError("Peer is null.");
                return;
            }
            DictionaryStreamWriter Writer = new DictionaryStreamWriter();
            SendPacket.Serialize(Writer);
            Peer.OpCustom((byte)SendPacket.PacketID, Writer.Dest, false);
        }

        public void OnEvent(EventData eventData)
        {
            byte Code = eventData.Code;
            if (!EventDic.ContainsKey(Code)) { return; }        // イベント購読者がいない

            DictionaryStreamReader Reader = new DictionaryStreamReader(eventData.Parameters);
            EventDic[Code]?.Invoke(Reader);
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            if (level != DebugLevel.ERROR)
            {
                Debug.Log("[" + level.ToString() + "]:" + message);
            }
            else
            {
                Debug.LogError("[" + level.ToString() + "]:" + message);
            }
        }

        public void OnStatusChanged(StatusCode statusCode)
        {
            OnNetworkStatusChangedSubject.OnNext(statusCode);
        }

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            RegisterPacketType.RegisterPackets((customType, code, serializeMethod, deserializeMethod) =>
            {
                return PhotonPeer.RegisterType(customType, code, (arg) => serializeMethod?.Invoke(arg), (arg) => deserializeMethod?.Invoke(arg));
            });
        }

        void Update()
        {
            if (Peer != null)
            {
                Peer.Service();
            }
        }

    }
}
