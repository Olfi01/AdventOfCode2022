using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DayTwentyThree
{
    public class Elf
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public (int Row, int Col)? Proposition { get; set; }
        private int considerFirst = 0;
        public Elf(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public void ProposeMove(List<Elf> elves)
        {
            if (elves.Count(x => Math.Abs(x.Row - this.Row) < 2 && Math.Abs(x.Col - this.Col) < 2) < 2) return;
            for (int i = 0; i < 4; i++)
            {
                Direction direction = (Direction)((considerFirst + i) % 4);
                switch (direction)
                {
                    case Direction.North:
                        if (!elves.Any(x => x.Row == this.Row - 1 && Math.Abs(x.Col - this.Col) < 2))
                        {
                            Proposition = (Row - 1, Col);
                            return;
                        }
                        break;
                    case Direction.South:
                        if (!elves.Any(x => x.Row == this.Row + 1 && Math.Abs(x.Col - this.Col) < 2))
                        {
                            Proposition = (Row + 1, Col);
                            return;
                        }
                        break;
                    case Direction.West:
                        if (!elves.Any(x => x.Col == this.Col - 1 && Math.Abs(x.Row - this.Row) < 2))
                        {
                            Proposition = (Row, Col - 1);
                            return;
                        }
                        break;
                    case Direction.East:
                        if (!elves.Any(x => x.Col == this.Col + 1 && Math.Abs(x.Row - this.Row) < 2))
                        {
                            Proposition = (Row, Col + 1);
                            return;
                        }
                        break;
                }
            }
        }

        public void Move()
        {
            Move(true);
        }

        public void DontMove()
        {
            Move(false);
        }

        private void Move(bool doMove)
        {
            if (doMove && Proposition.HasValue)
            {
                (this.Row, this.Col) = Proposition.Value;
            }
            considerFirst = (considerFirst + 1) % 4;
        }
    }

    public enum Direction
    {
        North,
        South,
        West,
        East
    }
}
