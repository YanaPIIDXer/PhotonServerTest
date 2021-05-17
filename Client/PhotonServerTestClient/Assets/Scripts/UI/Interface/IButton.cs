using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Game.UI.Interface
{
    /// <summary>
    /// ボタンインタフェース
    /// </summary>
    public interface IButton
    {
        /// <summary>
        /// 押された
        /// </summary>
        IObservable<Unit> OnPress { get; }
    }
}
