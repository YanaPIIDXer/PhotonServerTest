using Common.Packet;
using Common.Stream;

namespace GameState
{
    /// <summary>
    /// ゲームステート：タイトル
    /// </summary>
    public class GameStateTitle : GameState
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Parent">親</param>
        public GameStateTitle(GamePeer Parent)
            : base(Parent)
        {
            AddOperationHandler(EPacketID.LogInRequest, OnRecvLogInRequest);
        }

        /// <summary>
        /// ログインリクエスト受信
        /// </summary>
        /// <param name="Stream">ストリーム</param>
        /// <returns>結果パケット</returns>
        private IPacket OnRecvLogInRequest(IDictionaryStream Stream)
        {
            PacketLogInRequest Request = new PacketLogInRequest();
            Request.Serialize(Stream);
            // 今のところ問答無用
            PacketLogInResult Result = new PacketLogInResult();
            return Result;
        }
    }
}
