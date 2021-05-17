using UnityEngine;

namespace Common.Packet
{
    /// <summary>
    /// 拡張メソッド群
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// Vector3 → Vec3に変換
        /// </summary>
        /// <param name="Self">Vector3</param>
        /// <returns>Vec3</returns>
        public static Vec3 ToVec3(this Vector3 Self)
        {
            return new Vec3(Self.x, Self.y, Self.z);
        }

        /// <summary>
        /// Vec3 → Vector3に変換
        /// </summary>
        /// <param name="Self">Vec3</param>
        /// <returns>Vector3</returns>
        public static Vec3 ToVector3(this Vec3 Self)
        {
            return new Vec3(Self.X, Self.Y, Self.Z);
        }
    }
}
