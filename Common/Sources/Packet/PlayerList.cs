using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Packet
{
    /// <summary>
    /// プレイヤーリスト
    /// </summary>
    public class PlayerList
    {
        /// <summary>
        /// Dictionary
        /// </summary>
        public Dictionary<int, Vector3> Dic { get; private set; } = new Dictionary<int, Vector3>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlayerList()
        {
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="Id">ID</param>
        /// <param name="Position">座標</param>
        public void Add(int Id, Vector3 Position)
        {
            Dic.Add(Id, Position);
        }
    }
}
