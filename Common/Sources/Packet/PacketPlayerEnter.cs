using Common.Stream;

namespace Common.Packet
{
    public class PacketPlayerEnter : Packet, ISerializable
	{
		public static byte PacketID { get { return (byte) EPacketID.PlayerEnter; } }

		

		/// <summary>
		///  ID
		/// </summary>
		public int Id = new int();

		/// <summary>
		///  座標
		/// </summary>
		public Vec3 Position = new Vec3();

		

		protected override byte SendCode { get { return PacketID; } }

		public PacketPlayerEnter()
		{
		}

		public PacketPlayerEnter(int Id, Vec3 Position)
		{
			this.Id = Id;
			this.Position = Position;
			
		}

		public bool Serialize(IMemoryStream Stream)
		{
			Stream.Serialize(ref Id);
			Position.Serialize(Stream);
			
			return true;
		}

		public static byte[] SerializeObject(object customType)
		{
			var Stream = new MemoryStreamWriter();
			var Obj = (PacketPlayerEnter)customType;
			Obj.Serialize(Stream);
			return Stream.Buffer.ToArray();
		}

		public static object DeserializeObject(byte[] data)
		{
			var Stream = new MemoryStreamReader(data);
			var Obj = new PacketPlayerEnter();
			Obj.Serialize(Stream);
			return Obj;
		}
	}
}
