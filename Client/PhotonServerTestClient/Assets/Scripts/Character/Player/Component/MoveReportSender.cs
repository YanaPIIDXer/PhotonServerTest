using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character.Component;
using UniRx;
using System;
using UniRx.Operators;
using Common.Packet;
using Game.Network;

namespace Game.Character.Player.Component
{
    /// <summary>
    /// サーバへの移動通知
    /// </summary>
    public class MoveReportSender : CharacterComponent
    {
        /// <summary>
        /// 以前の座標
        /// </summary>
        private ReactiveProperty<Vector3> PrevPosition = new ReactiveProperty<Vector3>();

        /// <summary>
        /// Transform
        /// </summary>
        private Transform Trans = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MoveReportSender()
        {
            PrevPosition
                .Skip(1)
                .ThrottleFirst(TimeSpan.FromSeconds(3.0))
                .Subscribe((Pos) =>
                {
                    var Packet = new PacketPlayerMove(Pos.ToVec3());
                    NetworkCore.Instance.SendReport(Packet);
                });
        }

        /// <summary>
        /// 初期化された
        /// </summary>
        protected override void OnIntiialize()
        {
            Trans = GetMonoBehaviourComponent<Transform>();
        }

        /// <summary>
        /// OnUpdate
        /// </summary>
        public override void OnUpdate()
        {
            PrevPosition.Value = Trans.position;
        }
    }
}
