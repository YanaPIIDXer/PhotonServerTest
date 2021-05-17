using System.Collections.Generic;
using Common.Stream;

namespace Common.Packet
{
    public class CharacterData  : ISerializable
	{
		

		/// <summary>
		///  ID
		/// </summary>
		public int Id = new int();

		/// <summary>
		///  座標
		/// </summary>
		public Vec3 Position = new Vec3();

		

		public CharacterData()
		{
		}

		public CharacterData(int Id, Vec3 Position)
		{
			this.Id = Id;
			this.Position = Position;
			
		}

		public bool Serialize(IStream Stream)
		{
			Stream.Serialize(ref Id);
			Position.Serialize(Stream);
			
			return true;
		}

		public static byte[] SerializeObject(object customType)
		{
			var Stream = new MemoryStreamWriter();
			var Obj = (CharacterData)customType;
			Obj.Serialize(Stream);
			return Stream.Buffer.ToArray();
		}

		public static object DeserializeObject(byte[] data)
		{
			var Stream = new MemoryStreamReader(data);
			var Obj = new CharacterData();
			Obj.Serialize(Stream);
			return Obj;
		}
	}
}
