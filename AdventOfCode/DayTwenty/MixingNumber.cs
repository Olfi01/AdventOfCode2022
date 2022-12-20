using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DayTwenty
{
    public class MixingNumber
    {
        public int OriginalIndex { get; }
        public long Value { get; }
        public MixingNumber(int originalIndex, long value)
        {
            OriginalIndex = originalIndex;
            Value = value;
        }
    }
}
