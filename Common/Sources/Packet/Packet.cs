using Common.Stream;

namespace Common.Packet
{
    /// <summary>
    /// パケットインタフェース
    /// </summary>
    public interface IPacket : ISerializable
    {
        /// <summary>
        /// パケットＩＤ
        /// </summary>
        EPacketID PacketID { get; }
    }
}
