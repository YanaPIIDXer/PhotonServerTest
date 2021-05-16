using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using UniRx;
using System;

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

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
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

        public void OnOperationResponse(OperationResponse operationResponse)
        {
        }

        public void OnStatusChanged(StatusCode statusCode)
        {
            OnNetworkStatusChangedSubject.OnNext(statusCode);
        }

        public void OnEvent(EventData eventData)
        {
        }
    }
}
