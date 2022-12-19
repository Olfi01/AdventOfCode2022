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

        public static T[][] DeepCopy<T>(this T[][] arr)
        {
            T[][] result = new T[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = new T[arr[i].Length];
                for (int j = 0; j < arr[i].Length; j++)
                {
                    result[i][j] = arr[i][j];
                }
            }
            return result;
        }

        public static bool ContentEquals<T>(this T[] arr, T[] other) where T : struct
        {
            if (arr.Length != other.Length) return false;
            for (int i = 0; i < arr.Length; i++)
            {
                if (!arr[i].Equals(other[i])) return false;
            }
            return true;
        }
    }
}
