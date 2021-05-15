namespace Character.Player
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    public class Player : Character
    {
        /// <summary>
        /// 所有者
        /// </summary>
        private GamePeer Peer = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Peer">所有者</param>
        public Player(GamePeer Peer)
        {
            this.Peer = Peer;
        }
    }
}