using System.Collections.Generic;
using System;
using Common.Packet;
using Common.Code;

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
            Peers.Add(Peer);
            Peer.OnDisconnected.Subscribe((_) => Peers.Remove(Peer));
            Peer.PlayerCharacter.OnMoved.Subscribe((Pos) =>
            {
                EventPacket Packet = new EventPacket(EEventCode.PlayerMove);
                Packet.SetParam(0, Peer.PlayerCharacter.Id);
                Packet.SetParam(1, Pos);
                BroadcastEvent(Packet, Peer.ConnectionId);
            });
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
