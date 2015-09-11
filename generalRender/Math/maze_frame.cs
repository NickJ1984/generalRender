using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace generalRender
{
    class maze_frame_calculation
    {
        #region Constants
        private const int resolution = 800;
        private const int lineWidth = 2;
        private const int minCellWidth = 4;
        private const int maxCellWidth = 40;
        private const int minCellNumber = 19;
        private const int maxCellNumber = 133;
        private const int defCellNumber = 60;
        #endregion

        #region Properties
        public int cellWidth { get; private set; }
        public int cellsNumberInLine { get; private set; }
        public int cellsNumber { get; private set; }
        public int linesNumber { get; private set; }
        public int remain { get; private set; }
        public int maxLine { get; private set; }
        #endregion

        #region Constructors
        public maze_frame_calculation() : this(defCellNumber) { }
        public maze_frame_calculation(int NumCells)
        {
            cellWidthCalculate(NumCells);
        }
        public maze_frame_calculation(maze_frame_calculation mfc)
            : this(mfc.cellsNumberInLine)
        { }
        #endregion

        #region Cell information
        #region Coords
        public Point getCellPosition(int number)
        {
            Point point_error = new Point(-1, -1);
            if (number > cellsNumber || number < 1) return point_error;

            Point result = new Point();
            int colNum = getCellColumn(number);
            int lineNum = getCellLine(number);
            result.X = (cellWidth + lineWidth) * colNum;
            result.Y = cellWidth * lineNum + lineWidth * (lineNum + 1);
            return result;
        }
        public int getCellX(int number)
        {
            return getCellPosition(number).X;
        }
        public int getCellY(int number)
        {
            return getCellPosition(number).Y;
        }
        #endregion
        #region Columns&Lines
        public int getCellLine(int number)
        {
            if (number > cellsNumber || number < 1) return -1;

            if (number > cellsNumberInLine)
                return ((number % cellsNumberInLine) != 0) ? number / cellsNumberInLine : number / cellsNumberInLine - 1;
            else return 0;
        }
        public int getCellColumn(int number)
        {
            if (number > cellsNumber || number < 1) return -1;
            return number - (cellsNumberInLine * getCellLine(number));
        }
        #endregion
        #region Movement
        public int stepUp(int number)
        {
            if (getCellLine(number) > 0) return number - cellsNumberInLine;
            else return number;
        }
        public int stepDown(int number)
        {
            if (number < getCellLine(cellsNumber)) return number + cellsNumberInLine;
            else return number;
        }
        public int stepLeft(int number)
        {
            if (getCellLine(number) == getCellLine(number - 1)) return number - 1;
            else return number;
        }
        public int stepRight(int number)
        {
            if (getCellLine(number) == getCellLine(number + 1)) return number + 1;
            else return number;
        }
        #endregion
        #region Bounds
        public bool isBound(int number)
        {
            int pos = getCellColumn(number);
            int line = getCellLine(number);
            if (line == 0 || line == maxLine) return true;
            if (pos == 1 || pos == cellsNumberInLine) return true;
            return false;            
        }
        
        public eBounds isBounds(int number)
        {
            int posCol = getCellColumn(number);
            int posLine = getCellLine(number);

            if (posCol == 1 && posLine == 0) return eBounds.Left | eBounds.Up;
            if (posCol == cellsNumberInLine && posLine == 0) return eBounds.Right | eBounds.Up;
            if (posCol == 1 && posLine == maxLine) return eBounds.Left | eBounds.Down;
            if (posCol == cellsNumberInLine && posLine == maxLine) return eBounds.Right | eBounds.Down;
            if (posCol == 1) return eBounds.Left;
            if (posCol == cellsNumberInLine) return eBounds.Right;
            if (posLine == 0) return eBounds.Up;
            if (posLine == maxLine) return eBounds.Down;
            return 0;
        }
        #endregion
        #endregion

        #region Array
        
        public mCell[] getMCellArray()
        {
            mCell[] result = new mCell[cellsNumber];
            for(int i = 0; i < cellsNumber; i++)
            {
                result[i] = new mCell(getCellColumn(i + 1), getCellLine(i + 1), getCellPosition(i + 1), cellWidth, isBounds(i + 1));
            }
            return result;
        }
        #endregion
        #region Service
        private void cellWidthCalculate(int nCells)
        {
            if (!checkCellNumberInit(nCells)) nCells = defCellNumber;

            linesNumber = nCells + 1;
            cellsNumberInLine = nCells;
            int _width = resolution - linesOverallWidth(nCells);
            cellWidth = _width / nCells;
            remain = resolution - (cellsNumberInLine * cellWidth) - (linesNumber * lineWidth);
            cellsNumber = cellsNumberInLine * cellsNumberInLine;
            maxLine = getCellLine(cellsNumber);
        }
        private bool checkCellNumberInit(int num)
        { return (num >= minCellNumber && num <= maxCellNumber) ? true : false; }
        private bool checkCellNumber(int number)
        { return (number > 0 && number <= cellsNumber) ? true : false; }
        private int linesOverallWidth(int num)
        { return lineWidth * (num + 1); }
        #endregion

    }
}
