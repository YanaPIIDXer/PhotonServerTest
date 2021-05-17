using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Network;
using Common.Packet;
using Game.Character.Player;
using Game.UI;

namespace Game.Sequence
{
    /// <summary>
    /// ゲーム画面シーケンス
    /// </summary>
    public class GameSequence : MonoBehaviour
    {
        void Awake()
        {
            var ControlUIHandler = UIManager.Instance.Show<ControlInputs>("ControlInputs");
            NetworkCore.Instance.SendRequest(new PacketClientReady(), EPacketID.ServerReady, (Stream) =>
            {
                var Response = new PacketServerReady();
                Response.Serialize(Stream);
                //var Id = Response.CharacterId;
                // TODO;PlayerManagerクラスを定義してそこに投げる
                var Pos = Response.Position.ToVector3();
                var SpawnPlayer = Player.Spawn(Pos);
                SpawnPlayer.SetupAsLocalPlayer(ControlUIHandler.Instance.MoveInput);
            });
        }
    }
}
