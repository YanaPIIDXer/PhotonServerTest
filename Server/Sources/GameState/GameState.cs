using System;
using System.Collections.Generic;
using Common.Code;
using Photon.SocketServer;

namespace GameState
{
    using PacketParam = Dictionary<byte, object>;

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
        private Dictionary<EOperationCode, Func<PacketParam, PacketParam>> OperationHandlers = new Dictionary<EOperationCode, Func<PacketParam, PacketParam>>();

        /// <summary>
        /// オペレーションのハンドラ追加
        /// </summary>
        /// <param name="Code">オペレーションコード</param>
        /// <param name="Handler">ハンドラ</param>
        protected void AddOperationHandler(EOperationCode Code, Func<PacketParam, PacketParam> Handler)
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
        public void OnRecvOperation(EOperationCode Code, PacketParam Params)
        {
            if (OperationHandlers.ContainsKey(Code))
            {
                var ResponseParam = OperationHandlers[Code]?.Invoke(Params);
                if (ResponseParam != null)      // ResponseParamがnullならReportとして扱う
                {
                    var Response = new OperationResponse((byte)Code, ResponseParam);
                    Parent.SendOperationResponse(Response, new SendParameters());
                }
            }
        }
    }
}
