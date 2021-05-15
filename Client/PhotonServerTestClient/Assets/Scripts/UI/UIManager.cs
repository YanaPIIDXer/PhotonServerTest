using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// キャンバスの位置
    /// </summary>
    public enum ECanvas
    {
        Back,
        Middle,
        Front,
    }

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
        /// UIのPrefabが格納されているルートディレクトリ
        /// </summary>
        private static readonly string UIPrefabRootPath = "Prefabs/UI/";

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

        /// <summary>
        /// 表示
        /// </summary>
        /// <param name="PrefabRelativePath">Preabs/UI/からのPrefab相対パス</param>
        /// <param name="Canvas">キャンバスの位置</param>
        /// <typeparam name="T">UIComponentの型</typeparam>
        /// <returns>UIハンドラ</returns>
        public static UIHandler<T> Show<T>(string PrefabRelativePath, ECanvas Canvas)
            where T : MonoBehaviour
        {
            string Path = UIPrefabRootPath + PrefabRelativePath;
            // TODO:Prefabをキャッシュするクラスを作りたい
            GameObject Prefab = Resources.Load<GameObject>(Path);
            Debug.Assert(Prefab != null, "Prefab Load Failed. Path:" + Path);

            Vector3 OriginPos = Prefab.transform.position;

            GameObject Obj = Instantiate<GameObject>(Prefab);
            Debug.Assert(Obj != null, "UI Object Intantiate Failed. Path:" + Path);

            Transform Tr = null;
            switch (Canvas)
            {
                case ECanvas.Back:
                    Tr = Instance.BackCanvasTransform;
                    break;
                case ECanvas.Middle:
                    Tr = Instance.MiddleCanvasTransform;
                    break;
                case ECanvas.Front:
                    Tr = Instance.FrontCanvasTransform;
                    break;
            }

            return new UIHandler<T>(Obj, Tr, OriginPos);
        }

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
                    var Child = Tr.GetChild(0);
                    Child.SetParent(null);
                    Destroy(Child.gameObject);
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
