using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Game.UI.Interface
{
    /// <summary>
    /// 選択肢インタフェース
    /// </summary>
    /// <typeparam name="T">選択するものの型</typeparam>
    public interface ISelection<T>
    {
        /// <summary>
        /// 選択された
        /// </summary>
        IObservable<T> OnSelected { get; }
    }
}
