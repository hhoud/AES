using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AES_encryptie
{
    class Addition
    {

        public byte[,] addKey(byte[,] matrix,byte[,]w,int round) {
            for (int r = 0; r < 4; ++r)
            {
                for (int c = 0; c < 4; ++c)
                {
                    matrix[r, c] = (byte)((int)matrix[r, c] ^ (int)w[(round * 4) + c, r]);
                }
            }

            return matrix;
        
        }
    }
}
