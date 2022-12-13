using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.DayThirteen
{
    public class Packet : IComparable
    {
        public List<IntOrList> Buffer { get; }
        public Packet(string str)
        {
            Buffer = Decode(str);
        }

        private static readonly Regex regex = new(@"\[\]");
        private static List<IntOrList> Decode(string str)
        {
            List<IntOrList> list = new();
            StringBuilder sb = new();
            int openedBrackets = 0;
            foreach (char c in str[1..^1])
            {
                if (openedBrackets == 0 && c == ',')
                {
                    if (sb.Length > 0)
                    {
                        list.Add(new(int.Parse(sb.ToString())));
                        sb.Clear();
                    }
                    continue;
                }
                sb.Append(c);
                if (c == '[')
                {
                    openedBrackets++;
                }
                if (c == ']')
                {
                    if (--openedBrackets == 0)
                    {
                        list.Add(new(Decode(sb.ToString())));
                        sb.Clear();
                        continue;
                    }
                }
            }
            if (sb.Length > 0) list.Add(new(int.Parse(sb.ToString())));
            return list;
        }

        public int CompareTo(object? obj)
        {
            if (obj is not Packet p) return 0;
            if (this < p) return -1;
            if (this > p) return 1;
            return 0;
        }

        public static bool operator <(Packet left, Packet right)
        {
            return new IntOrList(left.Buffer) < new IntOrList(right.Buffer);
        }

        public static bool operator >(Packet left, Packet right)
        {
            return new IntOrList(left.Buffer) > new IntOrList(right.Buffer);
        }
    }
}
