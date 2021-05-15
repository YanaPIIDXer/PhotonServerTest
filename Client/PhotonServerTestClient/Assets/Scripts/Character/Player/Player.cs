using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using Game.Character.Player.Component;

namespace Game.Character.Player
{
    /// <summary>
    /// プレイヤークラス
    /// </summary>
    public class Player : Character
    {
        /// <summary>
        /// ローカルキャラの移動Componentをセットアップ
        /// </summary>
        /// <param name="ControlObservable">制御Observable</param>
        public void SetupLocalMoveementComponent(IObservable<Vector2> ControlObservable)
        {
            LocalPlayerMovement Movement = new LocalPlayerMovement();
            ControlObservable.Subscribe((Value) => Movement.InputVector = Value);
            AddCharacterComponent(Movement);
        }
    }
}
