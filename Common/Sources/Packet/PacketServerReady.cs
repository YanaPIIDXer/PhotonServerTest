using System.Collections.Generic;
using Common.Stream;

namespace Common.Packet
{
    public class PacketServerReady  : IPacket
	{
		

		/// <summary>
		///  キャラクタID
		/// </summary>
		public int CharacterId = new int();

		

		public EPacketID PacketID { get { return EPacketID.ServerReady; } }

		public PacketServerReady()
		{
		}

		public PacketServerReady(int CharacterId)
		{
			this.CharacterId = CharacterId;
			
		}

		public bool Serialize(IStream Stream)
		{
			Stream.Serialize(ref CharacterId);
			
			return true;
		}

		public static byte[] SerializeObject(object customType)
		{
			var Stream = new MemoryStreamWriter();
			var Obj = (PacketServerReady)customType;
			Obj.Serialize(Stream);
			return Stream.Buffer.ToArray();
		}

		public static object DeserializeObject(byte[] data)
		{
			var Stream = new MemoryStreamReader(data);
			var Obj = new PacketServerReady();
			Obj.Serialize(Stream);
			return Obj;
		}
	}
}
