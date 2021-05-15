using System.Collections.Generic;
using System;

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
        }
    }
}
