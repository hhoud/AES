using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AES_decryptie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            byte[,] test = new byte[4, 4] { { 142, 159, 1, 213 }, { 77, 220, 1, 213 }, { 161, 88, 1, 215 }, { 188, 157, 1, 214 } };
            Mixing mix = new Mixing();
            byte[,] result = mix.mixColumns(test);
            Console.WriteLine("DONE!");
        }
    }
}
