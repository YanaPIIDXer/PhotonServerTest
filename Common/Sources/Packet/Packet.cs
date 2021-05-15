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
        /// 送信用パラメータDictionary
        /// </summary>
        public Dictionary<byte, object> SendParamsDictionary { get { return Params; } }

        /// <summary>
        /// 送信用コード
        /// </summary>
        public abstract byte SendCode { get; }

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

        /// <summary>
        /// パラメータをセット
        /// </summary>
        /// <param name="ParamCode">パラメータコード</param>
        /// <param name="Param">パラメータ</param>
        public void SetParam(byte ParamCode, object Param)
        {
            Params[ParamCode] = Param;
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
        /// 送信用コード
        /// </summary>
        public override byte SendCode => (byte)Code;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Code">イベントコード</param>
        public EventPacket(EEventCode Code)
            : base(new Dictionary<byte, object>())
        {
            this.Code = Code;
        }

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
        public EOperationCode Code { get; private set; }

        /// <summary>
        /// 送信用コード
        /// </summary>
        public override byte SendCode => (byte)Code;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Code">オペレーションコード</param>
        public OperationPacket(EOperationCode Code)
            : base(new Dictionary<byte, object>())
        {
            this.Code = Code;
        }

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