using UnityEngine;
using Common.Packet;

/// <summary>
/// 拡張メソッド群
/// 主にパケットツールが吐き出したクラスとUnity標準のクラスの相互変換用のメソッドを生やしたりするとか
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Vec3 → Vector3への変換
    /// </summary>
    /// <param name="Self">Vec3</param>
    /// <returns>Vector3</returns>
    public static Vector3 ToVector3(this Vec3 Self)
    {
        return new Vector3(Self.X, Self.Y, Self.Z);
    }

    /// <summary>
    /// Vector3 → Vec3への変換
    /// </summary>
    /// <param name="Self">Vector3</param>
    /// <returns>Vec3</returns>
    public static Vec3 ToVec3(this Vector3 Self)
    {
        return new Vec3(Self.x, Self.y, Self.z);
    }
}
