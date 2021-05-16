using UnityEngine;

namespace Character.Player
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    public partial class Player : Character
    {
        /// <summary>
        /// 所有者
        /// </summary>
        private GamePeer Peer = null;

        /// <summary>
        /// ID
        /// </summary>
        public override int Id => Peer.ConnectionId;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Peer">所有者</param>
        public Player(GamePeer Peer)
        {
            this.Peer = Peer;
        }

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="ToPosition">移動先</param>
        public void Move(Vector3 ToPosition)
        {
            Position = ToPosition;
            OnMovedSubject.OnNext(Position);
        }
    }
}