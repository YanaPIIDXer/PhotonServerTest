using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// UIハンドラ
    /// </summary>
    /// <typeparam name="T">Componentの型</typeparam>
    public class UIHandler<T>
        where T : UIComponent
    {
        /// <summary>
        /// UIインスタンス
        /// </summary>
        public T Instance { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Instance">インスタンス</param>
        public UIHandler(T Instance)
        {
            this.Instance = Instance;
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Destroy()
        {
            GameObject.Destroy(Instance.gameObject);
            Instance = null;
        }
    }
}
