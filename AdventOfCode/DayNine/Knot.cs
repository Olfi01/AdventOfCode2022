using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DayNine
{
    public class Knot
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Knot? Next { get; init; }
        public Knot(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
