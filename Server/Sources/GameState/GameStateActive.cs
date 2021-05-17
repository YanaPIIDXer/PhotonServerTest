namespace GameState
{
    /// <summary>
    /// ゲームステート：プレイ中
    /// </summary>
    public class GameStateActive : GameState
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Parent">親</param>
        public GameStateActive(GamePeer Parent)
            : base(Parent)
        {
        }
    }
}