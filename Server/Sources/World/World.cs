using System.Collections.Generic;
using System;
using Common.Packet;
using UnityEngine;

namespace World
{
    /// <summary>
    /// ワールドクラス
    /// </summary>
    public class World
    {
        /// <summary>
        /// Peerリスト
        /// </summary>
        private List<GamePeer> Peers = new List<GamePeer>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public World()
        {
        }

        /// <summary>
        /// Peer追加
        /// </summary>
        /// <param name="Peer">Peer</param>
        public void AddPeer(GamePeer Peer)
        {
            var EnterPacket = new PacketPlayerEnter(Peer.PlayerCharacter.Id, Peer.PlayerCharacter.Position.ToVec3());
            BroadcastEvent(EnterPacket);

            var List = new FlexArray<CharacterData>();
            foreach (var Other in Peers)
            {
                var Pos = Other.PlayerCharacter.Position.ToVec3();
                var Data = new CharacterData(Other.PlayerCharacter.Id, Pos);
                List.Add(Data);
            }
            var ListPacket = new PacketPlayerList(List);
            Peer.SendEventPacket(ListPacket);

            Peers.Add(Peer);

            Peer.OnDisconnected.Subscribe((_) => PlayerLeave(Peer));
            Peer.PlayerCharacter.OnMoved.Subscribe((Pos) => PlayerMoved(Peer, Pos));
        }

        /// <summary>
        /// プレイヤーが移動した
        /// </summary>
        /// <param name="Peer">Peer</param>
        /// <param name="Position">座標</param>
        private void PlayerMoved(GamePeer Peer, Vector3 Position)
        {
            var MovePacket = new PacketOtherPlayerMove(Peer.PlayerCharacter.Id, Position.ToVec3());
            BroadcastEvent(MovePacket, Peer.ConnectionId);
        }

        /// <summary>
        /// プレイヤーが退場した
        /// </summary>
        /// <param name="Peer">Peer</param>
        private void PlayerLeave(GamePeer Peer)
        {
            var Id = Peer.PlayerCharacter.Id;
            Peers.Remove(Peer);
            var LeavePacket = new PacketPlayerLeave(Id);
            BroadcastEvent(LeavePacket);
        }

        /// <summary>
        /// イベントのブロードキャスト
        /// </summary>
        /// <param name="SendPacket">パケット</param>
        /// <param name="IgnoreId">無視するID</param>
        private void BroadcastEvent(Packet SendPacket, int IgnoreId = -1)
        {
            foreach (var Peer in Peers)
            {
                if (Peer.ConnectionId != IgnoreId)
                {
                    Peer.SendEventPacket(SendPacket);
                }
            }
        }
    }
}
