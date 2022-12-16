using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Valve
    {
        public string Id { get; }
        public int FlowRate { get; }
        public IEnumerable<string> TunnelsToValves { get; }
        public bool Opened { get; set; } = false;
        public Dictionary<string, int> Ways { get; set; } = new();
        public Valve(string id, int flowRate, IEnumerable<string> tunnelsToValves)
        {
            Id = id;
            FlowRate = flowRate;
            TunnelsToValves = tunnelsToValves;
        }
    }
}
