using System;
using UnityEngine;
using System.Collections.Generic;

namespace Common.Packet
{
    /// <summary>
    /// 色々なシリアライズメソッドの定義
    /// </summary>
    public static class SerializeMethods
    {
        public static byte[] SerializeVector3(object customType)
        {
            Vector3 Vec = (Vector3)customType;
            List<byte> Bytes = new List<byte>();
            Bytes.AddRange(BitConverter.GetBytes(Vec.x));
            Bytes.AddRange(BitConverter.GetBytes(Vec.y));
            Bytes.AddRange(BitConverter.GetBytes(Vec.z));
            return Bytes.ToArray();
        }

        public static object DeserializeVector3(byte[] data)
        {
            Vector3 Vec = new Vector3();
            int Size = sizeof(float);
            Vec.x = BitConverter.ToSingle(data, 0);
            Vec.y = BitConverter.ToSingle(data, Size);
            Vec.z = BitConverter.ToSingle(data, Size * 2);
            return Vec;
        }
    }
}