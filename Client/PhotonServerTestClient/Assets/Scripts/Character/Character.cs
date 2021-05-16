using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character.Component;
using System;

namespace Game.Character
{
    /// <summary>
    /// キャラクタクラス
    /// </summary>
    public abstract class Character : MonoBehaviour
    {
        /// <summary>
        /// コンポーネントリスト
        /// </summary>
        private List<CharacterComponent> Components = new List<CharacterComponent>();

        /// <summary>
        /// 移動を受信した
        /// </summary>
        protected Action<Vector3> OnRecvMove { private get; set; }

        /// <summary>
        /// キャラクタコンポーネントを追加
        /// </summary>
        /// <param name="Cmp">キャラクタコンポーネント</param>
        protected void AddCharacterComponent(CharacterComponent Cmp)
        {
            Cmp.Initialize(this);
            Components.Add(Cmp);
        }

        void Update()
        {
            foreach (var Cmp in Components)
            {
                Cmp.OnUpdate();
            }
        }

        void FixedUpdate()
        {
            foreach (var Cmp in Components)
            {
                Cmp.OnFixedUpdate();
            }
        }

        /// <summary>
        /// 移動を受信した
        /// </summary>
        /// <param name="Position">座標</param>
        public void RecvMove(Vector3 Position)
        {
            OnRecvMove?.Invoke(Position);
        }
    }
}
