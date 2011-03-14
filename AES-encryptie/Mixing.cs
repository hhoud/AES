using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AES_encryptie
{
    class Mixing
    {
        public byte[,] mixColumns(byte[,] block)
        {
            byte[,] mixed = new byte[4, 4];
            //Loop over the columns of the matrix
            for (int i = 0; i < 4; i++)
            {
                //Get the bytes from one column out of the matrix and put them in a new byte array.
                byte[] column = new byte[4];
                for (int j = 0; j < 4; j++)
                {
                    column[j] = block[j, i];
                }
                
                //Multiply the column with the MDS matrix in Rijndael's Galois Field
                byte[] newcolumn = multiplyMatrix(column);

                //Put the new bytes in the block.
                for (int k = 0; k < 4; k++)
                {
                    mixed[k, i] = column[k];
                }
            }
            //return the matrix with the mixed columns.
            return mixed;
        }

        private byte[] multiplyMatrix(byte[] column)
        {
            /* The array 'a' is simply a copy of the input array 'column'
             * The array 'b' is each element of the array 'a' multiplied by 2
             *      in Rijndael's Galois field
             * a[n] ^ b[n] is element n multiplied by 3 in Rijndael's Galois field */ 

            byte[] a = new byte[4];
            byte[] b = new byte[4];

            for(int c=0;c<4;c++) {
                a[c] = column[c];
                int h = column[c] & 0x80; /* hi bit */
                b[c] = (byte)((column[c] << 1) & 0xFF);
                if(h == 0x80)
                    b[c] ^= 0x1B; /* Rijndael's Galois field */
            }
            column[0] = (byte)(b[0] ^ a[3] ^ a[2] ^ b[1] ^ a[1]); /* 2 * a0 + a3 + a2 + 3 * a1 */
            column[1] = (byte)(b[1] ^ a[0] ^ a[3] ^ b[2] ^ a[2]); /* 2 * a1 + a0 + a3 + 3 * a2 */
            column[2] = (byte)(b[2] ^ a[1] ^ a[0] ^ b[3] ^ a[3]); /* 2 * a2 + a1 + a0 + 3 * a3 */
            column[3] = (byte)(b[3] ^ a[2] ^ a[1] ^ b[0] ^ a[0]); /* 2 * a3 + a2 + a1 + 3 * a0 */

            return column;
        }
    }
}
