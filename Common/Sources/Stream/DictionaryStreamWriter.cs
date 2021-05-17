using System.Collections.Generic;

namespace Common.Stream
{
    /// <summary>
    /// Dictionaryにデータを書き込むクラス
    /// </summary>
    public class DictionaryStreamWriter : IDictionaryStream
    {
        /// <summary>
        /// 出力先Dictionary
        /// </summary>
        public Dictionary<byte, object> Dest { get; private set; } = new Dictionary<byte, object>();

        /// <summary>
        /// 現在位置
        /// </summary>
        private byte CurrentPosition = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DictionaryStreamWriter()
        {
        }

        public bool Serialize(ref int Data)
        {
            Dest[CurrentPosition] = Data;
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref uint Data)
        {
            Dest[CurrentPosition] = Data;
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref short Data)
        {
            Dest[CurrentPosition] = Data;
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref ushort Data)
        {
            Dest[CurrentPosition] = Data;
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref char Data)
        {
            Dest[CurrentPosition] = Data;
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref byte Data)
        {
            Dest[CurrentPosition] = Data;
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref float Data)
        {
            Dest[CurrentPosition] = Data;
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref string Data)
        {
            Dest[CurrentPosition] = Data;
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref char[] Data, int Length)
        {
            Dest[CurrentPosition] = Data;
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ISerializable Data)
        {
            Dest[CurrentPosition] = Data;
            CurrentPosition++;
            return true;
        }
    }
}
