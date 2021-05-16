using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Networking;
using System;
using UniRx;
using Common.Code;
using Common.Packet;

namespace Game.Character.Player
{
    /// <summary>
    /// プレイヤーマネージャ
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        /// <summary>
        /// プレイヤーDictionary
        /// </summary>
        private Dictionary<int, Player> Players = new Dictionary<int, Player>();

        void Awake()
        {
            ConnectionClient.OnRecvEvent
                .Where((Packet) => Packet.Code == EEventCode.PlayerEnter)
                .Subscribe((Packet) =>
                {
                    var Id = Packet.GetParam<int>(0);
                    SpawnPlayer(Id);
                }).AddTo(gameObject);

            ConnectionClient.OnRecvEvent
                .Where((Packet) => Packet.Code == EEventCode.PlayerList)
                .Subscribe((Packet) =>
                {
                    PacketPlayerList List = Packet.GetParam<PacketPlayerList>(0);
                    foreach (var Data in List.List)
                    {
                        SpawnPlayer(Data.Id);
                        Players[Data.Id].transform.position = Data.Position.ToVector3();  // いいのかこれｗ
                    }
                }).AddTo(gameObject);

            ConnectionClient.OnRecvEvent
                .Where((Packet) => Packet.Code == EEventCode.PlayerMove)
                .Subscribe((Packet) =>
                {
                    var Id = Packet.GetParam<int>(0);
                    var Pos = Packet.GetParam<Vector3>(1);
                    Players[Id].RecvMove(Pos);
                }).AddTo(gameObject);

            ConnectionClient.OnRecvEvent
                .Where((Packet) => Packet.Code == EEventCode.PlayerLeave)
                .Subscribe((Packet) =>
                {
                    var Id = Packet.GetParam<int>(0);
                    Destroy(Players[Id].gameObject);
                    Players.Remove(Id);
                }).AddTo(gameObject);
        }

        /// <summary>
        /// プレイヤー生成
        /// </summary>
        /// <param name="Id">ID</param>
        private void SpawnPlayer(int Id)
        {
            // HACK:GameSequence.csからのコピペ
            //      Prefabを管理するクラスを作りたい
            var PlayerPrefab = Resources.Load<GameObject>("Prefabs/System/Player");
            Debug.Assert(PlayerPrefab != null, "Player Prefab Load Failed.");

            var PlayerObj = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
            Debug.Assert(PlayerObj != null, "Player Instantiate Failed.");

            var Player = PlayerObj.GetComponent<Player>();
            Player.SetupRemoteMovementComponent();
            Players.Add(Id, Player);
        }
    }
}
