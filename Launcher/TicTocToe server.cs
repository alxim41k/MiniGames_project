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
using Network;
using Network.Enums;
using Network.Extensions;
using Network.Packets;
using Network.Converter;

namespace Launcher
{
    public partial class TicTocToe_server : Form
    {
        private ServerConnectionContainer serverConnection;
        private const bool Debug = true;
        Player p1;
        Player p2;
        Random r = new Random();
        public TicTocToe_server(string ip, int port)
        {
            if (Debug)
            {
                this.Visible = true;
            }
            else
            {
                this.Visible = false;
            }
            InitializeComponent();
            serverConnection = ConnectionFactory.CreateServerConnectionContainer(ip, port, false);
            serverConnection.AllowBluetoothConnections = false;
            serverConnection.AllowUDPConnections = false;
            serverConnection.ConnectionEstablished += connectionEstablished;
            serverConnection.ConnectionLost += connectionLost;
            serverConnection.Start();
            
        }
        private void connectionEstablished(Connection con, ConnectionType conType)
        {
            int turn = r.Next(2);
            richTextBox1.Text += $"Connected{con.IPRemoteEndPoint}" + Environment.NewLine;
            if (p1 == null)
            {
                p1 = new Player(con);
                p1.ready = false;
            }
            else
            {
                p2 = new Player(con);
                p2.ready = false;
            }
            con.RegisterRawDataHandler("Image", (RawData, connection) =>
             {
                 if (connection == p1.con)
                 {
                     p1.pic = ImageConvert.bytes2Image(RawData.Data);
                 }
                 else
                 {
                     p2.pic = ImageConvert.bytes2Image(RawData.Data);
                     p1.con.SendRawData("image", ImageConvert.image2Bytes(p2.pic));
                     p2.con.SendRawData("image", ImageConvert.image2Bytes(p1.pic));
                     if (turn == 1)
                     {
                         p1.FirstTurn = true;
                         p1.con.SendRawData("firstTurn", RawDataConverter.GetBytes(true));
                         p1.turn = true;
                     }
                     else
                     {
                         p2.FirstTurn = true;
                         p2.con.SendRawData("firstTurn", RawDataConverter.GetBytes(true));
                         p2.turn = false;
                     }
                 }
             });
            con.RegisterRawDataHandler("Nick", (RawData, connection) =>
            {
                if (connection == p1.con)
                {
                    p1.nick = RawData.ToUTF8String(); 
                }
                else
                {
                    p2.nick = RawData.ToUTF8String();
                    p1.con.SendRawData("nick", Encoding.UTF8.GetBytes(p2.nick));
                    p2.con.SendRawData("nick", Encoding.UTF8.GetBytes(p1.nick));
                }
            });
            con.RegisterRawDataHandler("Ready", (RawData, connection) =>
            {
                if (connection == p1.con)
                {
                    p1.ready = RawData.ToBoolean();
                    if (p2 != null)
                    {
                        p1.con.SendRawData("ready", RawDataConverter.GetBytes(p2.ready));
                    }
                    p2.con.SendRawData("ready", RawDataConverter.GetBytes(p1.ready));
                }
                else
                {
                    p2.ready = RawData.ToBoolean();
                    p1.con.SendRawData("ready", RawDataConverter.GetBytes(p2.ready));
                    p2.con.SendRawData("ready", RawDataConverter.GetBytes(p1.ready));
                }
            });
            con.RegisterRawDataHandler("mapUpdate", (RawData,connection) =>
            {
                char[] newmap = RawData.ToUTF8String().ToArray();
                for(int i = 0; i < 9; i++)
                {
                    map[i] = int.Parse(newmap[i].ToString());
                }
                string mapString = "";
                for (int i = 0; i < 9; i++)
                {
                    mapString += map[i].ToString();
                }
                if (connection == p1.con)
                {
                    p1.turn = false;
                    p2.turn = true;
                    p1.con.SendRawData("Turn", RawDataConverter.GetBytes(p1.turn));
                    p2.con.SendRawData("Turn", RawDataConverter.GetBytes(p2.turn));
                    p2.con.SendRawData("MapUpdate", Encoding.UTF8.GetBytes(mapString));
                }
                else
                {
                    p2.turn = false;
                    p1.turn = true;
                    p1.con.SendRawData("Turn", RawDataConverter.GetBytes(p1.turn));
                    p2.con.SendRawData("Turn", RawDataConverter.GetBytes(p2.turn));
                    p1.con.SendRawData("MapUpdate", Encoding.UTF8.GetBytes(mapString));
                }
                if (checkGameWin() == 1 && p1.FirstTurn)
                {
                    p1.con.SendRawData("win",RawDataConverter.GetBytes(true));
                    p2.con.SendRawData("lose", RawDataConverter.GetBytes(true));
                    map = new int[]
            { 0,0,0,0,0,0,0,0,0};
                }
                else if (checkGameWin() == 2 && p2.FirstTurn)
                {
                    p1.con.SendRawData("win", RawDataConverter.GetBytes(true));
                    p2.con.SendRawData("lose", RawDataConverter.GetBytes(true));
                    map = new int[]
            { 0,0,0,0,0,0,0,0,0};
                }
                else if (checkGameWin() == 1 && p2.FirstTurn)
                {
                    p2.con.SendRawData("win", RawDataConverter.GetBytes(true));
                    p1.con.SendRawData("lose", RawDataConverter.GetBytes(true));
                    map = new int[]
            { 0,0,0,0,0,0,0,0,0};
                }
                else if (checkGameWin() == 2 && p1.FirstTurn)
                {
                    p2.con.SendRawData("win", RawDataConverter.GetBytes(true));
                    p1.con.SendRawData("lose", RawDataConverter.GetBytes(true));
                    map = new int[]
            { 0,0,0,0,0,0,0,0,0};
                }
            }
            );
        }
        private int checkGameWin()
        {
            if(map[0] == 1 && map[1] == 1 && map[2] == 1)
            {
                return 1;
            }
            else if (map[3] == 1 && map[4] == 1 && map[5] == 1)
            {
                return 1;
            }
            else if (map[6] == 1 && map[7] == 1 && map[8] == 1)
            {
                return 1;
            }
            else if (map[0] == 1 && map[3] == 1 && map[6] == 1)
            {
                return 1;
            }
            else if (map[1] == 1 && map[4] == 1 && map[7] == 1)
            {
                return 1;
            }
            else if (map[2] == 1 && map[5] == 1 && map[8] == 1)
            {
                return 1;
            }
            else if (map[0] == 1 && map[4] == 1 && map[8] == 1)
            {
                return 1;
            }
            else if (map[2] == 1 && map[4] == 1 && map[6] == 1)
            {
                return 1;//
            }
            if (map[0] == 2 && map[1] == 2 && map[2] == 2)
            {
                return 2;
            }
            else if (map[3] == 2 && map[4] == 2 && map[5] == 2)
            {
                return 2;
            }
            else if (map[6] == 2 && map[7] == 2 && map[8] == 2)
            {
                return 2;
            }
            else if (map[0] == 2 && map[3] == 2 && map[6] == 2)
            {
                return 2;
            }
            else if (map[1] == 2 && map[4] == 2 && map[7] == 2)
            {
                return 2;
            }
            else if (map[2] == 2 && map[5] == 2 && map[8] == 2)
            {
                return 2;
            }
            else if (map[0] == 2 && map[4] == 2 && map[8] == 2)
            {
                return 2;
            }
            else if (map[2] == 2 && map[4] == 2 && map[6] == 2)
            {
                return 2;
            }
            else
            {
                return 0;
            }

        }
        private void connectionLost(Connection con, ConnectionType conType,Network.Enums.CloseReason reason)
        {
            MessageBox.Show($"Connection lost{con.IPLocalEndPoint}, {reason}", "lost");
        }

        public int[] map = new int[] 
            { 0,0,0,0,0,0,0,0,0};
    }
}
