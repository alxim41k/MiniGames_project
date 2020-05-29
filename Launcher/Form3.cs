using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Network;
using Network.Enums;
using Network.Converter;
using Network.Extensions;
using Network.Packets;
using System.IO;

namespace Launcher
{
    public partial class Form3 : Form
    {
        public bool game;
        public int rounds;
        public bool Turn;
        public bool Ready;
        private TcpConnection connection;
        public Image localImage;
        public Image enemyImage;
        public string localNickName;
        public string EnemyNickName;
        public Form3(int port, string ip, Image localpic, string localnick, int rounds)
        {
            game = true;
            this.rounds = rounds;
            localNickName = localnick;
            localImage = localpic;
            ConnectionResult connectionResult = ConnectionResult.TCPConnectionNotAlive;
            connection = ConnectionFactory.CreateTcpConnection(ip, port, out connectionResult);
            if (connectionResult == ConnectionResult.Connected)
            {
                MessageBox.Show("Connected", "connected");
            }
            InitializeComponent();
            pictureBox2.Image = localImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            label1.Text = localnick;
            connection.Send(new ProfileDataUpload(ImageConvert.image2Bytes(localImage), localNickName));
            connection.RegisterRawDataHandler("con",GetProfile);
            connection.RegisterPacketHandler<ProfileDataGetResponse>(ProfileReceived, this);
        }
        private void GetProfile(RawData data, Connection con)
        {
            connection.Send(new ProfileDataGetRequest());
        }
        private void ProfileReceived(ProfileDataGetResponse resp, Connection con)
        {
            MessageBox.Show("Enemy connected", "Data");
            enemyImage = ImageConvert.bytes2Image(resp.pic);
            EnemyNickName = resp.nick;
            pictureBox1.Image = enemyImage;
            label2.Text = EnemyNickName;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)  
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }


        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
