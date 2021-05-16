using System;

namespace Common.Packet
{
    public static class RegisterPacketType
    {
        public static void RegisterPackets(Func<Type, byte, Func<object, byte[]>, Func<byte[], object>, bool> Method)
        {
			Method?.Invoke(typeof(PacketLogInRequest), PacketLogInRequest.PacketID, PacketLogInRequest.SerializeObject, PacketLogInRequest.DeserializeObject);
			Method?.Invoke(typeof(PacketLogInResult), PacketLogInResult.PacketID, PacketLogInResult.SerializeObject, PacketLogInResult.DeserializeObject);
			Method?.Invoke(typeof(PacketClientReady), PacketClientReady.PacketID, PacketClientReady.SerializeObject, PacketClientReady.DeserializeObject);
			Method?.Invoke(typeof(PacketPlayerList), PacketPlayerList.PacketID, PacketPlayerList.SerializeObject, PacketPlayerList.DeserializeObject);
			Method?.Invoke(typeof(PacketPlayerMove), PacketPlayerMove.PacketID, PacketPlayerMove.SerializeObject, PacketPlayerMove.DeserializeObject);
			Method?.Invoke(typeof(PacketOtherPlayerMove), PacketOtherPlayerMove.PacketID, PacketOtherPlayerMove.SerializeObject, PacketOtherPlayerMove.DeserializeObject);

        }
    }
}
