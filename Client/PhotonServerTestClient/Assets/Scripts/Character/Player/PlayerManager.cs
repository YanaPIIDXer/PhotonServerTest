using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Networking;
using System;
using UniRx;
using Common.Code;

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

                    // HACK:GameSequence.csからのコピペ
                    //      Prefabを管理するクラスを作りたい
                    var PlayerPrefab = Resources.Load<GameObject>("Prefabs/System/Player");
                    Debug.Assert(PlayerPrefab != null, "Player Prefab Load Failed.");

                    var PlayerObj = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
                    Debug.Assert(PlayerObj != null, "Player Instantiate Failed.");

                    var Player = PlayerObj.GetComponent<Player>();
                    Players.Add(Id, Player);
                });

            ConnectionClient.OnRecvEvent
                .Where((Packet) => Packet.Code == EEventCode.PlayerLeave)
                .Subscribe((Packet) =>
                {
                    var Id = Packet.GetParam<int>(0);
                    Destroy(Players[Id].gameObject);
                    Players.Remove(Id);
                });
        }
    }
}
