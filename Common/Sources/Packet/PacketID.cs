﻿namespace Common.Packet
{
    public enum EPacketID
    {
    	/// <summary>
		/// ログイン要求パケット
		/// </summary>
		LogInRequest = 0x01,
		/// <summary>
		/// ログイン結果パケット
		/// </summary>
		LogInResult = 0x02,
		/// <summary>
		/// 準備完了パケット
		/// </summary>
		ClientReady = 0x03,
		/// <summary>
		/// プレイヤーリストパケット
		/// </summary>
		PlayerList = 0x04,
		/// <summary>
		/// プレイヤー登場パケット
		/// </summary>
		PlayerEnter = 0x05,
		/// <summary>
		/// プレイヤー移動パケット
		/// </summary>
		PlayerMove = 0x06,
		/// <summary>
		/// 他人移動パケット
		/// </summary>
		OtherPlayerMove = 0x07,
		/// <summary>
		/// プレイヤー離脱パケット
		/// </summary>
		PlayerLeave = 0x08,
		
	}
}