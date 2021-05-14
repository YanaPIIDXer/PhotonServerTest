﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

public class PeerEventListener : MonoBehaviour, IPhotonPeerListener
{
    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(string.Format("Level:{0} {1}", level.ToString(), message));
    }

    public void OnEvent(EventData eventData)
    {
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
    }
}
