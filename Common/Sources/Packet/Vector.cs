using System;
using UnityEngine;

namespace Common.Packet
{
    /// <summary>
    /// シリアライズ可能な３次元ベクトルクラス
    /// </summary>
    [Serializable]
    public class Vec3
    {
        /// <summary>
        /// X
        /// </summary>
        public float x;

        /// <summary>
        /// Y
        /// </summary>
        public float y;

        /// <summary>
        /// Z
        /// </summary>
        public float z;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="z">Z</param>
        public Vec3(float x = 0.0f, float y = 0.0f, float z = 0.0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Src">UnityのVector3</param>
        public Vec3(Vector3 Src)
        {
            x = Src.x;
            y = Src.y;
            z = Src.z;
        }

        /// <summary>
        /// UnityのVector3に変換
        /// </summary>
        /// <returns>Vector3</returns>
        public Vector3 ToUnity()
        {
            return new Vector3(x, y, z);
        }
    }
}
