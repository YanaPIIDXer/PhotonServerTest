using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System;
using Game.Character.Player.Component;
using Game.ControlInput;

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

        /// <summary>
        /// 操作キャラとしてセットアップ
        /// </summary>
        /// <param name="MoveInput">移動入力インタフェース</param>
        public void SetupAsLocalPlayer(IMoveInput MoveInput)
        {
            var Movement = new LocalPlayerMovement(MoveInput);
            AddCharacterComponent(Movement);
            AddCharacterComponent(new MoveReportSender());
        }
    }
}
