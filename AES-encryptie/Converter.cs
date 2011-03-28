using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AES_encryptie
{
    class Converter
    {
        private byte[] _bufferReal;
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
                _Buffer = _BinaryReader.ReadBytes((Int32)(_TotalBytes + 16 - (_TotalBytes % 16)));
                _bufferReal = new byte[_TotalBytes + 16 - (_TotalBytes % 16)];
                _Buffer.CopyTo(_bufferReal,0);
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

            int toPad;
            if ((_Buffer.Length % 16) != 0)
            {
                toPad = 16 - (_Buffer.Length % 16);
                for (int i = 0; i < toPad; i++)
                {
                    _bufferReal[_Buffer.Length+i] = Convert.ToByte(toPad);
                    
                }
            }
            return _bufferReal;

        }

        public byte[] convertMatrixArrayToByteArray(List<byte[,]> data,int length)
        {
            byte[] dataStream = new byte[length];
            int k =0;
            foreach (byte[,] matrix in data)
            {
                for (int i = 0; i <= 3; i++)
                {
                    for (int j = 0; j <= 3; j++)
                    {
                        dataStream[k] = matrix[i, j];
                        k++;
                    }
                }
            }
        

            return dataStream;
        }
    }
}
