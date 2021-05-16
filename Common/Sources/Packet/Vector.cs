using System;
using UnityEngine;

namespace Common.Packet
{
    /// <summary>
    /// ３次元ベクトル
    /// </summary>
    [Serializable]
    public struct Vec3
    {
        /// <summary>
        /// X
        /// </summary>
        public float X;

        /// <summary>
        /// Y
        /// </summary>
        public float Y;

        /// <summary>
        /// Z
        /// </summary>
        public float Z;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="X">X</param>
        /// <param name="Y">Y</param>
        /// <param name="Z">Z</param>
        public Vec3(float X = 0, float Y = 0, float Z = 0)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Src">Unityのベクトルクラス</param>
        public Vec3(Vector3 Src)
        {
            this.X = Src.x;
            this.Y = Src.y;
            this.Z = Src.z;
        }
    }
}
