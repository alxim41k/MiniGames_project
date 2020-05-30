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
        public bool win;
        public bool lose;
        public bool firstTurn;
        public int score;
        public int enemyScore;
        public bool game;
        public int rounds;
        public bool Turn;
        public bool Ready;
        public bool enemyReady;
        private TcpConnection connection;
        public Image localImage;
        public Image enemyImage;
        public string localNickName;
        public string EnemyNickName;
        public Form3(int port, string ip, Image localpic, string localnick, int rounds)
        {
            win = false;
            lose = false;
            Turn = false;
            firstTurn = false;
            score = 0;
            enemyScore = 0;
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
            connection.SendRawData("Image", ImageConvert.image2Bytes(localImage));
            connection.SendRawData("Nick", Encoding.UTF8.GetBytes(localNickName));
            connection.RegisterRawDataHandler("image", (RawData, con) =>
                 {
                     enemyImage = ImageConvert.bytes2Image(RawData.Data);
                     pictureBox1.Image = enemyImage;
                 });
            connection.RegisterRawDataHandler("nick", (RawData, con) =>
                {
                    EnemyNickName = Encoding.UTF8.GetString(RawData.Data);
                    label2.Text = EnemyNickName;
                });
            connection.RegisterRawDataHandler("ready", (RawData, con) =>
            {
                enemyReady = RawData.ToBoolean();
                label2.ForeColor = Color.Green;
            });
            connection.RegisterRawDataHandler("Turn", (RawData, con) =>
             {
                 Turn = RawData.ToBoolean();
             });
            connection.RegisterRawDataHandler("firstTurn", (Rawdata, con) =>
            {
                firstTurn = Rawdata.ToBoolean();
                Turn = true;
            });
            connection.RegisterRawDataHandler("MapUpdate" , (RawData,con) =>
            {
                char[] newmap = RawData.ToUTF8String().ToArray();
                for (int i = 0; i < 9; i++)
                {
                    map[i] = int.Parse(newmap[i].ToString());
                }
                for (int i = 0; i < 9; i++)
                {
                    if (map[i] == 1)
                    {
                        panel1.Controls[i].BackgroundImage = Properties.Resources.Cross;

                    }
                    else if (map[i] == 2)
                    {
                        panel1.Controls[i].BackgroundImage = Properties.Resources.Circle;
                    }
                }
            });
            connection.RegisterRawDataHandler("win", (RawData, Con) =>
            {
                score += 1;
                label3.Text = score.ToString();
                ClearMap();
            });
            connection.RegisterRawDataHandler("lose", (RawData, Con) =>
            {
                enemyScore += 1;
                label4.Text = enemyScore.ToString();
                ClearMap();

            });
            label1.ForeColor = Color.Red;
            label2.ForeColor = Color.Red;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            if (!(Ready && enemyReady))
            {
                if (!Ready)
                {
                    label1.ForeColor = Color.Green;
                    connection.SendRawData("Ready", RawDataConverter.GetBytes(true));
                    Ready = true;
                }
                else
                {
                    label1.ForeColor = Color.Red;
                    connection.SendRawData("Ready", RawDataConverter.GetBytes(false));
                    Ready = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (firstTurn)
            {
                map[0] = 1;
            }
            else
            {
                map[0] = 2;
            }
            UpdateMap();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (firstTurn)
            {
                map[3] = 1;
            }
            else
            {
                map[3] = 2;
            }
            UpdateMap();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (firstTurn)
            {
                map[4] = 1;
            }
            else
            {
                map[4] = 2;
            }
            UpdateMap();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (firstTurn)
            {
                map[1] = 1;
            }
            else
            {
                map[1] = 2;
            }
            UpdateMap();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (firstTurn)
            {
                map[5] = 1;
            }
            else
            {
                map[5] = 2;
            }
            UpdateMap();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (firstTurn)
            {
                map[6] = 1;
            }
            else
            {
                map[6] = 2;
            }
            UpdateMap();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (firstTurn)
            {
                map[2] = 1;
            }
            else
            {
                map[2] = 2;
            }
            UpdateMap();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (firstTurn)
            {
                map[7] = 1;
            }
            else
            {
                map[7] = 2;
            }
            UpdateMap();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (firstTurn)
            {
                map[8] = 1;
            }
            else
            {
                map[8] = 2;
            }
            UpdateMap();
        }
        private void UpdateMap()
        {
            
            for(int i = 0; i < 9; i ++)
            {
                if (map[i] == 1)
                {
                    panel1.Controls[i].BackgroundImage = Properties.Resources.Cross;
                   
                }
                else if (map[i] == 2)
                {
                    panel1.Controls[i].BackgroundImage = Properties.Resources.Circle;
                }
            }
            string mapString = "";
            for(int i = 0; i < 9;i++)
            {
                mapString += map[i].ToString();
            }
            connection.SendRawData("mapUpdate",Encoding.UTF8.GetBytes(mapString));
            Turn = false;
        }
        private void ClearMap()
        {
            map = new int[]
            { 0,0,0,0,0,0,0,0,0};
            for(int i = 0; i < 9; i++)
            {
                panel1.Controls[i].BackgroundImage = null;
            }
        }
        public int[] map = new int[]
            { 0,0,0,0,0,0,0,0,0};
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (Ready && enemyReady)
            {
                if(Turn)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (map[i] == 1)
                        {
                            panel1.Controls[i].Enabled = false;
                        }
                        else if (map[i] == 2)
                        {
                            panel1.Controls[i].Enabled = false;
                        }
                        else
                        {
                            panel1.Controls[i].Enabled = true;
                        }
                    }
                }
                else
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    button7.Enabled = false;
                    button8.Enabled = false;
                    button9.Enabled = false;
                }
            }
        }
    }
}
