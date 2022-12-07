using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DaySeven
{
    public class FileOrDirectory
    {
        public long? File { get; }
        public Directory? Directory { get; }
        public bool IsFile()
        {
            return File != null;
        }

        public FileOrDirectory(long file)
        {
            File = file;
        }

        public FileOrDirectory(Directory directory)
        {
            Directory = directory;
        }
    }
}
