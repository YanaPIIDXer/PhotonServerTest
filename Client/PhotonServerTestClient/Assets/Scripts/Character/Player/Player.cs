using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System;

namespace Game.Character.Player
{
    /// <summary>
    /// プレイヤークラス
    /// </summary>
    public class Player : Character
    {
        /// <summary>
        /// Prefabのパス
        /// </summary>
        private static readonly string PrefabPath = "Prefabs/Character/Player";

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="Position">座標</param>
        /// <returns>プレイヤー</returns>
        public static Player Spawn(Vector3 Position)
        {
            var Player = PrefabManager.Instance.Load<Player>(PrefabPath);
            Player.transform.position = Position;
            return Player;
        }
    }
}
