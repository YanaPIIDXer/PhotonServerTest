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
        public GameState()
        {
        }

        /// <summary>
        /// Peerをセット
        /// </summary>
        /// <param name="Peer">Peer</param>
        public void SetPeer(GamePeer Peer)
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

        /// <summary>
        /// ステート切り替え
        /// </summary>
        /// <typeparam name="T">ステートクラスの型</typeparam>
        /// <return>ステート</return>
        protected T ChangeState<T>()
             where T : GameState, new()
        {
            T NextState = new T();
            NextState.SetPeer(Peer);
            return NextState;
        }
    }
}
