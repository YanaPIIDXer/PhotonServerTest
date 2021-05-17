using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character.Component;
using Game.ControlInput;
using UniRx;

namespace Game.Character.Player.Component
{
    /// <summary>
    /// 操作キャラの移動
    /// </summary>
    public class LocalPlayerMovement : CharacterComponent
    {
        /// <summary>
        /// 現在の入力値
        /// </summary>
        private Vector2 CurrentInput = Vector2.zero;

        /// <summary>
        /// Rigidbody
        /// </summary>
        private Rigidbody Body = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="MoveInput">移動入力インタフェース</param>
        public LocalPlayerMovement(IMoveInput MoveInput)
        {
            MoveInput.OnInputMove
                .Subscribe((Value) => CurrentInput = Value);
        }

        /// <summary>
        /// 初期化された
        /// </summary>
        protected override void OnIntiialize()
        {
            Body = GetMonoBehaviourComponent<Rigidbody>();
        }

        /// <summary>
        /// FixedUpdate
        /// </summary>
        public override void OnFixedUpdate()
        {
            Body.velocity = new Vector3(CurrentInput.x, 0.0f, CurrentInput.y);
        }
    }
}
