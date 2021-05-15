using System;
using Common.Packet;

namespace State
{
    /// <summary>
    /// ゲームステート基底クラス
    /// </summary>
    public abstract class GameState
    {
        /// <summary>
        /// Peer
        /// </summary>
        protected GamePeer Peer = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Peer">Peer</param>
        public GameState(GamePeer Peer)
        {
            this.Peer = Peer;
        }
    }
}
