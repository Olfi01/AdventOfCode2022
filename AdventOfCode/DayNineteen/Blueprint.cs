using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.DayNineteen
{
    public class Blueprint
    {
        public int Id { get; }
        public int OreRobotCost { get; }
        public int ClayRobotCost { get; }
        public int ObsidianRobotOreCost { get; }
        public int ObsidianRobotClayCost { get; }
        public int GeodeRobotOreCost { get; }
        public int GeodeRobotObsidianCost { get; }
        public int? QualityLevel { get; set; } = null;

        private static readonly Regex blueprintRegex = 
            new(@"Blueprint (?<id>\d+): Each ore robot costs (?<orc>\d+) ore. Each clay robot costs (?<crc>\d+) ore. Each obsidian robot costs (?<oroc>\d+) ore and (?<orcc>\d+) clay. Each geode robot costs (?<grorc>\d+) ore and (?<grobc>\d+) obsidian.");
        public Blueprint(string input)
        {
            Match match = blueprintRegex.Match(input);
            if (!match.Success) throw new ArgumentException("Blueprint regex failed!");
            Id = int.Parse(match.Groups["id"].Value);
            OreRobotCost = int.Parse(match.Groups["orc"].Value);
            ClayRobotCost = int.Parse(match.Groups["crc"].Value);
            ObsidianRobotOreCost = int.Parse(match.Groups["oroc"].Value);
            ObsidianRobotClayCost = int.Parse(match.Groups["orcc"].Value);
            GeodeRobotOreCost = int.Parse(match.Groups["grorc"].Value);
            GeodeRobotObsidianCost = int.Parse(match.Groups["grobc"].Value);
        }
    }
}
