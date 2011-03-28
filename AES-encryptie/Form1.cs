using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AES_encryptie
{
    public partial class Form1 : Form
    {
        //Global variables

        //The extention of the loaded document
        String ext;

        //The converter class
        AES aes = new AES();
        Converter converter = new Converter();
        Substitution subs = new Substitution();
        Shifting shifter = new Shifting();
        Mixing mixer = new Mixing();
        keyGen keygen = new keyGen();
        Addition addition = new Addition();

        //An object for the 1st step of an AES round
        Substitution substitution = new Substitution();

        public Form1()
        {
            InitializeComponent();

        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
           // Set the title of the ofd
           ofd.Title = "Select a file";

           //Check if the user selected something
           String file = (ofd.ShowDialog() == DialogResult.OK) ? ofd.FileName : "";

           // Get the extention of the loaded file
           ext =  System.IO.Path.GetExtension(file);

           List<byte[,]> data = new List<byte[,]>();

           //Convert the file into an array of bytes
           // Create the key
          String key =  keygen.createKey();
           keygen.KeyExpantsion(key);
          byte[,] w= keygen.KeyExpansion();

            
            
            byte[] datastream = converter.FileToByteArray(file);

            for (int i = 0; i < datastream.Length / 16; i++)
            {
                byte[] block = new byte[16];
                for(int j=0;j<16;j++)
                {
                    block[j] = datastream[i*16+j];
                }

                byte[,] state = addition.addKey(aes.convertTo2DArray(block), w, 0);

                for (int round = 1; round <= 8; round++)
                {
                    state = addition.addKey(mixer.mixColumns(shifter.shiftRows(subs.substitute(state))), w, round);
                }
                state = addition.addKey(shifter.shiftRows(subs.substitute(state)),w,9);

                data.Add(state);

            }

            converter.convertMatrixArrayToByteArray(data,datastream.Length); 
        }
    }
}
