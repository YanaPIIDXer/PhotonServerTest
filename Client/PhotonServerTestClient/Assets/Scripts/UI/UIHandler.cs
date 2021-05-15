using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// UIハンドラ
    /// </summary>
    public class UIHandler<T>
        where T : MonoBehaviour
    {
        /// <summary>
        /// UIのGameObject
        /// </summary>
        private GameObject UIObject = null;

        /// <summary>
        /// UIComponent
        /// </summary>
        public T UIComponent { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="UIObject">UIのGameObject</param>
        public UIHandler(GameObject UIObject)
        {
            this.UIObject = UIObject;
            UIComponent = this.UIObject.GetComponent<T>();
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Destroy()
        {
            GameObject.Destroy(UIObject);
        }
    }
}
