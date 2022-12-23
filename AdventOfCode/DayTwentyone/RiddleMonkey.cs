using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.DayTwentyone
{
    public class RiddleMonkey
    {
        public string Name { get; }
        public long? Number { get; set; }
        public (string Left, MathOperation Op, string Right)? Operation { get; private set; }
        private static readonly Regex riddleMonkeyRegex = new(@"^(?<name>[a-z]{4}): (?:(?<number>\d+)|(?<operation>[a-z]{4} [+\-*/] [a-z]{4}))");
        private static readonly Regex operationRegex = new(@"^(?<left>[a-z]{4}) (?<op>[+\-*/]) (?<right>[a-z]{4})");
        public RiddleMonkey(string line)
        {
            Match match = riddleMonkeyRegex.Match(line);
            if (!match.Success) throw new ArgumentException(nameof(riddleMonkeyRegex));
            Name = match.Groups["name"].Value;
            if (match.Groups.ContainsKey("number") && match.Groups["number"].Success)
            {
                Number = int.Parse(match.Groups["number"].Value);
            }
            else
            {
                Match match1 = operationRegex.Match(match.Groups["operation"].Value);
                if (!match1.Success) throw new ArgumentException(nameof(operationRegex));
                Operation = (match1.Groups["left"].Value, GetMathOperation(match1.Groups["op"].Value), match1.Groups["right"].Value);
            }
        }

        public void BecomeHuman()
        {
            Number = null;
            Operation = null;
        }

        private static MathOperation GetMathOperation(string op)
        {
            return op switch
            {
                "+" => MathOperation.Plus,
                "-" => MathOperation.Minus,
                "*" => MathOperation.Times,
                "/" => MathOperation.DividedBy,
                _ => throw new ArgumentException("Invalid math operation!")
            };
        }
    }

    public enum MathOperation
    {
        Plus,
        Minus,
        Times,
        DividedBy
    }
}
