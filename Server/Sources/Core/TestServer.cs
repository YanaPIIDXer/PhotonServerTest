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
        RegisterPacketType.RegisterPackets(Protocol.TryRegisterCustomType);
    }

    protected override void TearDown()
    {
    }
}
