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
using Newtonsoft.Json;
namespace Launcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            comboBox1.SelectedIndex = 0;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                ConnectForm cf = new ConnectForm();
                int port;
                string ip;
                if (cf.ShowDialog() == DialogResult.OK)
                {
                    port = int.Parse(cf.textBox2.Text);
                    ip = cf.textBox1.Text;                
                    switch(comboBox1.SelectedItem)
                    {
                        case "TicTocToe":
                            Form3 client = new Form3(port, ip, pictureBox1.Image, label1.Text,3);
                            client.Show();
                            break;
                    }
                }

            }
            else
            {
                MessageBox.Show("pick a game", "Pick a game", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnectForm cf = new ConnectForm();
            int port;
            string ip;
            cf.Text = "CreateServer";
            cf.button1.Text = "CreateServer";
            if (comboBox1.SelectedItem != null)
            {
                if (cf.ShowDialog() == DialogResult.OK)
                {
                    port = int.Parse(cf.textBox2.Text);
                    ip = cf.textBox1.Text;
                    switch (comboBox1.SelectedItem)
                    {
                        case "TicTocToe":
                            TicTocToe_server server = new TicTocToe_server(ip, port);
                            Form3 client = new Form3(port,ip,pictureBox1.Image,label1.Text,3);
                            client.Show();
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("pick a game", "Pick a game", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
