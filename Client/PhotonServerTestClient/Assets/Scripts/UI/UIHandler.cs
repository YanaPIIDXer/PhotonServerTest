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
        /// <param name="ParentTransform">親のTransform</param>
        /// <param name="OriginPos">元々の座標</param>
        public UIHandler(GameObject UIObject, Transform ParentTransform, Vector3 OriginPos)
        {
            this.UIObject = UIObject;
            this.UIObject.transform.SetParent(ParentTransform);
            UIComponent = this.UIObject.GetComponent<T>();
            this.UIObject.transform.localPosition = OriginPos;
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
