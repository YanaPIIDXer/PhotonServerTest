﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character.Component;
using UniRx;
using UniRx.Operators;
using System;
using Common.Packet;
using Game.Networking;
using Common.Code;

namespace Game.Character.Player.Component
{
    /// <summary>
    /// 移動パケット送信
    /// </summary>
    public class MovePacketSender : CharacterComponent
    {
        /// <summary>
        /// Transform
        /// </summary>
        private Transform Trans = null;

        /// <summary>
        /// 以前の座標
        /// </summary>
        private ReactiveProperty<Vector3> PrevPosition = new ReactiveProperty<Vector3>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MovePacketSender()
        {
            PrevPosition
                .ThrottleFirst(TimeSpan.FromSeconds(3))
                .Skip(1)
                .Subscribe((Pos) =>
                {
                    OperationPacket Packet = new OperationPacket(EOperationCode.Move);
                    Packet.SetParam(0, Pos);
                });
        }

        /// <summary>
        /// 初期化された
        /// </summary>
        protected override void OnInitialize()
        {
            Trans = GetMonoBehaviourComponent<Transform>();
        }

        /// <summary>
        /// Update
        /// </summary>
        public override void OnUpdate()
        {
            // Trans.positionの値をそのままReacrivePropertyに渡すと、
            // float型の誤差の影響で変化していないにも関わらず「変化した」と見做される。
            // （float型同士を == で比較してはならないのと同じ）
            // その為、各要素の小数点第二位以下を切り捨てて扱うものとする。
            var Pos = Trans.position;
            Pos.x = Mathf.Floor(Pos.x * 10) * 0.1f;
            Pos.y = Mathf.Floor(Pos.y) * 0.1f;
            Pos.z = Mathf.Floor(Pos.z * 10) / 0.1f;
            PrevPosition.Value = Pos;
        }
    }
}
