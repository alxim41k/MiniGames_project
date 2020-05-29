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

namespace Launcher
{
    public partial class TicTocToe_server : Form
    {
        private ServerConnectionContainer serverConnection;
        private const bool Debug = true;
        Player p1;
        Player p2;
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
            richTextBox1.Text += $"Connected{con.IPRemoteEndPoint}" + Environment.NewLine;
            con.RegisterPacketHandler<ProfileDataUpload>(ProfileDataReceived, this);
            con.RegisterPacketHandler<ProfileDataGetRequest>(ProfileDataUploaded, this);
        }
        private void connectionLost(Connection con, ConnectionType conType,Network.Enums.CloseReason reason)
        {
            MessageBox.Show($"Connection lost{con.IPLocalEndPoint}", "lost");
        }
        private void ProfileDataReceived(ProfileDataUpload req, Connection con)
        {
            MessageBox.Show("Data recevied","data");
            if (p1 == null)
            {
                p1 = new Player(req.nick, ImageConvert.bytes2Image(req.pic), con);
            }
            else
            {
                MessageBox.Show("All players connected", "data");
                p2 = new Player(req.nick, ImageConvert.bytes2Image(req.pic), con);
                p1.con.SendRawData("con", Encoding.UTF8.GetBytes("con"));
                p2.con.SendRawData("con", Encoding.UTF8.GetBytes("con"));
            }
        }
        private void ProfileDataUploaded(ProfileDataGetRequest req, Connection con)
        {
            
            if (con == p1.con)
            {
                MessageBox.Show("Data send p1", "data");
                con.Send(new ProfileDataGetResponse(ImageConvert.image2Bytes(p2.pic), p2.nick, req));
            }
            else
            {
                MessageBox.Show("Data send p2", "data");
                con.Send(new ProfileDataGetResponse(ImageConvert.image2Bytes(p1.pic), p1.nick, req));
            }
        }
    }
}
