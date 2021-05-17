using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character.Component
{
    /// <summary>
    /// キャラクタコンポーネント
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
        /// 所有者をセット
        /// </summary>
        /// <param name="Owner">所有者</param>
        public void SetOwner(Character Owner)
        {
            this.Owner = Owner;
            OnIntiialize();
        }

        /// <summary>
        /// MonoBehaviour派生のComponentを取得
        /// </summary>
        /// <typeparam name="T">Componentの型</typeparam>
        /// <returns>Compoennt</returns>
        protected T GetMonoBehaviourComponent<T>()
            where T : MonoBehaviour
        {
            return Owner.GetComponent<T>();
        }

        /// <summary>
        /// 初期化された
        /// </summary>
        protected virtual void OnIntiialize() { }

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
