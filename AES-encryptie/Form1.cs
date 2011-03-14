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

           //Convert the file into an array of bytes
           //1ste step = Substitution

           shifter.shiftRows(subs.substitute(aes.convertByteArrayToSingleMatrix(converter.FileToByteArray(file))));
           
        }
    }
}
