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
        public Player(string nick, Image pic, Connection con)
        {
            this.nick = nick;
            this.pic = pic;
            this.con = con;
            ready = false;
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
    public class ProfileDataUpload : RequestPacket
    {
        public ProfileDataUpload(byte[] pic, string nick)
        {
            this.pic = pic;
            this.nick = nick;
        }

        public byte[] pic { get; set; }

        public string nick{ get; set; }

    }
    [PacketRequest(typeof(ProfileDataUpload))]
    public class ProfileDataResponse : ResponsePacket
    {
        public ProfileDataResponse(RequestPacket request)
            : base(request)
        {

        }
    }
    public class ProfileDataGetRequest : RequestPacket
    {
        public ProfileDataGetRequest()
        {
        }
    }
    [PacketRequest(typeof(ProfileDataGetRequest))]
    public class ProfileDataGetResponse : ResponsePacket
    {
        public ProfileDataGetResponse(byte[] pic, string nick,RequestPacket request)
            : base(request)
        {
            this.pic = pic;
            this.nick = nick;
        }
        public byte[] pic { get; set; }
        public string nick { get; set; }
    }
}
