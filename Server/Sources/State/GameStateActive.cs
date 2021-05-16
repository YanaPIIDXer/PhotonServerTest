using System;
using System.Reactive.Linq;
using Common.Packet;
using Common.Code;
using UnityEngine;

namespace State
{
    /// <summary>
    /// ゲーム中のゲームステートクラス
    /// </summary>
    public class GameStateActive : GameState
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GameStateActive(GamePeer Peer)
            : base(Peer)
        {
            Peer.OnRecvRequest
                .Where((Packet) => Packet.Code == EOperationCode.Move)
                .Subscribe(OnCharacterMove);
        }

        /// <summary>
        /// キャラが移動した
        /// </summary>
        /// <param name="Packet">パケット</param>
        private void OnCharacterMove(OperationPacket Packet)
        {
            var Info = Packet.GetParam<PacketPlayerMove>(0);
            Peer.PlayerCharacter.Move(Info.Position.ToVector3());
        }
    }
}
