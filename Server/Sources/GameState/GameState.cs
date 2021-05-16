using System;
using System.Collections.Generic;
using Common.Code;
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
        private Dictionary<EOperationCode, Func<IDictioanryStream, Packet>> OperationHandlers = new Dictionary<EOperationCode, Func<IDictioanryStream, Packet>>();

        /// <summary>
        /// オペレーションのハンドラ追加
        /// </summary>
        /// <param name="Code">オペレーションコード</param>
        /// <param name="Handler">ハンドラ</param>
        protected void AddOperationHandler(EOperationCode Code, Func<IDictioanryStream, Packet> Handler)
        {
            OperationHandlers.Add(Code, Handler);
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
        /// <param name="Code">オペレーションコード</param>
        /// <param name="Params">パラメータ</param>
        public void OnRecvOperation(EOperationCode Code, Dictionary<byte, object> Params)
        {
            if (OperationHandlers.ContainsKey(Code))
            {
                DictionaryStreamReader Reader = new DictionaryStreamReader(Params);
                var ResponsePacket = OperationHandlers[Code]?.Invoke(Reader);
                if (ResponsePacket != null)      // ResponsePacketがnullならReportとして扱う
                {
                    DictionaryStreamWriter Writer = new DictionaryStreamWriter();
                    ResponsePacket.Serialize(Writer);
                    var Response = new OperationResponse((byte)Code, Writer.Dest);
                    Parent.SendOperationResponse(Response, new SendParameters());
                }
            }
        }
    }
}
