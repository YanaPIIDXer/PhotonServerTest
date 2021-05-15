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
        public GameStateTitle()
        {
        }

        protected override void Initialize(IObservable<OperationPacket> PacketObservable)
        {
            PacketObservable
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
            SendResponse(Response);

            ChangeState<GameStateActive>();
        }
    }
}
