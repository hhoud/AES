using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AES_encryptie;
using System.IO;

namespace AES_decryptie
{
    public partial class Form1 : Form
    {

        //The extention of the loaded document
        String ext;
        keyGen keygen = new keyGen();
        Converter converter = new Converter();
        Addition addition = new Addition();
        AES aes = new AES();
        Mixing mixer = new Mixing();
        Shifting shifter = new Shifting();
        Substitution subs = new Substitution();

        String key;
        public Form1()
        {
            InitializeComponent();
            

        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            ofd.Title = "Select a file";

            //Check if the user selected something
            String file = (ofd.ShowDialog() == DialogResult.OK) ? ofd.FileName : "";

            // Get the extention of the loaded file
            ext = System.IO.Path.GetExtension(file);

            List<byte[,]> data = new List<byte[,]>();

            //Convert the file into an array of bytes
            //Get the key from file
            keygen.KeyExpantsion(key);
            byte[,] w = keygen.KeyExpansion();

                        
            byte[] datastream = converter.FileToByteArray(file);
            Console.WriteLine(datastream);
            
            for (int i = 0; i < datastream.Length / 16; i++)
            {
                byte[] block = new byte[16];
                for(int j=0;j<16;j++)
                {
                    block[j] = datastream[i*16+j];
                }


                byte[,] state = subs.substitute(shifter.shiftRows(addition.addKey(aes.convertTo2DArray(block), w, 10)));

                for (int round = 9; round >= 1; round--)
                {
                    state = subs.substitute(shifter.shiftRows(mixer.mixColumns(addition.addKey(state,w,round)))) ;
                }

                state = addition.addKey(state, w, 0);

                data.Add(state);
            }

            string saveTo = System.IO.Path.GetFileNameWithoutExtension(file)+ "_decr" + ext;
            // create a write stream
            FileStream writeStream = new FileStream(saveTo, FileMode.Create, FileAccess.Write);
            // write to the stream
            MemoryStream readStream = new MemoryStream(converter.convertMatrixArrayToByteArray(data, datastream.Length));
            ReadWriteStream(readStream, writeStream);

            
        }
        private void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);

                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGetKey_Click(object sender, EventArgs e)
        {
            //Check if the user selected something
            String file = (ofd.ShowDialog() == DialogResult.OK) ? ofd.FileName : "";

            // Get the extention of the loaded file
            ext = System.IO.Path.GetExtension(file);
            key = keygen.ReadFromFile(file);
        }
    }
}
