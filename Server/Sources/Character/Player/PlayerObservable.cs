using System;
using System.Reactive.Subjects;
using UnityEngine;

namespace Character.Player
{
    /// <summary>
    /// プレイヤーのObservable定義
    /// </summary>
    public partial class Player : Character
    {
        /// <summary>
        /// 移動Subject
        /// </summary>
        private Subject<Vector3> OnMovedSubject = new Subject<Vector3>();

        /// <summary>
        /// 移動した
        /// </summary>
        public IObservable<Vector3> OnMoved { get { return OnMovedSubject; } }

    }
}