using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Networking;
using System;
using UniRx;
using ExitGames.Client.Photon;
using Common.Code;
using Common.Packet;

/// <summary>
/// ログイン実験用
/// ※クライアント側のアーキテクチャがまだ定まっていないので破棄前提
/// </summary>
public class LoginTest : MonoBehaviour
{
    void Awake()
    {
        ConnectionClient.OnConnectionStatusChanged
            .Where((Code) => Code == StatusCode.Connect)
            .Subscribe((_) =>
            {
                OperationPacket Packet = new OperationPacket(EOperationCode.LogIn, new Dictionary<byte, object>());
                ConnectionClient.Instance.SendRequest(Packet);
            }).AddTo(gameObject);

        ConnectionClient.OnRecvResponse
            .Where((Packet) => Packet.Code == EOperationCode.LogIn)
                .Subscribe((_) => Debug.Log("LogIn Success!!"))
                .AddTo(gameObject);
    }
}
