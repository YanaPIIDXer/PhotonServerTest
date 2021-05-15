using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character.Component;

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

    }
}
