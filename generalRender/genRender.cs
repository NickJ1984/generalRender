using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace generalRender
{
    class frame
    {
        public Graphics graphic { get; private set; }
        public Bitmap bmap { get; private set; }

        public frame(int width, int height)
        {
            bmap = new Bitmap(width, height);
            graphic = Graphics.FromImage(bmap);
        }

        public void reset(int width, int height)
        {
            bmap = new Bitmap(width, height);
            graphic = Graphics.FromImage(bmap);
        }

        public void reset()
        {
            reset(bmap.Width, bmap.Height);
        }

    }
    class genRender
    {
    }
}
