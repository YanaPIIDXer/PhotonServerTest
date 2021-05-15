using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Networking;
using System;
using UniRx;
using Common.Code;
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

            ConnectionClient.OnConnectionStatusChanged
                .Where((Code) => Code == ExitGames.Client.Photon.StatusCode.Connect)
                .Subscribe((_) =>
                {
                    OperationPacket Packet = new OperationPacket(EOperationCode.LogIn);
                    ConnectionClient.Instance.SendRequest(Packet);
                }).AddTo(gameObject);

            ConnectionClient.OnRecvResponse
                .Where((Packet) => Packet.Code == EOperationCode.LogIn)
                .Subscribe((Packet) =>
                {
                    SceneManager.LoadScene("Game");
                }).AddTo(gameObject);
        }
    }
}
