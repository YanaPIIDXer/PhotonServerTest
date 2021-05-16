using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Common.Stream
{
    /// <summary>
    /// ストリーム読み込み
    /// </summary>
    public class MemoryStreamReader : IMemoryStream
    {
        /// <summary>
        /// バッファ
        /// </summary>
        private byte[] Buffer = null;

        /// <summary>
        /// 現在のストリームの位置
        /// </summary>
        private int CurrentPosition = 0;

        /// <summary>
        /// エラーが発生しているか？
        /// </summary>
        public bool IsError { get; private set; }

        /// <summary>
        /// 文字列エンコード
        /// </summary>
        public Encoding StringEncord { set; private get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="InBuffer">バッファ</param>
        public MemoryStreamReader(byte[] InBuffer)
        {
            Buffer = InBuffer;
            CurrentPosition = 0;
            IsError = false;
            StringEncord = Encoding.UTF8;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref int Data)
        {
            byte[] Bytes = null;
            if (!Read(ref Bytes, sizeof(int))) { return false; }

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(Bytes);
            }

            Data = BitConverter.ToInt32(Bytes, 0);
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref uint Data)
        {
            byte[] Bytes = null;
            if (!Read(ref Bytes, sizeof(uint))) { return false; }

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(Bytes);
            }

            Data = BitConverter.ToUInt32(Bytes, 0);
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref short Data)
        {
            byte[] Bytes = null;
            if (!Read(ref Bytes, sizeof(short))) { return false; }

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(Bytes);
            }

            Data = BitConverter.ToInt16(Bytes, 0);
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref ushort Data)
        {
            byte[] Bytes = null;
            if (!Read(ref Bytes, sizeof(ushort))) { return false; }

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(Bytes);
            }

            Data = BitConverter.ToUInt16(Bytes, 0);
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref char Data)
        {
            byte[] Bytes = null;
            if (!Read(ref Bytes, 1)) { return false; }

            Data = (char)Bytes[0];
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref byte Data)
        {
            byte[] Bytes = null;
            if (!Read(ref Bytes, 1)) { return false; }

            Data = Bytes[0];
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref float Data)
        {
            byte[] Bytes = null;
            if (!Read(ref Bytes, sizeof(float))) { return false; }

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(Bytes);
            }

            Data = BitConverter.ToSingle(Bytes, 0);
            return true;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        /// <returns>成功したらtrueを返す</returns>
        public bool Serialize(ref string Data)
        {
            int Length = 0;
            if (!Serialize(ref Length)) { return false; }

            byte[] Bytes = null;
            if (!Read(ref Bytes, Length)) { return false; }

            Data = StringEncord.GetString(Bytes);
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
            byte[] Bytes = null;
            if (!Read(ref Bytes, Length)) { return false; }

            Data = new char[Length];
            Array.Copy(Bytes, Data, Length);
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
        /// 読み込み
        /// </summary>
        /// <param name="Data">データ格納用のバイト配列領域</param>
        /// <param name="ReadByte">読み込みバイト数</param>
        /// <returns>成功したらtrueを返す</returns>
        private bool Read(ref byte[] Data, int ReadByte)
        {
            if (CurrentPosition + ReadByte > Buffer.Length)
            {
                IsError = true;
                return false;
            }

            Data = new byte[ReadByte];
            Array.Copy(Buffer, CurrentPosition, Data, 0, ReadByte);

            CurrentPosition += ReadByte;
            return true;
        }
    }
}
