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
            /*
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
             * */

            block = InvMixColumns(block);
            return block;
        }

        //This method will multiply the column with the MDS Matrix in Rijndael's Galois Field.
        private byte[] multiplyMatrix(byte[] column)
        {
            //Copy the 
            byte[] a = column;

            column[0] = (byte)(galoisMultiply(a[0],14) ^ galoisMultiply(a[3],9) ^ galoisMultiply(a[2],13) ^ galoisMultiply(a[1],11));
            column[1] = (byte)(galoisMultiply(a[1],14) ^ galoisMultiply(a[0],9) ^ galoisMultiply(a[3],13) ^ galoisMultiply(a[2],11));
            column[2] = (byte)(galoisMultiply(a[2],14) ^ galoisMultiply(a[1],9) ^ galoisMultiply(a[0],13) ^ galoisMultiply(a[3],11));
            column[3] = (byte)(galoisMultiply(a[3],14) ^ galoisMultiply(a[2],9) ^ galoisMultiply(a[1],13) ^ galoisMultiply(a[0],11));
            

            return column;
        }

        private byte galoisMultiply(byte a, byte b)
        {
            byte p = 0;
	        int hi_bit_set;
	        for(int counter = 0; counter < 8; counter++) {
		        if((b & 1) == 1) 
			        p ^= a;
		        hi_bit_set = (a & 0x80);
		        a <<= 1;
		        if(hi_bit_set == 0x80) 
			        a ^= 0x1b;		
		        b >>= 1;
	        }
	        return p;
        }

        private byte[,] InvMixColumns(byte[,] state)
        {
            byte[,] temp = new byte[4, 4];
            for (int r = 0; r < 4; ++r)  // copy State into temp[]
            {
                for (int c = 0; c < 4; ++c)
                {
                    temp[r, c] = state[r, c];
                }
            }

            for (int c = 0; c < 4; ++c)
            {
                state[0, c] = (byte)((int)gfmultby0e(temp[0, c]) ^ (int)gfmultby0b(temp[1, c]) ^
                                           (int)gfmultby0d(temp[2, c]) ^ (int)gfmultby09(temp[3, c]));
                state[1, c] = (byte)((int)gfmultby09(temp[0, c]) ^ (int)gfmultby0e(temp[1, c]) ^
                                           (int)gfmultby0b(temp[2, c]) ^ (int)gfmultby0d(temp[3, c]));
                state[2, c] = (byte)((int)gfmultby0d(temp[0, c]) ^ (int)gfmultby09(temp[1, c]) ^
                                           (int)gfmultby0e(temp[2, c]) ^ (int)gfmultby0b(temp[3, c]));
                state[3, c] = (byte)((int)gfmultby0b(temp[0, c]) ^ (int)gfmultby0d(temp[1, c]) ^
                                           (int)gfmultby09(temp[2, c]) ^ (int)gfmultby0e(temp[3, c]));
            }
            return state;
        }  // InvMixColumns

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
