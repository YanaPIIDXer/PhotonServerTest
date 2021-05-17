using Photon.SocketServer;
using Common.Packet;

public class TestServer : ApplicationBase
{
    protected override PeerBase CreatePeer(InitRequest initRequest)
    {
        var Peer = new GamePeer(initRequest);
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
