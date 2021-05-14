using System;
using Common.Packet;

namespace State
{
    /// <summary>
    /// タイトルステート
    /// </summary>
    public class GameStateTitle : GameState
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GameStateTitle()
        {
        }

        protected override void Initialize(IObservable<OperationPacket> PacketObservable)
        {
        }
    }
}
