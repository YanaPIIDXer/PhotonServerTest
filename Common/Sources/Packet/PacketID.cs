namespace Common.Packet
{
    public enum EPacketID
    {
    	/// <summary>
		/// プレイヤーリストパケット
		/// </summary>
		PlayerList = 0x01,
		/// <summary>
		/// プレイヤー移動パケット
		/// </summary>
		PlayerMove = 0x02,
		/// <summary>
		/// 他人移動パケット
		/// </summary>
		OtherPlayerMove = 0x03,
		
	}
}
