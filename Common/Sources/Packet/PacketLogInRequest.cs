using Common.Stream;

namespace Common.Packet
{
    public class PacketLogInRequest : ISerializable
	{
		public static byte PacketID { get { return (byte) EPacketID.LogInRequest; } }

		

		

		public PacketLogInRequest()
		{
		}

		

		public bool Serialize(IMemoryStream Stream)
		{
			
			return true;
		}

		public static byte[] SerializeObject(object customType)
		{
			var Stream = new MemoryStreamWriter();
			var Obj = (PacketLogInRequest)customType;
			Obj.Serialize(Stream);
			return Stream.Buffer.ToArray();
		}

		public static object DeserializeObject(byte[] data)
		{
			var Stream = new MemoryStreamReader(data);
			var Obj = new PacketLogInRequest();
			Obj.Serialize(Stream);
			return Obj;
		}
	}
}
