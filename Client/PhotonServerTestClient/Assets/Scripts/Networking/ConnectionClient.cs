using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

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

        void Awake()
        {
            GameObject.DontDestroyOnLoad(gameObject);
            _Instance = this;
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
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
        }

        public void OnStatusChanged(StatusCode statusCode)
        {
            switch (statusCode)
            {
                case StatusCode.Connect:

                    DebugReturn(DebugLevel.INFO, "Connection Success!!");
                    break;
            }
        }
    }
}
