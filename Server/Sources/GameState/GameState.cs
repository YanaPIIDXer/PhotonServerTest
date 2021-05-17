using System;
using System.Collections.Generic;
using Photon.SocketServer;
using Common.Packet;
using Common.Stream;

namespace GameState
{
    /// <summary>
    /// ゲームステート基底クラス
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// 親
        /// </summary>
        protected GamePeer Parent { get; private set; }

        /// <summary>
        /// オペレーションのハンドラDictionary
        /// </summary>
        private Dictionary<EPacketID, Func<IDictionaryStream, IPacket>> OperationHandlers = new Dictionary<EPacketID, Func<IDictionaryStream, IPacket>>();

        /// <summary>
        /// オペレーションのハンドラ追加
        /// </summary>
        /// <param name="PacketID">パケットＩＤ</param>
        /// <param name="Handler">ハンドラ</param>
        protected void AddOperationHandler(EPacketID PacketID, Func<IDictionaryStream, IPacket> Handler)
        {
            OperationHandlers.Add(PacketID, Handler);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Parent">親</param>
        public GameState(GamePeer Parent)
        {
            this.Parent = Parent;
        }

        /// <summary>
        /// オペレーションを受信した
        /// </summary>
        /// <param name="PacketID">パケットＩＤ</param>
        /// <param name="Params">パラメータ</param>
        public void OnRecvOperation(EPacketID PacketID, Dictionary<byte, object> Params)
        {
            if (OperationHandlers.ContainsKey(PacketID))
            {
                DictionaryStreamReader Reader = new DictionaryStreamReader(Params);
                var ResponsePacket = OperationHandlers[PacketID]?.Invoke(Reader);
                if (ResponsePacket != null)      // ResponsePacketがnullならReportとして扱う
                {
                    DictionaryStreamWriter Writer = new DictionaryStreamWriter();
                    ResponsePacket.Serialize(Writer);
                    var Response = new OperationResponse((byte)ResponsePacket.PacketID, Writer.Dest);
                    Parent.SendOperationResponse(Response, new SendParameters());
                }
            }
        }
    }
}
