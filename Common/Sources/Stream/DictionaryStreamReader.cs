using System.Collections.Generic;

namespace Common.Stream
{
    /// <summary>
    /// Dictionaryからデータを読み込むクラス
    /// </summary>
    public class DictionaryStreamReader : IDictionaryStream
    {
        /// <summary>
        /// Dictionary
        /// </summary>
        private Dictionary<byte, object> Src = null;

        /// <summary>
        /// 現在位置
        /// </summary>
        private byte CurrentPosition = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Src">Dictionary</param>
        public DictionaryStreamReader(Dictionary<byte, object> Src)
        {
            this.Src = Src;
        }

        public bool Serialize(ref int Data)
        {
            Data = (int)Src[CurrentPosition];
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref uint Data)
        {
            Data = (uint)Src[CurrentPosition];
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref short Data)
        {
            Data = (short)Src[CurrentPosition];
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref ushort Data)
        {
            Data = (ushort)Src[CurrentPosition];
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref char Data)
        {
            Data = (char)Src[CurrentPosition];
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref byte Data)
        {
            Data = (byte)Src[CurrentPosition];
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref float Data)
        {
            Data = (float)Src[CurrentPosition];
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref string Data)
        {
            Data = (string)Src[CurrentPosition];
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ref char[] Data, int Length)
        {
            Data = (char[])Src[CurrentPosition];
            CurrentPosition++;
            return true;
        }

        public bool Serialize(ISerializable Data)
        {
            throw new System.NotImplementedException();
        }
    }
}
