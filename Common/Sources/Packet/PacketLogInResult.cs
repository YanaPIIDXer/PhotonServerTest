using Common.Stream;

namespace Common.Packet
{
    public class PacketLogInResult : Packet, ISerializable
	{
		public static byte PacketID { get { return (byte) EPacketID.LogInResult; } }

		

		

		protected override byte SendCode { get { return PacketID; } }

		public PacketLogInResult()
		{
		}

		

		public bool Serialize(IMemoryStream Stream)
		{
			
			return true;
		}

		public static byte[] SerializeObject(object customType)
		{
			var Stream = new MemoryStreamWriter();
			var Obj = (PacketLogInResult)customType;
			Obj.Serialize(Stream);
			return Stream.Buffer.ToArray();
		}

		public static object DeserializeObject(byte[] data)
		{
			var Stream = new MemoryStreamReader(data);
			var Obj = new PacketLogInResult();
			Obj.Serialize(Stream);
			return Obj;
		}
	}
}
