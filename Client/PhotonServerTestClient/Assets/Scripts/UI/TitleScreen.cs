using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Game.UI
{
    /// <summary>
    /// タイトル画面
    /// </summary>
    public class TitleScreen : UIComponent
    {
        /// <summary>
        /// ZOrder
        /// </summary>
        public override EZOrder ZOrder => EZOrder.Overlay;

        /// <summary>
        /// ログインボタン
        /// </summary>
        [SerializeField]
        private Button LogInButton = null;

        /// <summary>
        /// ログインボタンが押された
        /// </summary>
        public IObservable<Unit> OnLogInButtonPressed { get { return LogInButton.OnClickAsObservable(); } }
    }
}
