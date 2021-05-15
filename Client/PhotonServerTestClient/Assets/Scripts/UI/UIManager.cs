using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// UI管理
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        /// <summary>
        /// Prefabのパス
        /// </summary>
        private static readonly string PrefabPath = "Prefabs/System/UIManager";

        /// <summary>
        /// 背面キャンバスのトランスフォーム
        /// </summary>
        [SerializeField]
        private Transform BackCanvasTransform = null;

        /// <summary>
        /// 中間キャンバスのトランスフォーム
        /// </summary>
        [SerializeField]
        private Transform MiddleCanvasTransform = null;

        /// <summary>
        /// 前面キャンバスのトランスフォーム
        /// </summary>
        [SerializeField]
        private Transform FrontCanvasTransform = null;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 全消去
        /// </summary>
        public void RemoveAll()
        {
            Transform[] Trs = { BackCanvasTransform, MiddleCanvasTransform, FrontCanvasTransform };
            foreach (var Tr in Trs)
            {
                while (Tr.childCount != 0)
                {
                    Destroy(Tr.GetChild(0).gameObject);
                }
            }
        }

        #region Singleton
        public static UIManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    GameObject Prefab = Resources.Load<GameObject>(PrefabPath);
                    Debug.Assert(Prefab != null, "UIManager Prefab Load Failed.");

                    GameObject Obj = Instantiate<GameObject>(Prefab, Vector3.zero, Quaternion.identity);
                    Debug.Assert(Obj != null, "UIManager Instantiate Failed.");

                    _Instance = Obj.GetComponent<UIManager>();
                }
                return _Instance;
            }
        }
        private static UIManager _Instance = null;
        #endregion
    }
}
