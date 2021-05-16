using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Stream
{
    /// <summary>
    /// ストリーム書き込み
    /// </summary>
    public class MemoryStreamWriter : IMemoryStream
    {
        /// <summary>
        /// バッファ
        /// </summary>
        public List<byte> Buffer { get; private set; }

        /// <summary>
        /// 文字列エンコード
        /// </summary>
        public Encoding StringEncord { set; private get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MemoryStreamWriter()
        {
            Buffer = new List<byte>();
            StringEncord = Encoding.UTF8;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref int Data)
        {
            byte[] Bytes = BitConverter.GetBytes(Data);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(Bytes);
            }

            Write(Bytes);
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref uint Data)
        {
            byte[] Bytes = BitConverter.GetBytes(Data);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(Bytes);
            }

            Write(Bytes);
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref short Data)
        {
            byte[] Bytes = BitConverter.GetBytes(Data);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(Bytes);
            }

            Write(Bytes);
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref ushort Data)
        {
            byte[] Bytes = BitConverter.GetBytes(Data);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(Bytes);
            }

            Write(Bytes);
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref char Data)
        {
            byte[] Bytes = new byte[1];
            Bytes[0] = (byte)Data;

            Write(Bytes);
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref byte Data)
        {
            byte[] Bytes = new byte[1];
            Bytes[0] = Data;

            Write(Bytes);
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref float Data)
        {
            byte[] Bytes = BitConverter.GetBytes(Data);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(Bytes);
            }

            Write(Bytes);
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref string Data)
        {
            int Length = Data.Length;
            if (!Serialize(ref Length)) { return false; }

            byte[] Bytes = StringEncord.GetBytes(Data);
            Write(Bytes);
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <param name="Length">バイト長</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref char[] Data, int Length)
        {
            byte[] Bytes = new byte[Length];

            Array.Copy(Data, Bytes, Length);
            Write(Bytes);
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ISerializable Data)
        {
            return Data.Serialize(this);
        }

        /// <summary>
        /// 書き込み
        /// </summary>
        /// <param name="Bytes">バイト配列</param>
        /// <returns>成功したらtrueを返す</returns>
        private void Write(byte[] Bytes)
        {
            Buffer.AddRange(Bytes);
        }
    }
}
