using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character.Component;
using System;

namespace Game.Character.Player.Component
{
    /// <summary>
    /// 自分が操作するキャラの移動
    /// </summary>
    public class LocalPlayerMovement : CharacterComponent
    {
        /// <summary>
        /// 入力ベクトル
        /// </summary>
        public Vector2 InputVector { private get; set; }

        /// <summary>
        /// Rigidbody
        /// </summary>
        private Rigidbody Body = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LocalPlayerMovement()
        {
        }

        /// <summary>
        /// 初期化された
        /// </summary>
        protected override void OnInitialize()
        {
            Body = GetMonoBehaviourComponent<Rigidbody>();
        }

        /// <summary>
        /// FixedUpdate
        /// </summary>
        public override void OnFixedUpdate()
        {
            Body.velocity = new Vector3(InputVector.x, 0.0f, InputVector.y);
        }
    }
}
