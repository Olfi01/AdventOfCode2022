using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DayTwelve
{
    public record DijkstraNode
    {
        public int X { get; }
        public int Y { get; }
        public int Elevation { get; }
        public DijkstraNode? Previous { get; set; }
        public int Distance { get; set; } = 200000;
        public bool Visited { get; set; } = false;

        public DijkstraNode(int x, int y, int elevation)
        {
            X = x;
            Y = y;
            Elevation = elevation;
        }
    }
}
