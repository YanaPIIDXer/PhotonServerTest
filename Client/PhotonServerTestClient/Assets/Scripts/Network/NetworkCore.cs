﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using UniRx;
using System;
using Common.Code;

namespace Game.Network
{
    using PacketParam = Dictionary<byte, object>;

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
        /// イベントに対応したSubjectを保持するDictionary
        /// </summary>
        private Dictionary<byte, Subject<PacketParam>> EventDic = new Dictionary<byte, Subject<PacketParam>>();

        /// <summary>
        /// レスポンスのハンドラを保持するDictionary
        /// </summary>
        private Dictionary<byte, Action<PacketParam>> ResponseHandlers = new Dictionary<byte, Action<PacketParam>>();

        /// <summary>
        /// イベントに対応したObservable
        /// </summary>
        /// <param name="Code">イベントコード</param>
        /// <returns>Observable</returns>
        public IObservable<PacketParam> OnEventObservable(EEventCode Code)
        {
            byte ByteCode = (byte)Code;
            if (!EventDic.ContainsKey(ByteCode))
            {
                EventDic[ByteCode] = new Subject<PacketParam>();
            }
            return EventDic[ByteCode];
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
        /// <param name="Code">オペレーションコード</param>
        /// <param name="Params">パラメータ</param>
        /// <param name="ResponseHandler">リクエストに対応するレスポンスのハンドラ</param>
        public void SendRequest(EOperationCode Code, PacketParam Params, Action<PacketParam> ResponseHandler)
        {
            if (Peer == null)
            {
                Debug.LogError("Peer is null.");
                return;
            }
            byte ByteCode = (byte)Code;
            ResponseHandlers.Add(ByteCode, ResponseHandler);
            Peer.OpCustom(ByteCode, Params, false);
        }

        /// <summary>
        /// リクエスト送信
        /// </summary>
        /// <param name="Code">オペレーションコード</param>
        /// <param name="Params">パラメータ</param>
        /// <param name="ResponseHandler">リクエストに対応するレスポンスのハンドラ</param>
        public void SendRequest(EOperationCode Code, Action<PacketParam> ResponseHandler)
        {
            SendRequest(Code, new PacketParam(), ResponseHandler);
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
            byte Code = operationResponse.OperationCode;
            if (!ResponseHandlers.ContainsKey(Code)) { throw new Exception(string.Format("{0} に対応するハンドラがない", ((EOperationCode)Code).ToString())); }
            ResponseHandlers[Code]?.Invoke(operationResponse.Parameters);
            ResponseHandlers.Remove(Code);
        }

        /// <summary>
        /// レスポンスの伴わないパケットを送信
        /// </summary>
        /// <param name="Code">オペレーションコード</param>
        /// <param name="Params">パラメータ</param>
        public void SendReport(EOperationCode Code, PacketParam Params)
        {
            if (Peer == null)
            {
                Debug.LogError("Peer is null.");
                return;
            }
            Peer.OpCustom((byte)Code, Params, false);
        }

        /// <summary>
        /// レスポンスの伴わないパケットを送信
        /// </summary>
        /// <param name="Code">オペレーションコード</param>
        public void SendReport(EOperationCode Code)
        {
            SendReport(Code, new PacketParam());
        }

        public void OnEvent(EventData eventData)
        {
            byte Code = eventData.Code;
            if (!EventDic.ContainsKey(Code)) { return; }        // イベント購読者がいない
            EventDic[Code].OnNext(eventData.Parameters);
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