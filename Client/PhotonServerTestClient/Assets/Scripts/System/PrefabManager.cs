using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.System
{
    /// <summary>
    /// Prefab管理
    /// </summary>
    public class PrefabManager
    {
        /// <summary>
        /// PrefabのDictionary
        /// </summary>
        private Dictionary<string, GameObject> PrefabDic = new Dictionary<string, GameObject>();

        /// <summary>
        /// 読み込み
        /// </summary>
        /// <param name="Path">Prefabのパス</param>
        /// <typeparam name="T">Componentの型</typeparam>
        /// <returns>Component</returns>
        public T Load<T>(string Path)
        {
            var Prefab = FindOrLoadPrefab(Path);
            var Obj = GameObject.Instantiate(Prefab);
            Debug.Assert(Obj != null, "Prefab Instantiate Failed. Path:" + Path);

            return Obj.GetComponent<T>();
        }

        /// <summary>
        /// 読み込み
        /// </summary>
        /// <param name="Path">Prefabのパス</param>
        /// <param name="ParentTransform">親Transform</param>
        /// <typeparam name="T">Componentの型</typeparam>
        /// <returns>Component</returns>
        public T Load<T>(string Path, Transform ParentTransform)
        {
            var Prefab = FindOrLoadPrefab(Path);
            var Obj = GameObject.Instantiate(Prefab, ParentTransform);
            Debug.Assert(Obj != null, "Prefab Instantiate Failed. Path:" + Path);

            return Obj.GetComponent<T>();
        }

        /// <summary>
        /// Prefabを読み込むか保持してあるものを返す
        /// </summary>
        /// <param name="Path">パス</param>
        /// <returns>Prefab</returns>
        private GameObject FindOrLoadPrefab(string Path)
        {
            if (!PrefabDic.ContainsKey(Path))
            {
                var Prefab = Resources.Load<GameObject>(Path);
                Debug.Assert(Prefab != null, "Prefab Load Failed. Path:" + Path);
                PrefabDic.Add(Path, Prefab);
            }
            return PrefabDic[Path];
        }

        #region Singleton
        public static PrefabManager Instance { get { return _Instance; } }
        private static PrefabManager _Instance = new PrefabManager();
        private PrefabManager() { }
        #endregion
    }
}
