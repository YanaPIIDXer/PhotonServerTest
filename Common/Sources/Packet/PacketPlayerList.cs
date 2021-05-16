using Common.Stream;

namespace Common.Packet
{
    public class PacketPlayerList : ISerializable
	{
		

		/// <summary>
		///  プレイヤーリスト
		/// </summary>
		public FlexArray<CharacterData> List = new FlexArray<CharacterData>();

		

		public PacketPlayerList()
		{
		}

		public PacketPlayerList(FlexArray<CharacterData> List)
		{
			this.List = List;
			
		}

		public bool Serialize(IMemoryStream Stream)
		{
			List.Serialize(Stream);
			
			return true;
		}

		public static byte[] SerializeObject(object customType)
		{
			var Stream = new MemoryStreamWriter();
			var Obj = (PacketPlayerList)customType;
			Obj.Serialize(Stream);
			return Stream.Buffer.ToArray();
		}

		public static object DeserializeObject(byte[] data)
		{
			var Stream = new MemoryStreamReader(data);
			var Obj = new PacketPlayerList();
			Obj.Serialize(Stream);
			return Obj;
		}
	}
}
