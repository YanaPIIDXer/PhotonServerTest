using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Network;
using Common.Packet;

namespace Game.Sequence
{
    /// <summary>
    /// ゲーム画面シーケンス
    /// </summary>
    public class GameSequence : MonoBehaviour
    {
        void Awake()
        {
            NetworkCore.Instance.SendRequest(new PacketClientReady(), EPacketID.ServerReady, (Stream) =>
            {
                var Response = new PacketServerReady();
                Response.Serialize(Stream);
                var Id = Response.CharacterId;
                var Pos = Response.Position.ToVector3();
                Debug.Log("Id:" + Id + " Position:" + Pos.ToString());
            });
        }
    }
}
