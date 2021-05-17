using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using Game.Network;
using ExitGames.Client.Photon;

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

        void Awake()
        {
            OnLogInButtonPressed
                .Subscribe((_) => LogInButton.interactable = false)
                .AddTo(gameObject);

            NetworkCore.Instance.OnNetworkStatusChanged
                .Where((Code) => Code != StatusCode.Connect)
                .Subscribe((_) => LogInButton.interactable = true)
                .AddTo(gameObject);
        }
    }
}
