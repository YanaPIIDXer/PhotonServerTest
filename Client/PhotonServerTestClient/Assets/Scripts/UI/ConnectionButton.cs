using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UnityEngine.UI;
using Game.Networking;
using ExitGames.Client.Photon;

namespace Game.UI
{
    /// <summary>
    /// 接続ボタン
    /// </summary>
    public class ConnectionButton : MonoBehaviour
    {
        /// <summary>
        /// 接続ボタン
        /// </summary>
        private Button ConnButton = null;

        void Awake()
        {
            ConnButton = GetComponent<Button>();
            ConnButton.OnClickAsObservable()
                        .Subscribe((_) => ConnectionClient.Instance.Connect())
                        .AddTo(gameObject);

            ConnectionClient.Instance.OnConnectionStatusChanged.Subscribe((Status) =>
            {
                ConnButton.interactable = (Status != StatusCode.Connect);
            }).AddTo(gameObject);
        }
    }
}
