using Common.Stream;

namespace Common.Packet
{
    public class PacketPlayerLeave : Packet, ISerializable
	{
		public static byte PacketID { get { return (byte) EPacketID.PlayerLeave; } }

		

		/// <summary>
		///  ID
		/// </summary>
		public int Id = new int();

		

		protected override byte SendCode { get { return PacketID; } }

		public PacketPlayerLeave()
		{
		}

		public PacketPlayerLeave(int Id)
		{
			this.Id = Id;
			
		}

		public bool Serialize(IMemoryStream Stream)
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
