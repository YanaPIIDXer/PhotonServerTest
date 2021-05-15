using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character.Component
{
    /// <summary>
    /// キャラクタコンポーネント基底クラス
    /// </summary>
    public abstract class CharacterComponent
    {
        /// <summary>
        /// 所有者
        /// </summary>
        private Character Owner = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CharacterComponent()
        {
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="Owner">所有者</param>
        public void Initialize(Character Owner)
        {
            this.Owner = Owner;
            OnInitialize();
        }

        /// <summary>
        /// MonoBehaviour派生Component取得
        /// </summary>
        /// <typeparam name="T">Componentの型</typeparam>
        /// <returns>Component</returns>
        protected T GetMonoBehaviourComponent<T>()
        {
            return Owner.GetComponent<T>();
        }

        /// <summary>
        /// 初期化された
        /// </summary>
        protected virtual void OnInitialize() { }

        /// <summary>
        /// Update
        /// </summary>
        public virtual void OnUpdate() { }

        /// <summary>
        /// FixedUpdate
        /// </summary>
        public virtual void OnFixedUpdate() { }
    }
}
