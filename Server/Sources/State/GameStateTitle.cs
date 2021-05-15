using System;
using System.Reactive.Linq;
using Common.Packet;
using Common.Code;
using System.Collections.Generic;

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
        public GameStateTitle(GamePeer Peer)
            : base(Peer)
        {
            Peer.OnRecvRequest
                .Where((Packet) => Packet.Code == EOperationCode.LogIn)
                .Subscribe((Packet) => OnRecvLogInRequest(Packet));
        }

        /// <summary>
        /// ログインリクエスト受信
        /// </summary>
        /// <param name="Packet">パケット</param>
        private void OnRecvLogInRequest(OperationPacket Packet)
        {
            OperationPacket Response = new OperationPacket(EOperationCode.LogIn);
            Peer.SendResponsePacket(Response);

            Peer.ToActiveState();
        }
    }
}
