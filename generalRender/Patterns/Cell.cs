using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace generalRender
{
    class Cell
    {
        #region Variables
        public int Width { get; private set; }
        public int Height { get; private set; }
        public eBounds Bounds { get; private set; }
        #endregion

        #region Events
        public delegate void onChangeBounds(eBounds bounds);
        public event onChangeBounds changeBounds;
        #endregion

        #region Constructors
        public Cell(int _width, int _height, eBounds _bounds)
        {
            Width = _width;
            Height = _height;
            setBounds(_bounds);
        }
        public Cell(int _width, int _height)
            : this(_width, _height, eBounds.None)
        { }
        public Cell(eBounds _bounds)
            : this(0, 0, _bounds)
        { }
        public Cell()
            : this(0, 0, eBounds.None)
        { }

        #endregion

        #region Bounds
        public void setRight()
        {
            if ((Bounds & eBounds.Right) == eBounds.Right)
                setBounds(Bounds ^ eBounds.Right);
            else
                setBounds(Bounds | eBounds.Right);
        }
        public void setLeft()
        {
            if ((Bounds & eBounds.Left) == eBounds.Left)
                setBounds(Bounds ^ eBounds.Left);
            else
                setBounds(Bounds | eBounds.Left);
        }
        public void setUp()
        {
            if ((Bounds & eBounds.Up) == eBounds.Up)
                setBounds(Bounds ^ eBounds.Up);
            else
                setBounds(Bounds | eBounds.Up);
        }
        public void setDown()
        {
            if ((Bounds & eBounds.Down) == eBounds.Down)
                setBounds(Bounds ^ eBounds.Down);
            else
                setBounds(Bounds | eBounds.Down);
        }
        public void clear() { setBounds(0); }
        private void setBounds(eBounds _Bounds)
        {
            Bounds = _Bounds;
            if (changeBounds != null) changeBounds(Bounds);
        }
        #endregion

        #region Coordinates

        public Point topLeft { get { return new Point(0, 0); } }
        public Point topRight { get { return new Point(Width - 1, 0); } }
        public Point bottomLeft { get { return new Point(0, Height - 1); } }
        public Point bottomRight { get { return new Point(Width - 1, Height - 1); } }

        #endregion
    }


    partial class graphicElement : Cell
    {
        private Pen cPen;
        private frame box;
        private bool _autoDraw;
        
        public Color color { get; private set; }

        public bool autoDraw
        {
            get { return _autoDraw; }
            set
            {
                if(value == true & _autoDraw != true)
                {
                    _autoDraw = true;
                    changeBounds += changeBoundsHandler;                    
                }
                else if(value == false & _autoDraw != false)
                {
                    _autoDraw = false;
                    changeBounds -= changeBoundsHandler;
                }
            }
        }
        public Bitmap picture
        {
            get
            {
                return box.bmap;
            }
        }
    }

    partial class graphicElement : Cell
    {
        public graphicElement(int _width, int _height, Color _color, eBounds _bounds)
            :base(_width, _height, _bounds)
        {
            box = new frame(Width, Height);
            setColor(_color);
            _autoDraw = false;
        }
        public graphicElement(int _width, int _height, eBounds _bounds)
            :this(_width, _height, Color.Red, _bounds)
        { }
        public graphicElement(int _width, int _height, Color _color)
            : this(_width, _height, _color, eBounds.None)
        { }
        public graphicElement(int _width, int _height)
            : this(_width, _height, Color.Red, eBounds.None)
        { }
    }

    partial class graphicElement : Cell
    {
        public void setColor(Color _color)
        {
            color = _color;
            cPen = new Pen(color);
        }
        private void changeBoundsHandler(eBounds _bounds)
        {
            draw();
        }

        #region draw methods
        private void drawBoundDown()
        { box.graphic.DrawLine(cPen, bottomLeft, bottomRight); }
        private void drawBoundLeft()
        { box.graphic.DrawLine(cPen, topLeft, bottomLeft); }
        private void drawBoundUp()
        { box.graphic.DrawLine(cPen, topLeft, topRight); }
        private void drawBoundRight()
        { box.graphic.DrawLine(cPen, topRight, bottomRight); }
        public void draw()
        {
            if (Bounds == 0) return;

            box.reset();
            if ((Bounds & eBounds.Down) == eBounds.Down) drawBoundDown();
            if ((Bounds & eBounds.Up) == eBounds.Up) drawBoundUp();
            if ((Bounds & eBounds.Left) == eBounds.Left) drawBoundLeft();
            if ((Bounds & eBounds.Right) == eBounds.Right) drawBoundRight();
        }

        #endregion
    }

}


