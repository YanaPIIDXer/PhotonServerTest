using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character.Component;

namespace Game.Character
{
    /// <summary>
    /// キャラクタ
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Character : MonoBehaviour
    {
        /// <summary>
        /// コンポーネントリスト
        /// </summary>
        private List<CharacterComponent> Components = new List<CharacterComponent>();

        /// <summary>
        /// キャラクタコンポーネント追加
        /// </summary>
        /// <param name="Component">キャラクタコンポーネント</param>
        protected void AddCharacterComponent(CharacterComponent Component)
        {
            Component.SetOwner(this);
            Components.Add(Component);
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
    }
}
