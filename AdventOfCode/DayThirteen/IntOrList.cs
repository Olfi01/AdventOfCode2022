using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DayThirteen
{
    public class IntOrList
    {
        public int? Int { get; }
        public List<IntOrList>? List { get; }
        public IntOrList(int @int)
        {
            Int = @int;
        }

        public IntOrList(List<IntOrList> list)
        {
            List = list;
        }

        public static bool operator <(IntOrList left, IntOrList right)
        {
            if (left.Int != null && right.Int != null) return left.Int < right.Int;
            if (left.List != null && right.List != null)
            {
                for (int i = 0; i < Math.Max(left.List.Count, right.List.Count); i++)
                {
                    if (i >= left.List.Count) return true;
                    if (i >= right.List.Count) return false;
                    if (left.List[i] < right.List[i]) return true;
                    else if (left.List[i] > right.List[i]) return false;
                }
                return false;
            }
            if (left.Int != null) return new IntOrList(new List<IntOrList> { left }) < right;
            else return left < new IntOrList(new List<IntOrList> { right });
        }

        public static bool operator >(IntOrList left, IntOrList right)
        {
            return left != right && !(left < right);
        }

        public static bool operator ==(IntOrList left, IntOrList right)
        {
            if (left.List != null && right.List != null)
            {
                if (left.List.Count != right.List.Count) return false;
                for (int i = 0; i < left.List.Count; i++)
                {
                    if (left.List[i] != right.List[i]) return false;
                }
                return true;
            }
            return left.Int == right.Int;
        }

        public static bool operator !=(IntOrList left, IntOrList right)
        {
            return !(left == right);
        }
    }
}
