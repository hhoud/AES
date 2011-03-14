using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AES_encryptie
{
    public class AES
    {

        Shifting shifter = new Shifting();

        public byte [,] convertByteArrayToSingleMatrix(byte[] b){
            byte[] startmat = new byte[16];

            for (int i = 0; i < b.Length; i++)
            {
                if ((i % 16 == 0)&&(i != 0)) {
                    return convertTo2DArray(startmat);
                }
                startmat[i % 16] = b[i];
            }
            return null;
                 
        }


        private byte[,] convertTo2DArray(byte[] p)
        {
            int k = 0;
            byte[,] matrix = new byte[4, 4];
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    matrix[i, j] = p[k];
                    k++;
                }
            }
            return matrix;

        }

    }
}
