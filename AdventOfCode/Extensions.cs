using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Extensions
    {
        public static Stack<T> Copy<T>(this Stack<T> stack)
        {
            return new(new Stack<T>(stack));
        }

        public static (T, T) Copy<T>(this (T, T) tuple)
        {
            return (tuple.Item1, tuple.Item2);
        }
    }
}
