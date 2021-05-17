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

		/// <summary>
		///  座標
		/// </summary>
		public Vec3 Position = new Vec3();

		

		public EPacketID PacketID { get { return EPacketID.ServerReady; } }

		public PacketServerReady()
		{
		}

		public PacketServerReady(int CharacterId, Vec3 Position)
		{
			this.CharacterId = CharacterId;
			this.Position = Position;
			
		}

		public bool Serialize(IStream Stream)
		{
			Stream.Serialize(ref CharacterId);
			Position.Serialize(Stream);
			
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
