using System.Collections.Generic;
using Common.Code;

namespace Common.Packet
{
    /// <summary>
    /// パケット基底クラス
    /// </summary>
    public abstract class PacketBase
    {

        /// <summary>
        /// パラメータが入ったDictionary
        /// </summary>
        private Dictionary<byte, object> Params = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Params">パラメータが入ったDictionary</param>
        public PacketBase(Dictionary<byte, object> Params)
        {
            this.Params = Params;
        }

        /// <summary>
        /// パラメータ取得
        /// </summary>
        /// <param name="ParamCode">パラメータコード</param>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <returns>パラメータ</returns>
        public T GetParam<T>(byte ParamCode)
        {
            object Param = Params[ParamCode];
            return (T)Param;
        }
    }

    /// <summary>
    /// イベント用パケット
    /// </summary>
    public class EventPacket : PacketBase
    {
        /// <summary>
        /// イベントコード
        /// </summary>
        public EEventCode Code { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Code">イベントコード</param>
        /// <param name="Params">パラメータが入ったDictionary</param>
        public EventPacket(byte Code, Dictionary<byte, object> Params)
            : base(Params)
        {
            this.Code = (EEventCode)Code;
        }
    }

    /// <summary>
    /// オペレーション用パケット
    /// </summary>
    public class OperationPacket : PacketBase
    {
        /// <summary>
        /// オペレーションコード
        /// </summary>
        public EOperationCode Code { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Code">オペレーションコード</param>
        /// <param name="Params">パラメータが入ったDictionary</param>
        public OperationPacket(byte Code, Dictionary<byte, object> Params)
            : base(Params)
        {
            this.Code = (EOperationCode)Code;
        }
    }
}
