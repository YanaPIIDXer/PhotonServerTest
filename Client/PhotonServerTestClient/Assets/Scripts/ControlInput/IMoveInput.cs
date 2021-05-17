using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Game.ControlInput
{
    /// <summary>
    /// 移動入力インタフェース
    /// </summary>
    public interface IMoveInput
    {
        /// <summary>
        /// 移動入力
        /// </summary>
        IObservable<Vector2> OnInputMove { get; }
    }
}
