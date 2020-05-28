using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Classes;
using Newtonsoft.Json;
namespace Launcher
{
    public partial class Form1 : Form
    {
        public Player player;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fl = new OpenFileDialog();
            fl.Filter = "jpg|*jpg|png | *png";
            fl.Title = "ProfileImage";
            fl.CheckFileExists = true;
            if(fl.ShowDialog() == DialogResult.OK)
            {
                
                pictureBox1.Image = Image.FromFile(fl.FileName);
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            Form2 Dialog = new Form2();
            if (Dialog.ShowDialog(this) == DialogResult.OK)
            {
                if (Dialog.textBox1.Text != "")
                {
                    label1.Text = Dialog.textBox1.Text;
                }
            }
        }
    }
}
