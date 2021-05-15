using Photon.SocketServer;
using System;

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
    }

    protected override void TearDown()
    {
    }
}
