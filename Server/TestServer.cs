using Photon.SocketServer;

public class TestServer : ApplicationBase
{
    protected override PeerBase CreatePeer(InitRequest initRequest)
    {
        return new GamePeer(initRequest);
    }

    protected override void Setup()
    {
    }

    protected override void TearDown()
    {
    }
}
