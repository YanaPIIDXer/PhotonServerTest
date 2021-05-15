using System;
using System.Reactive.Linq;
using Common.Packet;

namespace State
{
    /// <summary>
    /// ゲーム中のゲームステートクラス
    /// </summary>
    public class GameStateActive : GameState
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GameStateActive(GamePeer Peer)
            : base(Peer)
        {
        }
    }
}
