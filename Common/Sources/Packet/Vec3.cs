using System.Collections.Generic;
using Common.Stream;

namespace Common.Packet
{
    public class Vec3  : ISerializable
	{
		

		/// <summary>
		///  X
		/// </summary>
		public float X = new float();

		/// <summary>
		///  Y
		/// </summary>
		public float Y = new float();

		/// <summary>
		///  Z
		/// </summary>
		public float Z = new float();

		

		

		public Vec3()
		{
		}

		public Vec3(float X, float Y, float Z)
		{
			this.X = X;
			this.Y = Y;
			this.Z = Z;
			
		}

		public bool Serialize(IStream Stream)
		{
			Stream.Serialize(ref X);
			Stream.Serialize(ref Y);
			Stream.Serialize(ref Z);
			
			return true;
		}

		public static byte[] SerializeObject(object customType)
		{
			var Stream = new MemoryStreamWriter();
			var Obj = (Vec3)customType;
			Obj.Serialize(Stream);
			return Stream.Buffer.ToArray();
		}

		public static object DeserializeObject(byte[] data)
		{
			var Stream = new MemoryStreamReader(data);
			var Obj = new Vec3();
			Obj.Serialize(Stream);
			return Obj;
		}
	}
}
