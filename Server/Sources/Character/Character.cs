using UnityEngine;

namespace Character
{
    /// <summary>
    /// キャラクタ
    /// </summary>
    public abstract class Character
    {
        /// <summary>
        /// 座標
        /// </summary>
        public Vector3 Position { get; protected set; }
    }
}
