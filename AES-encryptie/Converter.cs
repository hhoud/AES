using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AES_encryptie
{
    class Converter
    {

        public BitArray convertByteToBitArray(byte b) {
            BitArray bits = new BitArray(b);
            return bits;
        }

        public BitArray convertByteArrayToBitArray(byte[] bytes)
        {
            BitArray bits = new BitArray(bytes);
            return bits;
        }


        public byte convertBitArraytoByte(BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }

        public byte[] FileToByteArray(string _FileName)
        {

            byte[] _Buffer = null;

            try
            {

                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // attach filestream to binary reader
                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);

                // get total byte length of the file
                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;

                // read entire file into buffer
                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);

                // close file reader
                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();
            }

            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            return _Buffer;

        }
    }
}
