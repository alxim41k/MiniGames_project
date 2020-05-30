using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Network;
using Network.Packets;
using Network.Attributes;

namespace Launcher
{
    public class Player
    {
        public string nick;
        public Connection con;
        public Image pic;
        public bool ready;
        public bool turn;
        public bool FirstTurn;
        public Player(Connection con)
        {
            this.con = con;
        }
            
    }
    public static class ImageConvert
    {
        public static Image bytes2Image(byte[] data)
        {
            ImageConverter imageConverter = new ImageConverter();
            return (Image)imageConverter.ConvertFrom(data);
        }
        public static byte[] image2Bytes(Image image)
        {
            ImageConverter imageConverter = new ImageConverter();
            return (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
        }
    }
   
}
