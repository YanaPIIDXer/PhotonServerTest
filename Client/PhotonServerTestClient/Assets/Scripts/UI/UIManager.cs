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
    }
}
