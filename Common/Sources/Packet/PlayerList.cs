using System.Collections;
using System.Collections.Generic;
using Common.Stream;
using UnityEngine;

namespace Common.Packet
{
    /// <summary>
    /// プレイヤーリスト
    /// </summary>
    public class PlayerList : ISerializable
    {
        /// <summary>
        /// プレイヤーリスト
        /// </summary>
        public FlexArray<PlayerData> List = new FlexArray<PlayerData>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlayerList()
        {
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Stream">ストリーム</param>
        /// <returns>成功したらtrue</returns>
        public bool Serialize(IMemoryStream Stream)
        {
            List.Serialize(Stream);
            return true;
        }
    }

    /// <summary>
    /// プレイヤーデータ
    /// </summary>
    public class PlayerData : ISerializable
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id = 0;

        public float X = 0;
        public float Y = 0;
        public float Z = 0;

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Stream">ストリーム</param>
        /// <returns>成功したらtrue</returns>
        public bool Serialize(IMemoryStream Stream)
        {
            Stream.Serialize(ref Id);
            Stream.Serialize(ref X);
            Stream.Serialize(ref Y);
            Stream.Serialize(ref Z);
            return true;
        }
    }
}
