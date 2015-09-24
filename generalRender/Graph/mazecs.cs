using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace generalRender.Graph
{
    partial class maze //Variables
    {
        private maze_processor mp;
        private graphicElement[] cells;
        private Point[] coords;
        private frame plane;
        private frame outline;
        public Color borderColor { get; private set; }
        public Point startPoint { get; private set; }
        public readonly Point screenResolution;
    }

    partial class maze //Constructors
    {
        public maze(Point WindowResolution, int cellSize, Color color)
        {
            mp = new maze_processor(WindowResolution, cellSize);
            startPoint = getStartPoint(WindowResolution);
            screenResolution = WindowResolution;
            borderColor = color;
            plane = new frame(mp.resolution.X, mp.resolution.Y);
            initOutline();
            initCells();
        }
    }

    partial class maze //service methods
    {
        private Point getStartPoint(Point windowRes)
        {
            int diffX = windowRes.X - mp.resolution.X;
            int diffY = windowRes.Y - mp.resolution.Y;

            if (diffX != 0) diffX /= 2;
            if (diffY != 0) diffY /= 2;
            return new Point(diffX, diffY);
        }
        private void initOutline()
        {
            Pen cPen = new Pen(borderColor);
            int maxX = mp.resolution.X + 2;
            int maxY = mp.resolution.Y + 2;
            outline = new frame(maxX, maxY);
            outline.graphic.DrawLine(cPen, new Point(0, 0), new Point(maxX - 1, 0));
            outline.graphic.DrawLine(cPen, new Point(maxX - 1, 0), new Point(maxX - 1, maxY - 1));
            outline.graphic.DrawLine(cPen, new Point(maxX - 1, maxY - 1), new Point(0, maxY - 1));
            outline.graphic.DrawLine(cPen, new Point(0, 0), new Point(0, maxY - 1));
        }

        private void initCells()
        {
            cells = new graphicElement[mp.cellsCount];
            coords = new Point[mp.cellsCount];

            for(int i = 0; i < mp.cellsCount; i++)
            {
                cells[i] = new graphicElement(mp.cellSize, mp.cellSize, borderColor, eBounds.None);
                coords[i] = mp.getCellCoordinates(i);
            }
        }
    }
}
