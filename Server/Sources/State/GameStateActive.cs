using System;
using System.Reactive.Linq;
using Common.Packet;
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
            Peer.AddRequestHandler(EPacketID.PlayerMove, OnCharacterMove);
        }

        /// <summary>
        /// キャラが移動した
        /// </summary>
        /// <param name="Obj">パケット</param>
        private void OnCharacterMove(object Obj)
        {
            var Packet = (PacketPlayerMove)Obj;
            Peer.PlayerCharacter.Move(Packet.Position.ToVector3());
        }
    }
}
