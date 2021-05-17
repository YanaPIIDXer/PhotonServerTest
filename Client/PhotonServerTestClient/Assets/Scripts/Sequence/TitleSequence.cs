using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Game.UI;
using Game.Network;
using ExitGames.Client.Photon;
using Common.Packet;
using UnityEngine.SceneManagement;

namespace Game.Sequence
{
    /// <summary>
    /// タイトルシーケンス
    /// </summary>
    public class TitleSequence : MonoBehaviour
    {
        void Awake()
        {
            UIManager.Instance.Show<TitleScreen>("TitleScreen").Instance.OnLogInButtonPressed
                .Subscribe(_ =>
                {
                    NetworkCore.Instance.Connect();
                }).AddTo(gameObject);

            NetworkCore.Instance.OnNetworkStatusChanged
                .Where((Code) => Code == StatusCode.Connect)
                .Subscribe((_) =>
                {
                    NetworkCore.Instance.SendRequest(new PacketLogInRequest(), EPacketID.LogInResult, (Stream) =>
                    {
                        var Packet = new PacketLogInResult();
                        Packet.Serialize(Stream);
                        // 今のところ問答無用でログイン成功
                        Debug.Log("ログイン成功");
                        SceneManager.LoadScene("Game");
                    });
                }).AddTo(gameObject);
        }
    }
}
