using Photon.SocketServer;
using System;
using UnityEngine;
using Common.Packet;

public class TestServer : ApplicationBase
{
    /// <summary>
    /// ワールド
    /// </summary>
    private World.World GameWorld = new World.World();

    protected override PeerBase CreatePeer(InitRequest initRequest)
    {
        var Peer = new GamePeer(initRequest);
        Peer.OnActiveState.Subscribe((_) => GameWorld.AddPeer(Peer));
        return Peer;
    }

    protected override void Setup()
    {
        RegisterCustomClasses();
    }

    protected override void TearDown()
    {
    }

    /// <summary>
    /// カスタムクラスの登録
    /// </summary>
    private void RegisterCustomClasses()
    {
        Protocol.TryRegisterCustomType(typeof(PacketPlayerList), PacketPlayerList.PacketID, PacketPlayerList.SerializeObject, PacketPlayerList.DeserializeObject);
        Protocol.TryRegisterCustomType(typeof(PacketPlayerMove), PacketPlayerMove.PacketID, PacketPlayerMove.SerializeObject, PacketPlayerMove.DeserializeObject);
        Protocol.TryRegisterCustomType(typeof(PacketOtherPlayerMove), PacketOtherPlayerMove.PacketID, PacketOtherPlayerMove.SerializeObject, PacketOtherPlayerMove.DeserializeObject);
    }
}
