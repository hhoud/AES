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
            byte[,] test = new byte[4, 4] { { 142, 77, 161, 188 }, { 159, 220, 88, 157 }, { 1, 1, 1, 1 }, { 213, 213, 215, 214 } };
            /*
                62 7b ce b9     99 9d 5a aa     c9 45 ec f4     23 f5 6d a5
                round[ 1].im_col e51c9502a5c1950506a61024596b2b07 = { 229, 28, 149, 2 }, { 165, 193, 149, 5 }, { 6, 166, 16, 36 }, { 89, 107, 43, 7 } 
                round[ 1].ik_sch 34f1d1ffbfceaa2ffce9e25f2558016e
            */
            byte[,] test2 = new byte[4, 4] { { 98, 123, 206, 185 }, { 153, 157, 90, 170 }, { 201, 69, 236, 244 }, { 35, 245, 109, 165 } };
            Mixing mix = new Mixing();
            byte[,] result = mix.mixColumns(test2);
            Console.WriteLine("DONE!");
        }
    }
}
