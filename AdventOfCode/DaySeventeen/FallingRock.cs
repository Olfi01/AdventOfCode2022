using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DaySeventeen
{
    public class FallingRock
    {
        public bool[][] Shape { get; }
        public int Column { get; set; }
        public int Row { get; set; }
        public FallingRock(bool[][] shape, int column, int row)
        {
            Shape = shape;
            Column = column;
            Row = row;
        }
        public bool? GetAt(int row, int col)
        {
            col -= Column;
            row -= Row;
            if (row >= 0 && row < Shape.Length && col >= 0 && col < Shape[row].Length) return Shape[row][col];
            return null;
        }
    }
}
