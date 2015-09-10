using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace generalRender
{
    partial class mazeGraphicArray
    {
        private mazeElement[,] elements;
        private Point[,] coords;
        private frame map;

        public mazeGraphicArray(int row_number, int column_number, int cell_width, int cell_height, Color bound_color, e_direction bounds)
        {
            initMazeElements(row_number, column_number, cell_width, cell_height, bound_color, bounds);
            map = new frame(Width, Height);
        }

        public mazeGraphicArray(int row_number, int column_number, int cell_width, int cell_height, Color bound_color)
            :this(row_number, column_number, cell_width, cell_height, bound_color, e_direction.none)
        { }

        public mazeGraphicArray(int row_number, int column_number, int cell_width, int cell_height)
            : this(row_number, column_number, cell_width, cell_height, Color.Black, e_direction.none)
        { }

        private void initMazeElements(int row, int column, int width, int height, Color color, e_direction bounds)
        {
            elements = new mazeElement[row, column];
            coords = new Point[row, column];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    elements[i, j] = new mazeElement(width, height, color, bounds);
                    coords[i, j] = new Point(j * width, i * height);
                }
            }
        }

        private void initMap()
        {
            map.reset();
            
            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    map.graphic.DrawImage(elements[i, j].picture, coords[i, j]);
                }
            }
        }

        /*private void drawSquare()
        {
            map.graphic.DrawLine(new Pen(elements[0, 0].color), new Point(0, 0), new Point(Width - 1, 0));
            map.graphic.DrawLine(new Pen(elements[0, 0].color), new Point(0, Height - 1), new Point(Width - 1, Height - 1));
            map.graphic.DrawLine(new Pen(elements[0, 0].color), new Point(0, 0), new Point(0, Height - 1));
            map.graphic.DrawLine(new Pen(elements[0, 0].color), new Point(Width - 1, 0), new Point(Width - 1, Height - 1));
        }*/
    }

    partial class mazeGraphicArray
    {
        public Bitmap picture { get { return map.bmap; } }
        public int Width
        {
            get { return elements[0, 0].width * elements.GetLength(1); }
        }
        public int Height
        {
            get { return elements[0, 0].height * elements.GetLength(0); }
        }
    }

    partial class mazeGraphicArray
    {
        public mazeElement this[int row, int column]
        {
            get
            {
                return elements[row, column];
            }
        }
    }

    partial class mazeGraphicArray
    {
        public void draw()
        {
            initMap();
        }
    }
}
