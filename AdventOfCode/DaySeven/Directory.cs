using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DaySeven
{
    public class Directory
    {
        public Directory Parent { get; }
        public Dictionary<string, FileOrDirectory> Content { get; } = new();
        public Directory(Directory parent)
        {
            Parent = parent;
        }

        private Directory()
        {
            Parent = this;
        }
        public static Directory CreateRootDir()
        {
            return new();
        }
    }
}
