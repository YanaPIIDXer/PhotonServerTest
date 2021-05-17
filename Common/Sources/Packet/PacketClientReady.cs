using System.Collections.Generic;
using Common.Stream;

namespace Common.Packet
{
    public class PacketClientReady  : IPacket
	{
		

		

		public PacketClientReady()
		{
		}

		

		public bool Serialize(IStream Stream)
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
