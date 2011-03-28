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

        //This method will multiply the column with the MDS Matrix in Rijndael's Galois Field.
        private byte[] multiplyMatrix(byte[] column)
        {
            //Copy the bytes from the column
            byte[] a = column;

            //Do the multiplication for each row in the column.
            column[0] = (byte)(galoisMultiply(a[0],14) ^ galoisMultiply(a[3],9) ^ galoisMultiply(a[2],13) ^ galoisMultiply(a[1],11));
            column[1] = (byte)(galoisMultiply(a[1],14) ^ galoisMultiply(a[0],9) ^ galoisMultiply(a[3],13) ^ galoisMultiply(a[2],11));
            column[2] = (byte)(galoisMultiply(a[2],14) ^ galoisMultiply(a[1],9) ^ galoisMultiply(a[0],13) ^ galoisMultiply(a[3],11));
            column[3] = (byte)(galoisMultiply(a[3],14) ^ galoisMultiply(a[2],9) ^ galoisMultiply(a[1],13) ^ galoisMultiply(a[0],11));
            
            //Return the mixed column
            return column;
        }

        //This function performs a bitwise multiplication of two numbers
        private byte galoisMultiply(byte a, byte b)
        {
            byte p = 0;
	        int hi_bit_set;
            //Loop over all bits in the byte
	        for(int counter = 0; counter < 8; counter++) {
                //Check if the last bit of the second value is set (=1)
                //Xor byte p with the first value
		        if((b & 1) == 1) 
			        p ^= a;
                //Check if the most significant bit of the first value is set (=1)
		        hi_bit_set = (a & 0x80);
                //shift the first value 1 time to the left
		        a <<= 1;
                //if the highest bit is set, xor with x^4+x^3+x+1
                if (hi_bit_set == 0x80)
                    a ^= 0x1b;
                //shift the second value once to the right.
		        b >>= 1;
	        }
	        return p;
        }
    }
}
