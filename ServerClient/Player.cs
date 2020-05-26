using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Classes
{
    public class Item
    {
        public string game;
        public string name;
        public Bitmap pic;
        public int cost;
        public Item(string game, string name, Bitmap pic, int cost)
        {
            this.game = game;
            this.name = name;
            this.pic = pic;
            this.cost = cost;
        }
    }
    static class XP
    {
        public static int[] XpSteps = new int[] {0,100,200,400,800,1600,3200,6400,12800,25600,51200,102400};
    }
    public class Player
    {
        public Boolean IsLocal;
        public String Name;
        public Bitmap ProfileImage;
        public string PicPath;
        public int lvl;
        public int xp;
        public Player(Boolean IsLocal,string name, int lvl, int xp,string PicPath)
        {
            this.IsLocal = IsLocal;
            this.Name = name;
            this.lvl = lvl;
            this.xp = xp;
            this.PicPath = PicPath;
        }
        public Player(Boolean IsLocal, string name, int lvl, int xp, Bitmap ProfileImage)
        {
            this.IsLocal = IsLocal;
            this.Name = name;
            this.lvl = lvl;
            this.xp = xp;
            this.ProfileImage = ProfileImage;
        }
        public int xp2percent()
        {
            int[] xpsteps = XP.XpSteps;
            int xpstep = xpsteps[lvl + 1];
            int delta = xpstep - xp;
            if (lvl != 0)
            {
                int percent = (int)Math.Pow(2, lvl - 1);
                return delta / percent;
            }
            else
            {
                int percent = (int)Math.Pow(2, lvl);
                return delta / percent;
            }
        }

    }
}
