using Common.Stream;

namespace Common.Packet
{
    /// <summary>
    /// パケット基底クラス
    /// </summary>
    public abstract class Packet
    {
        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Stream">ストリーム</param>
        public abstract void Serialize(IDictioanryStream Stream);
    }
}
