using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Networking;
using System;
using UniRx;
using Common.Packet;
using UnityEngine.SceneManagement;
using Game.UI;

namespace Game.Sequence
{
    /// <summary>
    /// タイトル画面シーケンス
    /// </summary>
    public class TitleSequence : MonoBehaviour
    {
        void Awake()
        {
            UIManager.Show<ConnectionButton>("Title/ConnectionButton", ECanvas.Front);

            ConnectionClient.AddPacketHandler(EPacketID.LogInResult, (_) =>
            {
                // TODO:UIのリセットとシーン遷移を同時にやってくれるクラスを作りたい
                UIManager.Instance.RemoveAll();
                SceneManager.LoadScene("Game");
            });

            ConnectionClient.OnConnectionStatusChanged
                .Where((Code) => Code == ExitGames.Client.Photon.StatusCode.Connect)
                .Subscribe((_) =>
                {
                    ConnectionClient.Instance.SendRequest(new PacketLogInRequest());
                }).AddTo(gameObject);
        }
    }
}
