using AdventOfCode.DayTwentyTwo;
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

        public static Facing TurnRight(this Facing facing)
        {
            return facing switch
            {
                Facing.Right => Facing.Down,
                Facing.Down => Facing.Left,
                Facing.Left => Facing.Up,
                Facing.Up => Facing.Right,
                _ => throw new NotImplementedException()
            };
        }

        public static Facing TurnLeft(this Facing facing)
        {
            return facing switch
            {
                Facing.Right => Facing.Up,
                Facing.Down => Facing.Right,
                Facing.Left => Facing.Down,
                Facing.Up => Facing.Left,
                _ => throw new NotImplementedException()
            };
        }

        public static bool MoveNext(this bool?[,] map, Facing facing, ref (int Row, int Col) position)
        {
            int nextRow = position.Row;
            int nextCol = position.Col;
            do
            {
                switch (facing)
                {
                    case Facing.Right:
                        nextCol++;
                        break;
                    case Facing.Down:
                        nextRow++;
                        break;
                    case Facing.Left:
                        nextCol--;
                        break;
                    case Facing.Up:
                        nextRow--;
                        break;
                }

                int length0 = map.GetLength(0);
                if (nextRow < 0) nextRow += length0;
                if (nextRow >= length0) nextRow %= length0;
                int length1 = map.GetLength(1);
                if (nextCol < 0) nextCol += length1;
                if (nextCol >= length1) nextCol %= length1;
            } while (!map[nextRow, nextCol].HasValue);
            if (map[nextRow, nextCol] == true)
            {
                return false;
            }
            position.Row = nextRow;
            position.Col = nextCol;
            return true;
        }
        public static bool MoveNextCube(this bool?[,] map, ref Facing facing, ref (int Row, int Col) position)
        {
            int nextRow = position.Row;
            int nextCol = position.Col;
            Facing nextFacing = facing;
            switch (facing)
            {
                case Facing.Right:
                    nextCol++;
                    break;
                case Facing.Down:
                    nextRow++;
                    break;
                case Facing.Left:
                    nextCol--;
                    break;
                case Facing.Up:
                    nextRow--;
                    break;
            }
            // I'm too lazy to generalize this
            if (nextRow < 0 || nextRow >= map.GetLength(0) 
                || nextCol < 0 || nextCol >= map.GetLength(1) 
                || !map[nextRow, nextCol].HasValue)
            {
                if (nextRow == -1 && facing == Facing.Up)
                {
                    if (nextCol < 100)
                    {
                        nextRow = nextCol - 50 + 150;
                        nextCol = 0;
                        nextFacing = Facing.Right;
                    }
                    else
                    {
                        nextRow = 199;
                        nextCol -= 100;
                        nextFacing = Facing.Up;
                    }
                }
                else if (nextRow == 50 && facing == Facing.Down)
                {
                    nextRow = nextCol - 100 + 50;
                    nextCol = 99;
                    nextFacing = Facing.Left;
                }
                else if (nextRow == 99 && facing == Facing.Up)
                {
                    nextRow = nextCol + 50;
                    nextCol = 50;
                    nextFacing = Facing.Right;
                }
                else if (nextRow == 150 && facing == Facing.Down)
                {
                    nextRow = nextCol - 50 + 150;
                    nextCol = 49;
                    nextFacing = Facing.Left;
                }
                else if (nextRow == 200 && facing == Facing.Down)
                {
                    nextRow = 0;
                    nextCol += 100;
                    nextFacing = Facing.Down;
                }
                else if (nextCol == -1 && facing == Facing.Left)
                {
                    if (nextRow < 150)
                    {
                        nextCol = 50;
                        nextRow = 49 - (nextRow - 100);
                        nextFacing = Facing.Right;
                    }
                    else
                    {
                        nextCol = nextRow - 100;
                        nextRow = 0;
                        nextFacing = Facing.Down;
                    }
                }
                else if (nextCol == 49 && facing == Facing.Left)
                {
                    if (nextRow < 50)
                    {
                        nextCol = 0;
                        nextRow = 149 - nextRow;
                        nextFacing = Facing.Right;
                    }
                    else
                    {
                        nextCol = nextRow - 50;
                        nextRow = 100;
                        nextFacing = Facing.Down;
                    }
                }
                else if (nextCol == 50 && facing == Facing.Right)
                {
                    nextCol = nextRow - 150 + 50;
                    nextRow = 149;
                    nextFacing = Facing.Up;
                }
                else if (nextCol == 100 && facing == Facing.Right)
                {
                    if (nextRow < 100)
                    {
                        nextCol = nextRow - 50 + 100;
                        nextRow = 49;
                        nextFacing = Facing.Up;
                    }
                    else
                    {
                        nextCol = 149;
                        nextRow = 49 - (nextRow - 100);
                        nextFacing = Facing.Left;
                    }
                }
                else if (nextCol == 150 && facing == Facing.Right)
                {
                    nextCol = 99;
                    nextRow = 149 - nextRow;
                    nextFacing = Facing.Left;
                }
                else
                {
                    throw new NotImplementedException("Overlooked edge!");
                }
            }
            if (map[nextRow, nextCol] == true)
            {
                return false;
            }
            else if (map[nextRow, nextCol] == null) throw new NotImplementedException("Incorrect edge!");
            position.Row = nextRow;
            position.Col = nextCol;
            facing = nextFacing;
            return true;
        }
    }
}
