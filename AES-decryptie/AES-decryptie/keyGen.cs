using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
namespace AES_decryptie
{
    public class keyGen
    {
        private int Nb = 4;
        private int Nk = 4;
        private int Nr = 10;
        private byte[] key;
        private byte[,] w;
        private byte[,] Rcon;
        private byte[,] Sbox;

        public keyGen()
        {
            BuildRcon();
            BuildSbox();

        }

        private byte[] SubWord(byte[] word)
        {
            byte[] result = new byte[4];
            result[0] = this.Sbox[word[0] >> 4, word[0] & 0x0f];
            result[1] = this.Sbox[word[1] >> 4, word[1] & 0x0f];
            result[2] = this.Sbox[word[2] >> 4, word[2] & 0x0f];
            result[3] = this.Sbox[word[3] >> 4, word[3] & 0x0f];
            return result;
        }

        private void BuildSbox()
        {
            this.Sbox = new byte[16, 16] {  

     {0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76},
     {0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0},
     {0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15},
     {0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75},
     {0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84},
     {0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf},
     {0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8},
     {0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2},
     {0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73},
     {0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb},
     {0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79},
     {0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08},
     {0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a},
     {0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e},
     {0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf},
     {0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16} };

        }

        private byte[] RotWord(byte[] word)
        {
            byte[] result = new byte[4];
            result[0] = word[1];
            result[1] = word[2];
            result[2] = word[3];
            result[3] = word[0];
            return result;
        }

        private void BuildRcon()
        {
            this.Rcon = new byte[11, 4] { {0x00, 0x00, 0x00, 0x00},  
                                   {0x01, 0x00, 0x00, 0x00},
                                   {0x02, 0x00, 0x00, 0x00},
                                   {0x04, 0x00, 0x00, 0x00},
                                   {0x08, 0x00, 0x00, 0x00},
                                   {0x10, 0x00, 0x00, 0x00},
                                   {0x20, 0x00, 0x00, 0x00},
                                   {0x40, 0x00, 0x00, 0x00},
                                   {0x80, 0x00, 0x00, 0x00},
                                   {0x1b, 0x00, 0x00, 0x00},
                                   {0x36, 0x00, 0x00, 0x00} };
        }



        public String createKey()
        {

            // create a writer and open the file
            TextWriter tw = new StreamWriter("key.txt");


            // write a line of text to the file
            String key = CreateKey(16);
            tw.WriteLine(key);

            // close the stream
            tw.Close();

            return key;
        }


        static String CreateKey(int numBytes)
        {
            Random random = new Random();
            String password = "";
            for (int i = 0; i < numBytes; i++)
            {
                password += random.Next(10);
            }
            return password;
        }

        public void KeyExpantsion(String key)
        {
            byte[] bkey = new byte[16];
            int i = 0;
            foreach (char c in key)
            {
                bkey[i] = byte.Parse(c.ToString());
                i++;
            }
            this.key = bkey;
        }

        public byte[,] KeyExpansion()
        {
            this.w = new byte[Nb * (Nr + 1), 4];  // 4 columns of bytes corresponds to a word

            for (int row = 0; row < Nk; ++row)
            {
                this.w[row, 0] = this.key[4 * row];
                this.w[row, 1] = this.key[4 * row + 1];
                this.w[row, 2] = this.key[4 * row + 2];
                this.w[row, 3] = this.key[4 * row + 3];
            }

            byte[] temp = new byte[4];

            for (int row = Nk; row < Nb * (Nr + 1); ++row)
            {
                temp[0] = this.w[row - 1, 0]; temp[1] = this.w[row - 1, 1];
                temp[2] = this.w[row - 1, 2]; temp[3] = this.w[row - 1, 3];

                if (row % Nk == 0)
                {
                    temp = SubWord(RotWord(temp));

                    temp[0] = (byte)((int)temp[0] ^ (int)this.Rcon[row / Nk, 0]);
                    temp[1] = (byte)((int)temp[1] ^ (int)this.Rcon[row / Nk, 1]);
                    temp[2] = (byte)((int)temp[2] ^ (int)this.Rcon[row / Nk, 2]);
                    temp[3] = (byte)((int)temp[3] ^ (int)this.Rcon[row / Nk, 3]);
                }
                else if (Nk > 6 && (row % Nk == 4))
                {
                    temp = SubWord(temp);
                }

                this.w[row, 0] = (byte)((int)this.w[row - Nk, 0] ^ (int)temp[0]);
                this.w[row, 1] = (byte)((int)this.w[row - Nk, 1] ^ (int)temp[1]);
                this.w[row, 2] = (byte)((int)this.w[row - Nk, 2] ^ (int)temp[2]);
                this.w[row, 3] = (byte)((int)this.w[row - Nk, 3] ^ (int)temp[3]);

            }  // for loop

            return w;
        }


    }
}