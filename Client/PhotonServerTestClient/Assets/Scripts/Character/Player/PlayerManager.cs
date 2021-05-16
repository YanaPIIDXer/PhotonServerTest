using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Networking;
using System;
using UniRx;
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
            ConnectionClient.AddPacketHandler(EPacketID.PlayerEnter, (Obj) =>
            {
                var Packet = (PacketPlayerEnter)Obj;
                SpawnPlayer(Packet.Id);
                Players[Packet.Id].transform.position = Packet.Position.ToVector3();  // いいのかこれｗ
            });

            ConnectionClient.AddPacketHandler(EPacketID.PlayerList, (Obj) =>
            {
                var Packet = (PacketPlayerList)Obj;
                foreach (var Data in Packet.List)
                {
                    SpawnPlayer(Data.Id);
                    Players[Data.Id].transform.position = Data.Position.ToVector3();  // いいのかこれｗ
                }
            });

            ConnectionClient.AddPacketHandler(EPacketID.OtherPlayerMove, (Obj) =>
            {
                var Packet = (PacketOtherPlayerMove)Obj;
                var Id = Packet.Id;
                var Pos = Packet.Position.ToVector3();
                Players[Id].RecvMove(Pos);
            });

            ConnectionClient.AddPacketHandler(EPacketID.PlayerLeave, (Obj) =>
            {
                var Packet = (PacketPlayerLeave)Obj;
                Destroy(Players[Packet.Id].gameObject);
                Players.Remove(Packet.Id);
            });
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
