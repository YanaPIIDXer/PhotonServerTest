using Common.Stream;

namespace Common.Packet
{
    public class PacketClientReady : ISerializable
	{
		public static byte PacketID { get { return (byte) EPacketID.ClientReady; } }

		

		

		public PacketClientReady()
		{
		}

		

		public bool Serialize(IMemoryStream Stream)
		{
			
			return true;
		}

		public static byte[] SerializeObject(object customType)
		{
			var Stream = new MemoryStreamWriter();
			var Obj = (PacketClientReady)customType;
			Obj.Serialize(Stream);
			return Stream.Buffer.ToArray();
		}

		public static object DeserializeObject(byte[] data)
		{
			var Stream = new MemoryStreamReader(data);
			var Obj = new PacketClientReady();
			Obj.Serialize(Stream);
			return Obj;
		}
	}
}
