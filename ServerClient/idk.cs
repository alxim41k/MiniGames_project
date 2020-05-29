using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace Classes
{
    static class ImageConvert
    {
        public static Image bytes2Image(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            return Image.FromStream(ms);
        }
        public static byte[] image2Bytes(Image image)
        {
            ImageConverter imageConverter = new ImageConverter();
            return (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
        }
    }
}
