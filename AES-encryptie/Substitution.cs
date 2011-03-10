using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AES_encryptie
{
    public class Substitution
    {

        public byte[] substitute(byte[] data) {
            generateSBoxes();


            return data;
        }

        // This function generates the S-Boxes
        private byte[] generateSBoxes()
        {
            return null;
        }

    }
}
