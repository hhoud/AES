using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AES_encryptie
{
    class Shifting
    {
        public byte[,] shiftRows(byte[,] block)
        {
            //Create an empty byte array which will contain the shifted matrix.
            byte[,] shifted = new byte[4,4];

            //Loop over the bytes in the matrix
            for(int i=0; i<4 ; i++){
                for (int j = 0; j < 4; j++)
                {
                    //Copy the bytes to the left depending on the row.
                    //The first row doesn't change, the second is shifted one to the 
                    //left and the first bytes are appended at the end of the row.
                    shifted[i, j] = block[i, (i + j)%4];
                }
            }
            return shifted;
        }
    }
}
