using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AES_decryptie
{
    class Mixing
    {
        public byte[,] mixColumns(byte[,] block)
        {
            byte[,] temp = new byte[4,4];
            for (int r = 0; r < 4; ++r)  // copy State into temp[]
            {
                   for (int c = 0; c < 4; ++c)
                     {
                        temp[r,c] = block[r,c];
                     }
            }

              for (int c = 0; c < 4; ++c)
              {
                  block[0, c] = (byte)((int)gfmultby0e(temp[0, c]) ^ (int)gfmultby0b(temp[1, c]) ^
                                   (int)gfmultby0d(temp[2, c]) ^ (int)gfmultby09(temp[3, c]));
                  block[1, c] = (byte)((int)gfmultby09(temp[0, c]) ^ (int)gfmultby0e(temp[1, c]) ^
                                             (int)gfmultby0b(temp[2, c]) ^ (int)gfmultby0d(temp[3, c]));
                  block[2, c] = (byte)((int)gfmultby0d(temp[0, c]) ^ (int)gfmultby09(temp[1, c]) ^
                                             (int)gfmultby0e(temp[2, c]) ^ (int)gfmultby0b(temp[3, c]));
                  block[3, c] = (byte)((int)gfmultby0b(temp[0, c]) ^ (int)gfmultby0d(temp[1, c]) ^
                                             (int)gfmultby09(temp[2, c]) ^ (int)gfmultby0e(temp[3, c]));
              }

            //return the matrix with the mixed columns.
            return block;
            
        }

        private static byte gfmultby01(byte b)
        {
            return b;
        }

        private static byte gfmultby02(byte b)
        {
            if (b < 0x80)
                return (byte)(int)(b << 1);
            else
                return (byte)((int)(b << 1) ^ (int)(0x1b));
        }

        private static byte gfmultby03(byte b)
        {
            return (byte)((int)gfmultby02(b) ^ (int)b);
        }

        private static byte gfmultby09(byte b)
        {
            return (byte)((int)gfmultby02(gfmultby02(gfmultby02(b))) ^
                           (int)b);
        }

        private static byte gfmultby0b(byte b)
        {
            return (byte)((int)gfmultby02(gfmultby02(gfmultby02(b))) ^
                           (int)gfmultby02(b) ^
                           (int)b);
        }

        private static byte gfmultby0d(byte b)
        {
            return (byte)((int)gfmultby02(gfmultby02(gfmultby02(b))) ^
                           (int)gfmultby02(gfmultby02(b)) ^
                           (int)(b));
        }

        private static byte gfmultby0e(byte b)
        {
            return (byte)((int)gfmultby02(gfmultby02(gfmultby02(b))) ^
                           (int)gfmultby02(gfmultby02(b)) ^
                           (int)gfmultby02(b));
        }
    }
}
