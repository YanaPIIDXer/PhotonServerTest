using System.Collections.Generic;

namespace Common.Packet
{
    /// <summary>
    /// 送信データ
    /// </summary>
    public class SendData
    {
        /// <summary>
        /// パラメータが入ったDictionary
        /// </summary>
        private Dictionary<byte, object> Params = new Dictionary<byte, object>();

        /// <summary>
        /// 送信用Dictionary
        /// </summary>
        public Dictionary<byte, object> SendDictionary { get { return Params; } }

        /// <summary>
        /// 送信用コード
        /// </summary>
        public byte SendCode { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="SendCode">送信コード</param>
        /// <param name="SendPacket">送信パケット</param>
        public SendData(byte SendCode, object SendPacket)
        {
            this.SendCode = SendCode;
            Params.Add(0, SendPacket);
        }
    }

    /// <summary>
    /// パケット
    /// </summary>
    public abstract class Packet
    {
        /// <summary>
        /// 送信コード
        /// </summary>
        protected abstract byte SendCode { get; }

        /// <summary>
        /// 送信データ生成
        /// </summary>
        /// <returns>送信データ</returns>
        public SendData MakeSendData()
        {
            return new SendData(SendCode, this);
        }
    }
}
