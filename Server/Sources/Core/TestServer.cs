using Photon.SocketServer;

public class TestServer : ApplicationBase
{
    protected override PeerBase CreatePeer(InitRequest initRequest)
    {
        var Peer = new GamePeer(initRequest);
        return Peer;
    }

    protected override void Setup()
    {
    }

    protected override void TearDown()
    {
    }
}
