using Common.Code;

namespace Common.Packet
{
    /// <summary>
    /// パケット基底クラス
    /// クライアントやサーバが直接使うことはない
    /// </summary>
    public abstract class PacketBase
    {
        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="ParameterDic">パラメータを突っ込むDictionary</param>
        public abstract void Serialize(ref Dictionary<byte, object> ParameterDic);

        /// <summary>
        /// デシリアライズ
        /// </summary>
        /// <param name="ParameterDic">パラメータが入ったDictionary</param>
        public abstract void Deserialize(Dictionary<byte, object> ParameterDic);
    }

    /// <summary>
    /// イベント用パケット
    /// </summary>
    public abstract class EventPacket : PacketBase
    {
        /// <summary>
        /// イベントコード
        /// </summary>
        public abstract EEventCode Code { get; }
    }

    /// <summary>
    /// オペレーション用パケット
    /// </summary>
    public abstract class OperationPacket : PacketBase
    {
        /// <summary>
        /// オペレーションコード
        /// </summary>
        public abstract EOperationCode Code { get; }
    }
}
