using System.Collections.Generic;
using System;
using Common.Packet;
using Common.Code;
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
            EventPacket EnterPacket = new EventPacket(EEventCode.PlayerEnter);
            EnterPacket.SetParam(0, Peer.PlayerCharacter.Id);
            BroadcastEvent(EnterPacket);

            PacketPlayerList List = new PacketPlayerList();
            foreach (var Other in Peers)
            {
                var Pos = Other.PlayerCharacter.Position.ToVec3();
                var Data = new CharacterData(Other.PlayerCharacter.Id, Pos);
                List.List.Add(Data);
            }
            EventPacket ListPacket = new EventPacket(EEventCode.PlayerList);
            ListPacket.SetParam(0, List);
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
            EventPacket Packet = new EventPacket(EEventCode.PlayerMove);
            Packet.SetParam(0, Peer.PlayerCharacter.Id);
            Packet.SetParam(1, Position);
            BroadcastEvent(Packet, Peer.ConnectionId);
        }

        /// <summary>
        /// プレイヤーが退場した
        /// </summary>
        /// <param name="Peer">Peer</param>
        private void PlayerLeave(GamePeer Peer)
        {
            var Id = Peer.PlayerCharacter.Id;
            Peers.Remove(Peer);
            EventPacket Packet = new EventPacket(EEventCode.PlayerLeave);
            Packet.SetParam(0, Id);
            BroadcastEvent(Packet);
        }

        /// <summary>
        /// イベントのブロードキャスト
        /// </summary>
        /// <param name="Packet">パケット</param>
        /// <param name="IgnoreId">無視するID</param>
        private void BroadcastEvent(EventPacket Packet, int IgnoreId = -1)
        {
            foreach (var Peer in Peers)
            {
                if (Peer.ConnectionId != IgnoreId)
                {
                    Peer.SendEventPacket(Packet);
                }
            }
        }
    }
}
