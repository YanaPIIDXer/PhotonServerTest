using System.Collections.Generic;
using Common.Stream;

namespace Common.Packet
{
    public class PacketPlayerMove  : IPacket
	{
		

		/// <summary>
		///  座標
		/// </summary>
		public Vec3 Position = new Vec3();

		

		public EPacketID PacketID { get { return EPacketID.PlayerMove; } }

		public PacketPlayerMove()
		{
		}

		public PacketPlayerMove(Vec3 Position)
		{
			this.Position = Position;
			
		}

		public bool Serialize(IStream Stream)
		{
			Position.Serialize(Stream);
			
			return true;
		}

		public static byte[] SerializeObject(object customType)
		{
			var Stream = new MemoryStreamWriter();
			var Obj = (PacketPlayerMove)customType;
			Obj.Serialize(Stream);
			return Stream.Buffer.ToArray();
		}

		public static object DeserializeObject(byte[] data)
		{
			var Stream = new MemoryStreamReader(data);
			var Obj = new PacketPlayerMove();
			Obj.Serialize(Stream);
			return Obj;
		}
	}
}
