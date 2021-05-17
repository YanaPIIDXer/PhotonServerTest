using System;

namespace Common.Packet
{
    public static class RegisterPacketType
    {
        public static void RegisterPackets(Func<Type, byte, Func<object, byte[]>, Func<byte[], object>, bool> Method)
        {
			Method?.Invoke(typeof(CharacterData), 1, CharacterData.SerializeObject, CharacterData.DeserializeObject);
			Method?.Invoke(typeof(Vec3), 2, Vec3.SerializeObject, Vec3.DeserializeObject);

        }
    }
}
