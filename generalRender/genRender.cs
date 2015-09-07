using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace generalRender
{
    [Flags]
    public enum e_direction
    {
        left = 1,
        right = 2,
        up = 4,
        down = 8
    };
    partial class mazeElement
    {
        public Bitmap picture
        {
            get
            {
                return box.bmap;
            }
        }
        public int width { get; private set; }
        public int height { get; private set; }
        public Color color { get; private set; }
        public e_direction direction { get; private set; }
        private Pen cPen;
        private frame box;
        
        public mazeElement(int _width, int _height, Color _color, e_direction _direction)
        {
            width = _width;
            height = _height;
            setColor(_color);
            setDirection(_direction);
        }

        public void setColor(Color _color)
        {
            cPen = new Pen(_color);
            color = _color;
        }

        public void setDirection(e_direction _direction)
        {
            if (direction == _direction) return;
            direction = _direction;
            box.reset();
            Pen cPen = new Pen(color);

            if ((direction & e_direction.down) == e_direction.down) setBound_down();
            if ((direction & e_direction.up) == e_direction.up) setBound_up();
            if ((direction & e_direction.left) == e_direction.left) setBound_left();
            if ((direction & e_direction.right) == e_direction.right) setBound_right();
        }

        

    }

    partial class mazeElement
    {
        private void setBound_down()
        {
            Point p1 = new Point(0, height - 1);
            Point p2 = new Point(width - 1, height - 1);
            box.graphic.DrawLine(cPen, p1, p2);
        }
        private void setBound_up()
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(width - 1, 0);
            box.graphic.DrawLine(cPen, p1, p2);
        }
        private void setBound_left()
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, height - 1);
            box.graphic.DrawLine(cPen, p1, p2);
        }
        private void setBound_right()
        {
            Point p1 = new Point(width - 1, 0);
            Point p2 = new Point(width - 1, height - 1);
            box.graphic.DrawLine(cPen, p1, p2);
        }
    }

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
