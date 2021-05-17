using System.Collections.Generic;
using Common.Stream;

namespace Common.Packet
{
    public class PacketLogInResult  : IPacket
	{
		

		

		public EPacketID PacketID { get { return EPacketID.LogInResult; } }

		public PacketLogInResult()
		{
		}

		

		public bool Serialize(IStream Stream)
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
