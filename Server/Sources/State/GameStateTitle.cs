using System;
using System.Reactive.Linq;
using Common.Packet;
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
            Peer.AddRequestHandler(EPacketID.LogInRequest, OnRecvLogInRequest);
            Peer.AddRequestHandler(EPacketID.ClientReady, (_) => Peer.ToActiveState());
        }

        /// <summary>
        /// ログインリクエスト受信
        /// </summary>
        /// <param name="Obj">パケット</param>
        private void OnRecvLogInRequest(object Obj)
        {
            var Request = (PacketLogInRequest)Obj;
            // 今のところは特に何もしていない

            var Response = new PacketLogInResult();
            Peer.SendResponsePacket(Response);
        }
    }
}
