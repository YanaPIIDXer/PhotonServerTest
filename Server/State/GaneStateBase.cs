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
        private GamePeer Peer = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GameState(GamePeer Peer)
        {
            this.Peer = Peer;
            Initialize(Peer.OnRecvRequest);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="PacketObservable">リクエスト受信Observable</param>
        protected abstract void Initialize(IObservable<OperationPacket> PacketObservable);

        /// <summary>
        /// イベント送信
        /// </summary>
        /// <param name="Packet">パケット</param>
        protected void SendEvent(EventPacket Packet)
        {
            Peer.SendEventPacket(Packet);
        }

        /// <summary>
        /// レスポンス送信
        /// </summary>
        /// <param name="Packet">パケット</param>
        protected void SendResponse(OperationPacket Packet)
        {
            Peer.SendResponsePacket(Packet);
        }
    }
}
