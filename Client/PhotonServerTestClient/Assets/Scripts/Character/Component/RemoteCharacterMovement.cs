using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character.Component
{
    /// <summary>
    /// 他人の移動
    /// </summary>
    public class RemoteCharacterMovement : CharacterComponent
    {
        /// <summary>
        /// 移動にかかる時間
        /// </summary>
        private static readonly float MoveTime = 3.0f;

        /// <summary>
        /// 移動ベクトル
        /// </summary>
        private Vector3 PrevPosition = Vector3.zero;

        /// <summary>
        /// 行先
        /// </summary>
        private Vector3 Destination = Vector3.zero;

        /// <summary>
        /// Transform
        /// </summary>
        private Transform Trans = null;

        /// <summary>
        /// 残り時間
        /// </summary>
        private float LastTime = 0.0f;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RemoteCharacterMovement()
        {
        }

        /// <summary>
        /// 初期化された
        /// </summary>
        protected override void OnInitialize()
        {
            Trans = GetMonoBehaviourComponent<Transform>();
            GetMonoBehaviourComponent<Rigidbody>().isKinematic = true;  // 物理演算は邪魔っ気じゃあ！
        }

        /// <summary>
        /// 移動をセット
        /// </summary>
        /// <param name="Position">座標</param>
        public void SetMove(Vector3 Position)
        {
            PrevPosition = Trans.position;
            Destination = Position;
            LastTime = MoveTime;
        }

        /// <summary>
        /// Update
        /// </summary>
        public override void OnUpdate()
        {
            if (LastTime <= 0.0f) { return; }

            LastTime = Mathf.Max(LastTime - Time.deltaTime, 0.0f);
            var Current = Vector3.Lerp(PrevPosition, Destination, 1.0f - (LastTime / MoveTime));
            Trans.position = Current;
        }
    }
}
