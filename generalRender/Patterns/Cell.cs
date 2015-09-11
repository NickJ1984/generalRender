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
        public Point Coodinates { get; private set; }
        #endregion

        #region Events
        public delegate void onChangeBounds(eBounds bounds);
        public event onChangeBounds changeBounds;
        #endregion

        #region Constructors
        public Cell(int _width, int _height, Point coords, eBounds _bounds)
        {
            Width = _width;
            Height = _height;
            Coodinates = coords;
            Bounds = _bounds;
        }
        public Cell(int _width, int _height, eBounds _bounds)
            :this(_width, _height, new Point(-1,-1), _bounds)
        { }
        public Cell(int _width, int _height, Point coords)
            : this(_width, _height, coords, eBounds.None)
        { }
        public Cell(int _width, int _height)
            : this(_width, _height, new Point(-1, -1), eBounds.None)
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
    }


}
