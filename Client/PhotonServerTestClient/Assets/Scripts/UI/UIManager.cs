using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System;

namespace Game.UI
{
    /// <summary>
    /// UIマネージャ
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        /// <summary>
        /// Prefabのパス
        /// </summary>
        private static readonly string PrefabPath = "Prefabs/System/UIManager";

        /// <summary>
        /// UIのPrefabが格納されているルートパス
        /// </summary>
        private static readonly string UIPrefabPathRoot = "Prefabs/UI/";

        /// <summary>
        /// インスタンス
        /// </summary>
        public static UIManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = PrefabManager.Instance.Load<UIManager>(PrefabPath);
                }
                return _Instance;
            }
        }
        private static UIManager _Instance = null;

        /// <summary>
        /// CanvasのTransform
        /// </summary>
        [SerializeField]
        private Transform CanvasTransform = null;

        /// <summary>
        /// UI表示
        /// </summary>
        /// <param name="PrefabRelativePath">Prefabs/UI/からPrefabまでの相対パス</param>
        /// <typeparam name="T">UIのComponentの型</typeparam>
        /// <returns>ハンドラ</returns>
        public UIHandler<T> Show<T>(string PrefabRelativePath)
            where T : UIComponent
        {
            string Path = UIPrefabPathRoot + PrefabRelativePath;
            T Inst = PrefabManager.Instance.Load<T>(Path, CanvasTransform);
            Inst.transform.localPosition += new Vector3(0.0f, 0.0f, (float)Inst.ZOrder);
            return new UIHandler<T>(Inst);
        }
    }
}
