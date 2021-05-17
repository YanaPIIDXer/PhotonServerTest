using System.Collections.Generic;
using Common.Stream;

namespace Common.Packet
{
    public class PacketPlayerLeave  : IPacket
	{
		

		/// <summary>
		///  ID
		/// </summary>
		public int Id = new int();

		

		public EPacketID PacketID { get { return EPacketID.PlayerLeave; } }

		public PacketPlayerLeave()
		{
		}

		public PacketPlayerLeave(int Id)
		{
			this.Id = Id;
			
		}

		public bool Serialize(IStream Stream)
		{
			Stream.Serialize(ref Id);
			
			return true;
		}

		public static byte[] SerializeObject(object customType)
		{
			var Stream = new MemoryStreamWriter();
			var Obj = (PacketPlayerLeave)customType;
			Obj.Serialize(Stream);
			return Stream.Buffer.ToArray();
		}

		public static object DeserializeObject(byte[] data)
		{
			var Stream = new MemoryStreamReader(data);
			var Obj = new PacketPlayerLeave();
			Obj.Serialize(Stream);
			return Obj;
		}
	}
}
