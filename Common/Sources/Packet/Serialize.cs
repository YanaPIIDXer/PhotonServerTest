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

        public static byte[] SerializePlayerList(object customType)
        {
            PlayerList Obj = (PlayerList)customType;
            var Dic = Obj.Dic;
            List<byte> Bytes = new List<byte>();
            Bytes.AddRange(BitConverter.GetBytes(Dic.Count));
            foreach (var KeyValue in Dic)
            {
                Bytes.AddRange(BitConverter.GetBytes(KeyValue.Key));
                Vector3 Vec = KeyValue.Value;
                Bytes.AddRange(SerializeVector3(Vec));
            }
            return Bytes.ToArray();
        }

        public static object DeserializePlayerList(byte[] data)
        {
            PlayerList Obj = new PlayerList();
            int Current = 0;
            int Count = BitConverter.ToInt32(data, Current);
            Current += sizeof(int);
            for (var i = 0; i < Count; i++)
            {
                int Id = BitConverter.ToInt32(data, Current);
                Current += sizeof(int);

                float X = BitConverter.ToSingle(data, Current);
                Current += sizeof(float);
                float Y = BitConverter.ToSingle(data, Current);
                Current += sizeof(float);
                float Z = BitConverter.ToSingle(data, Current);
                Current += sizeof(float);

                Vector3 Vec = new Vector3(X, Y, Z);
                Obj.Add(Id, Vec);
            }

            return Obj;
        }
    }
}