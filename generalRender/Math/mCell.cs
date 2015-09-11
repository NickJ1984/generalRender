using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace generalRender
{
    [Flags]
    public enum eBounds
    {
        None = 0,
        Left = 1,
        Right = 2,
        Up = 4,
        Down = 8
    }
    partial class mCell
    {
        #region properties
        public int column { get; private set; }
        public int line { get; private set; }
        public CellState cState { get; private set; }
        #endregion
        #region classes
        public cellBounds bounds;
        public cellCoord coordinates;
        #endregion
        #region enums
        [Flags]
        public enum CellState
        {
            None = 0,
            Bound = 1,
            Start = 2,
            Finish = 4
        }
        
        #endregion

    }

    #region Constructors
    partial class mCell
    {
        public mCell(int _column, int _line, int _X, int _Y, int _size, eBounds _bounds, CellState state)
        {
            column = _column;
            line = _line;
            bounds = new cellBounds(_bounds);
            coordinates = new cellCoord(_X, _Y, _size);
            cState = state;
        }
        public mCell(int _column, int _line, Point Coords, int _size, eBounds _bounds, CellState state)
            : this(_column, _line, Coords.X, Coords.Y, _size, _bounds, state)
        { }
        public mCell(int _column, int _line, Point Coords, int _size, eBounds _bounds)
            : this(_column, _line, Coords.X, Coords.Y, _size, _bounds, 0)
        { }
        public mCell(int _column, int _line, Point Coords, int _size)
            : this(_column, _line, Coords.X, Coords.Y, _size, 0, 0)
        { }
        public mCell(int _column, int _line, int _X, int _Y, int _size)
            : this(_column, _line, _X, _Y, _size, 0, 0)
        { }
        
    }
    #endregion

    #region mCell inner classes
    partial class mCell
    {
        public class cellBounds
        {
            #region Variables
            public eBounds bounds { get; private set; }
            #endregion

            #region Constructors
            public cellBounds(eBounds BR)
            { bounds = BR; }
            public cellBounds() 
                : this(0)
            {}
            public cellBounds(cellBounds _cellBounds) 
                : this(_cellBounds.bounds)
            { }
            #endregion

            #region Set bounds
            public void setUp() { bounds = ((bounds & eBounds.Up) == eBounds.Up) ? bounds ^ eBounds.Up : bounds | eBounds.Up; }
            public void setDown() { bounds = ((bounds & eBounds.Down) == eBounds.Down) ? bounds ^ eBounds.Down : bounds | eBounds.Down; }
            public void setLeft() { bounds = ((bounds & eBounds.Left) == eBounds.Left) ? bounds ^ eBounds.Left : bounds | eBounds.Left; }
            public void setRight() { bounds = ((bounds & eBounds.Right) == eBounds.Right) ? bounds ^ eBounds.Right : bounds | eBounds.Right; }
            public void clear() { bounds = 0; }
            public void setBounds(eBounds _bounds) { bounds = _bounds; }
            #endregion

            #region Neighboors comparison
            public eBounds cmpUp(cellBounds _cellBounds)
            {
                if ((_cellBounds.bounds & eBounds.Down) == eBounds.Down) return eBounds.Up;
                return 0;
            }
            public eBounds cmpDown(cellBounds _cellBounds)
            {
                if ((_cellBounds.bounds & eBounds.Up) == eBounds.Up) return eBounds.Down;
                return 0;
            }
            public eBounds cmpLeft(cellBounds _cellBounds)
            {
                if ((_cellBounds.bounds & eBounds.Right) == eBounds.Right) return eBounds.Left;
                return 0;
            }
            public eBounds cmpRight(cellBounds _cellBounds)
            {
                if ((_cellBounds.bounds & eBounds.Left) == eBounds.Left) return eBounds.Right;
                return 0;
            }
            #endregion

            #region Inversion
            private eBounds invVertical(eBounds _bounds)
            {
                eBounds result = _bounds;
                if ( ((_bounds & eBounds.Up) == eBounds.Up) && ((_bounds & eBounds.Down) == eBounds.Down) ) return _bounds;
                if (((_bounds & eBounds.Up) != eBounds.Up) && ((_bounds & eBounds.Down) != eBounds.Down)) return _bounds;

                result = result ^ eBounds.Up ^ eBounds.Down;
                return result;
            }

            private eBounds invHorizontal(eBounds _bounds)
            {
                eBounds result = _bounds;
                if (((_bounds & eBounds.Right) == eBounds.Right) && ((_bounds & eBounds.Left) == eBounds.Left)) return _bounds;
                if (((_bounds & eBounds.Right) != eBounds.Right) && ((_bounds & eBounds.Left) != eBounds.Left)) return _bounds;

                result = result ^ eBounds.Right ^ eBounds.Left;
                return result;
            }
            #endregion

            #region Checks
            public bool isBound { get { return !(bounds == 0); } }
            #endregion
        }

        public class cellCoord
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public Point coordinates { get; private set; }
            public int size { get; private set; }

            public cellCoord(int _X, int _Y, int _size)
            {
                setCoords(_X, _Y); size = _size;
            }
            public cellCoord(Point coords, int _size)
                : this(coords.X, coords.Y, _size)
            { }
            public cellCoord(cellCoord _cell)
                :this(_cell.X, _cell.Y, _cell.size)
            { }
            public cellCoord(int _X, int _Y)
                :this(_X,_Y,0)
            { }
            public cellCoord()
                :this(-1,-1,0)
            { }

            public Point getTopLeftDot() { return coordinates; }
            public Point getTopRightDot() { return new Point(X + size, Y); }
            public Point getBottomLeftDot() { return new Point(X, Y + size); }
            public Point getBottomRightDot() { return new Point(X + size, Y + size); }
            private void setCoords(int _X = -1, int _Y = -1)
            {
                if (_X == -1) _X = X;
                if (_Y == -1) _Y = Y;

                X = _X;
                Y = _Y;
                coordinates = new Point(X, Y);
            }

        }
    }
    #endregion
}
