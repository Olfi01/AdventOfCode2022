using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DayEleven
{
    public class Monkey
    {
        public int Id { get; }
        public List<long> Items { get; }
        public string Operation { get; }
        public int TestDivisibleBy { get; }
        public int IfTrue { get; }
        public int IfFalse { get; }
        public long TotalInspections { get; set; } = 0;

        public Monkey(string str)
        {
            string[] lines = str.Split('\n');
            Id = int.Parse(lines[0].Substring("Monkey ".Length, 1));
            Items = lines[1].Substring("  Starting items: ".Length).Split(',')
                .Select(x => x.Trim()).Select(x => long.Parse(x)).ToList();
            Operation = lines[2].Substring("  Operation: new = ".Length);
            TestDivisibleBy = int.Parse(lines[3].Substring("  Test: divisible by ".Length));
            IfTrue = int.Parse(lines[4].Substring("    If true: throw to monkey ".Length));
            IfFalse = int.Parse(lines[5].Substring("    If false: throw to monkey ".Length));
        }

        public long PerformOperation(long old)
        {
            return Id switch
            {
                0 => old * 11,
                1 => old + 1,
                2 => (int)Math.Pow(old, 2),
                3 => old + 2,
                4 => old + 6,
                5 => old + 7,
                6 => old * 7,
                7 => old + 8,
                _ => int.MaxValue,
            };
        }
    }

    enum Op { Add, Multiply }
    record Monkey2(
        Queue<long> items, Op operation, int? modifier,
        int test, int trueMonkey, int falseMonkey
    )
    { public long Inspections; }
}
