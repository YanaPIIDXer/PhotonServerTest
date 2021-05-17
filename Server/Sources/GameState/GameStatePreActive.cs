using Common.Packet;
using Common.Stream;
using UnityEngine;

namespace GameState
{
    /// <summary>
    /// ゲームステート：準備中
    /// </summary>
    public class GameStatePreActive : GameState
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Parent">親</param>
        public GameStatePreActive(GamePeer Parent)
            : base(Parent)
        {
            AddOperationHandler(EPacketID.ClientReady, OnRecvClientReady);
        }

        /// <summary>
        /// クライアントの準備完了を受信した
        /// </summary>
        /// <param name="Stream">ストリーム</param>
        /// <returns>レスポンスパケット</returns>
        private IPacket OnRecvClientReady(IDictionaryStream Stream)
        {
            var Request = new PacketClientReady();
            Request.Serialize(Stream);

            // とりあえずダミーデータでも投げる
            var Id = Parent.ConnectionId;
            var Pos = new Vec3(1.0f, 0.0f, -3.0f);
            var Response = new PacketServerReady(Id, Pos);

            Parent.SetNextState(new GameStateActive(Parent));
            return Response;
        }
    }
}