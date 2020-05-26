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
            string foldername = "minigamesData";
            string path = Path.Combine(Environment.SpecialFolder.ApplicationData.ToString(), foldername,"playerData.json");
            
            if (File.Exists(path))
            {
                player = JsonConvert.DeserializeObject<Player>(File.ReadAllText(path));
            }
            else
            {

                player = new Player();
                string playerData = JsonConvert.SerializeObject(player);
                File.Create(path);
                File.WriteAllText(path, playerData);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Click(object sender, EventArgs e)
        {
            if (button1.Visible == false && button2.Visible == false)
            {
                button1.Visible = true;
                button2.Visible = true;
            }
            else
            {
                button1.Visible = false;
                button2.Visible = false;
            }
        }
        private void panel6_Click(object sender, EventArgs e)
        {
            if (button3.Visible == false && button4.Visible == false)
            {
                button3.Visible = true;
                button4.Visible = true;
            }
            else
            {
                button3.Visible = false;
                button4.Visible = false;
            }
        }
        private void panel7_Click(object sender, EventArgs e)
        {
            if (button5.Visible == false && button6.Visible == false)
            {
                button5.Visible = true;
                button6.Visible = true;
            }
            else
            {
                button5.Visible = false;
                button6.Visible = false;
            }
        }
        private void panel8_Click(object sender, EventArgs e)
        {
            if (button7.Visible == false && button8.Visible == false)
            {
                button7.Visible = true;
                button8.Visible = true;
            }
            else
            {
                button7.Visible = false;
                button8.Visible = false;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
