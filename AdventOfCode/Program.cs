using AdventOfCode;
using AdventOfCode.DayEleven;
using AdventOfCode.DayFifteen;
using AdventOfCode.DayFourteen;
using AdventOfCode.DayNine;
using AdventOfCode.DayNineteen;
using AdventOfCode.DaySeven;
using AdventOfCode.DaySeventeen;
using AdventOfCode.DayThirteen;
using AdventOfCode.DayTwelve;
using System.Collections.Immutable;
using System.Text;
using System.Text.RegularExpressions;
using Directory = AdventOfCode.DaySeven.Directory;

class Program
{
    static async Task Main()
    {
        string? url = null;
        int day;
        do
        {
            Console.Write("Enter day number: ");
            string? cliIn = Console.ReadLine();
            if (!int.TryParse(cliIn, out day) || day > 25 || day < 1)
            {
                Console.Clear();
                continue;
            }
            url = $"https://adventofcode.com/2022/day/{day}/input";
        } while (url == null);
        string sessionCookie = File.ReadAllText("session.txt");
        using HttpClient client = new();
        client.DefaultRequestHeaders.Add("Cookie", $"session={sessionCookie}");
        string input = await client.GetStringAsync(url);
        Solve(day, input);
        Console.Read();
    }

    static void Solve(int day, string input)
    {
        switch (day)
        {
            case 1:
                DayOne(input);
                break;
            case 2:
                DayTwo(input);
                break;
            case 3:
                DayThree(input);
                break;
            case 4:
                DayFour(input);
                break;
            case 5:
                DayFive(input);
                break;
            case 6:
                DaySix(input);
                break;
            case 7:
                DaySeven(input);
                break;
            case 8:
                DayEight(input);
                break;
            case 9:
                DayNine(input);
                break;
            case 10:
                DayTen(input);
                break;
            case 11:
                DayEleven(input);
                break;
            case 12:
                DayTwelve(input);
                break;
            case 13:
                DayThirteen(input);
                break;
            case 14:
                DayFourteen(input);
                break;
            case 15:
                DayFifteen(input);
                break;
            case 16:
                DaySixteen(input);
                break;
            case 17:
                DaySeventeen(input);
                break;
            case 18:
                DayEightteen(input);
                break;
            case 19:
                DayNineteen(input);
                break;
            default:
                throw new NotImplementedException("Haven't coded this day yet!");
        }
    }

    static void DayOne(string input)
    {
        string[] lines = input.Split('\n');
        int currentElf = 0;
        int maxCalories = 0;
        int secondMostCalories = 0;
        int thirdMostCalories = 0;
        void UpdateTop3(int currentElf, ref int maxCalories, ref int secondMostCalories, ref int thirdMostCalories)
        {
            if (currentElf > thirdMostCalories)
            {
                thirdMostCalories = currentElf;
                if (thirdMostCalories > secondMostCalories)
                {
                    (thirdMostCalories, secondMostCalories) = (secondMostCalories, thirdMostCalories);
                    if (secondMostCalories > maxCalories)
                    {
                        (secondMostCalories, maxCalories) = (maxCalories, secondMostCalories);
                    }
                }
                Console.WriteLine($"New top 3:\n" +
                    $"  1. {maxCalories}\n" +
                    $"  2. {secondMostCalories}\n" +
                    $"  3. {thirdMostCalories}");
            }
        }
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                Console.WriteLine($"Current elf: {currentElf} calories.");
                UpdateTop3(currentElf, ref maxCalories, ref secondMostCalories, ref thirdMostCalories);
                currentElf = 0;
                continue;
            }
            int calories = int.Parse(line);
            currentElf += calories;
        }
        UpdateTop3(currentElf, ref maxCalories, ref secondMostCalories, ref thirdMostCalories);

        Console.WriteLine($"Maximum calories: {maxCalories}");
        int top3Sum = maxCalories + secondMostCalories + thirdMostCalories;
        Console.WriteLine($"Total calories of top 3: {top3Sum}");
    }

    static void DayTwo(string input)
    {
        int CalculateScoreCase1(string opponent, string me)
        {
            int score = 0;
            switch ((opponent, me))
            {
                case ("A", "X"):
                case ("B", "Y"):
                case ("C", "Z"):
                    Console.Write("You draw ");
                    score += 3;
                    break;
                case ("A", "Y"):
                case ("B", "Z"):
                case ("C", "X"):
                    Console.Write("You win ");
                    score += 6;
                    break;
                case ("A", "Z"):
                case ("B", "A"):
                case ("C", "B"):
                    Console.Write("You lose ");
                    score += 0;
                    break;
            }
            switch (me)
            {
                case "X":
                    Console.WriteLine("with rock.");
                    score += 1;
                    break;
                case "Y":
                    Console.WriteLine("with paper.");
                    score += 2;
                    break;
                case "Z":
                    Console.WriteLine("with scissors.");
                    score += 3;
                    break;
            }
            Console.WriteLine($"Score case 1: {score}");
            return score;
        }
        int CalculateScoreCase2(string opponent, string me)
        {
            int score = 0;
            switch (me)
            {
                case "X":
                    Console.Write("You lose ");
                    score += 0;
                    break;
                case "Y":
                    Console.Write("You draw ");
                    score += 3;
                    break;
                case "Z":
                    Console.Write("You win ");
                    score += 6;
                    break;
            }
            switch ((opponent, me))
            {
                case ("A", "Y"):
                case ("B", "X"):
                case ("C", "Z"):
                    Console.WriteLine("with rock.");
                    score += 1;
                    break;
                case ("A", "Z"):
                case ("B", "Y"):
                case ("C", "X"):
                    Console.WriteLine("with paper.");
                    score += 2;
                    break;
                case ("A", "X"):
                case ("B", "Z"):
                case ("C", "Y"):
                    Console.WriteLine("with scissors.");
                    score += 3;
                    break;
            }
            Console.WriteLine($"Score: {score}");
            return score;
        }
        string[] lines = input.Split('\n');
        int score1 = 0, score2 = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            string[] split = line.Split(' ');
            string opponent = split[0];
            string me = split[1];
            Console.WriteLine("Case 1:");
            score1 += CalculateScoreCase1(opponent, me);
            Console.WriteLine("Case 2:");
            score2 += CalculateScoreCase2(opponent, me);
        }
        Console.WriteLine($"Total score case 1: {score1}");
        Console.WriteLine($"Total score case 2: {score2}");
    }

    static void DayThree(string input)
    {
        int GetPriority(char c)
        {
            int value = (int)c;
            if (value >= 97 && value < 97 + 26)
                return value - 96;
            else if (value >= 65 && value < 65 + 26)
                return value - 38;
            else
                throw new ArgumentException("Illegal char!");
        }
        string[] lines = input.Split('\n');
        int sumPriority = 0;
        int sumBadgePriority = 0;
        List<string> group = new();
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            int halfIndex = line.Length / 2;
            string firstHalf = line.Remove(halfIndex);
            string secondHalf = line.Substring(halfIndex);
            char commonItem = firstHalf.Intersect(secondHalf).First();
            int priority = GetPriority(commonItem);
            Console.WriteLine($"{commonItem} ({priority})");
            sumPriority += priority;
            group.Add(line);
            if (group.Count == 3)
            {
                char badge = group[0].Intersect(group[1]).Intersect(group[2]).First();
                int badgePriority = GetPriority(badge);
                Console.WriteLine($"Badge: {badge} ({badgePriority})");
                sumBadgePriority += badgePriority;
                group.Clear();
            }
        }
        Console.WriteLine($"Sum of priorities: {sumPriority}");
        Console.WriteLine($"Sum of badge priorities: {sumBadgePriority}");
    }

    static void DayFour(string input)
    {
        (int, int) GetRange(string rangeStr)
        {
            string[] split = rangeStr.Split('-');
            return (int.Parse(split[0]), int.Parse(split[1]));
        }
        bool Contains((int, int) range, (int, int) other)
        {
            return range.Item1 <= other.Item1 && range.Item2 >= other.Item2;
        }
        bool Overlaps((int, int) range, (int, int) other)
        {
            return (range.Item1 >= other.Item1 && range.Item1 <= other.Item2)
                || (range.Item2 >= other.Item1 && range.Item2 <= other.Item2);
        }
        int fullyContained = 0;
        int overlapping = 0;
        foreach (string line in input.Split('\n'))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            string[] split = line.Split(',');
            (int, int) firstRange = GetRange(split[0]);
            (int, int) secondRange = GetRange(split[1]);
            if (Contains(firstRange, secondRange)
                || Contains(secondRange, firstRange))
                fullyContained++;
            if (Overlaps(firstRange, secondRange)
                || Overlaps(secondRange, firstRange))
                overlapping++;
        }
        Console.WriteLine($"Number of fully redundant elves: {fullyContained}");
        Console.WriteLine($"Number of overlapping assignment pairs: {overlapping}");
    }

    static void DayFive(string input)
    {
        string[] blocks = input.Split("\n\n");
        string[] position = blocks[0].Split('\n');
        string[] actions = blocks[1].Split('\n');
        List<Stack<string>> stacks = new();
        Regex cratePattern = new(@"\[(?<crate>.)\]");
        foreach (string line in position.Reverse())
        {
            string remaining = line;
            int i = 0;
            while (remaining.Length > 4)
            {
                string crate = remaining[..3];
                while (stacks.Count <= i) stacks.Add(new());
                Match match = cratePattern.Match(crate);
                if (match.Success) stacks[i].Push(match.Groups["crate"].Value);
                remaining = remaining[4..];
                i++;
            }
            while (stacks.Count <= i) stacks.Add(new());
            Match match2 = cratePattern.Match(remaining);
            if (match2.Success) stacks[i].Push(match2.Groups["crate"].Value);
        }

        List<Stack<string>> stacks2 = new();
        foreach (Stack<string> stack in stacks) stacks2.Add(stack.Copy());

        Regex actionRegex = new(
            @"move (?<n>\d+) from (?<source>\d+) to (?<target>\d+)");
        foreach (string action in actions)
        {
            if (string.IsNullOrWhiteSpace(action)) continue;
            Match match = actionRegex.Match(action);
            int n = int.Parse(match.Groups["n"].Value);
            int source = int.Parse(match.Groups["source"].Value) - 1;
            int target = int.Parse(match.Groups["target"].Value) - 1;
            // CraneMover 9000
            for (int i = 0; i < n; i++)
            {
                stacks[target].Push(stacks[source].Pop());
            }

            // CraneMover 8001
            Stack<string> temp = new();
            for (int i = 0; i < n; i++)
            {
                temp.Push(stacks2[source].Pop());
            }
            while (temp.TryPop(out string? crate))
            {
                stacks2[target].Push(crate);
            }
        }
        Console.Write("CraneMover 9000: ");
        foreach (Stack<string> stack in stacks)
        {
            Console.Write(stack.Peek());
        }
        Console.WriteLine();
        Console.Write("CraneMover 8001 " +
            "(mocks behavior of CraneMover 9001 inefficiently): ");
        foreach (Stack<string> stack in stacks2)
        {
            Console.Write(stack.Peek());
        }
        Console.WriteLine();
    }

    static void DaySix(string input)
    {
        char[] buffer = new char[4];
        char[] messageBuffer = new char[14];
        int i = 0;
        bool packetMarkerFound = false;
        foreach (char c in input)
        {
            buffer[i++ % 4] = c;
            messageBuffer[(i - 1) % 14] = c;
            if (i > 3 && buffer.Distinct().Count() == 4 && !packetMarkerFound)
            {
                Console.WriteLine($"Packet marker found at {i}.");
                packetMarkerFound = true;
            }
            if (i > 13 && messageBuffer.Distinct().Count() == 14)
            {
                Console.WriteLine($"Message marker found at {i}.");
                break;
            }
        }
    }

    static void DaySeven(string input)
    {
        string[] lines = input.Split('\n');
        Directory rootDir = Directory.CreateRootDir();
        Directory currentDir = rootDir;
        Regex fileAndSize = new(@"^(?<size>\d+) (?<name>.+)$");
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.StartsWith("$ cd "))
            {
                string path = line[5..];
                if (path == "/") currentDir = rootDir;
                else if (path == "..")
                {
                    currentDir = currentDir.Parent;
                }
                else
                {
                    currentDir = currentDir.Content[path].Directory
                        ?? throw new Exception();
                }
            }
            else if (line.StartsWith("$ ls"))
            {
                continue;
            }
            else if (line.StartsWith("dir "))
            {
                string path = line[4..];
                if (!currentDir.Content.ContainsKey(path))
                    currentDir.Content[path] = new(new Directory(currentDir));
            }
            else
            {
                Match match = fileAndSize.Match(line);
                string fileName = match.Groups["name"].Value;
                long fileSize = long.Parse(match.Groups["size"].Value);
                currentDir.Content[fileName] = new(fileSize);
            }
        }
        Dictionary<Directory, long> dirSizes = new();
        long CalculateSize(Directory dir)
        {
            long sum = 0;
            foreach (var kvp in dir.Content)
            {
                if (kvp.Value?.IsFile() ?? false)
                {
                    long fileSize = kvp.Value?.File ?? throw new Exception();
                    sum += fileSize;
                    Console.WriteLine($"File {kvp.Key} ({fileSize})");
                }
                else
                {
                    Console.WriteLine($"Directory {kvp.Key}");
                    long dirSize = CalculateSize(kvp.Value?.Directory ?? throw new Exception());
                    dirSizes[kvp.Value?.Directory ?? throw new Exception()] = dirSize;
                    sum += dirSize;
                    Console.WriteLine($"End of directory {kvp.Key} ({dirSize})");
                }
            }
            return sum;
        }
        Console.WriteLine("Directory /");
        dirSizes[rootDir] = CalculateSize(rootDir);
        Console.WriteLine($"End of directory / ({dirSizes[rootDir]})");
        long sum = 0;
        foreach (var kvp in dirSizes)
        {
            if (kvp.Value <= 100000) sum += kvp.Value;
        }
        Console.WriteLine($"Sum of directory sizes below 100000: {sum}");
        Console.WriteLine($"Size of root dir: {dirSizes[rootDir]}");
        long spaceAvailable = 70000000 - dirSizes[rootDir];
        Console.WriteLine($"Space available: {spaceAvailable}");
        long spaceMissing = 30000000 - spaceAvailable;
        Console.WriteLine($"Space missing: {spaceMissing}");
        KeyValuePair<Directory, long> directoryToDelete =
            dirSizes
            .Where(kvp => kvp.Value >= spaceMissing)
            .OrderBy(kvp => kvp.Value)
            .First();
        Console.WriteLine($"Directory to delete: " +
            $"{directoryToDelete.Key.Parent.Content.First(x => x.Value.Directory == directoryToDelete.Key).Key} " +
            $"({directoryToDelete.Value})");
    }

    static void DayEight(string input)
    {
        string[] lines = input.Split('\n');
        int rows = lines
            .Where(x => !string.IsNullOrWhiteSpace(x)).Count();
        int cols = lines[0].Length;
        int[,] grid = new int[cols, rows];
        int x = 0, y = 0;
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            foreach (char c in line)
            {
                grid[x, y] = (int)char.GetNumericValue(c);
                x++;
            }
            x = 0;
            y++;
        }
        int visibleTrees = 0;
        int[,] viewingScores = new int[cols, rows];
        for (x = 0; x < cols; x++)
        {
            for (y = 0; y < rows; y++)
            {
                int thisHeight = grid[x, y];
                bool visible = true;
                bool visibleIncremented = false;
                int viewingRangeXm = 0;
                for (int x0 = x - 1; x0 >= 0; x0--)
                {
                    viewingRangeXm++;
                    if (grid[x0, y] >= thisHeight)
                    {
                        visible = false;
                        break;
                    }
                }
                if (!visibleIncremented && visible)
                {
                    visibleTrees++;
                    visibleIncremented = true;
                }
                visible = true;
                int viewingRangeXp = 0;
                for (int x0 = x + 1; x0 < cols; x0++)
                {
                    viewingRangeXp++;
                    if (grid[x0, y] >= thisHeight)
                    {
                        visible = false;
                        break;
                    }
                }
                if (!visibleIncremented && visible)
                {
                    visibleTrees++;
                    visibleIncremented = true;
                }
                visible = true;
                int viewingRangeYm = 0;
                for (int y0 = y - 1; y0 >= 0; y0--)
                {
                    viewingRangeYm++;
                    if (grid[x, y0] >= thisHeight)
                    {
                        visible = false;
                        break;
                    }
                }
                if (!visibleIncremented && visible)
                {
                    visibleTrees++;
                    visibleIncremented = true;
                }
                visible = true;
                int viewingRangeYp = 0;
                for (int y0 = y + 1; y0 < rows; y0++)
                {
                    viewingRangeYp++;
                    if (grid[x, y0] >= thisHeight)
                    {
                        visible = false;
                        break;
                    }
                }
                if (!visibleIncremented && visible)
                {
                    visibleTrees++;
                    visibleIncremented = true;
                }
                viewingScores[x, y]
                    = viewingRangeXm * viewingRangeXp * viewingRangeYm * viewingRangeYp;
            }
        }
        int viewHighscore = 0;
        foreach (int score in viewingScores)
        {
            if (score > viewHighscore) viewHighscore = score;
        }
        Console.WriteLine($"Visible trees: {visibleTrees}");
        Console.WriteLine($"Highest viewing score: {viewHighscore}");
    }

    static void DayNine(string input)
    {
        string[] lines = input.Split('\n');
        Knot tail = new(0, 0);
        Knot head = new(0, 0) { Next = tail };
        Knot tail2 = new(0, 0);
        Knot eight = new(0, 0) { Next = tail2 };
        Knot seven = new(0, 0) { Next = eight };
        Knot six = new(0, 0) { Next = seven };
        Knot five = new(0, 0) { Next = six };
        Knot four = new(0, 0) { Next = five };
        Knot three = new(0, 0) { Next = four };
        Knot two = new(0, 0) { Next = three };
        Knot one = new(0, 0) { Next = two };
        Knot head2 = new(0, 0) { Next = one };
        HashSet<(int, int)> visited = new()
        {
            (tail.X, tail.Y)
        };
        HashSet<(int, int)> visited2 = new()
        {
            (tail2.X, tail2.Y)
        };
        static void FollowKnot(Knot next, Knot knot)
        {
            if (Math.Abs(knot.X - next.X) >= 2 || Math.Abs(knot.Y - next.Y) >= 2)
            {
                if (knot.X == next.X)
                {
                    if (knot.Y > next.Y) next.Y++;
                    else next.Y--;
                }
                else if (knot.Y == next.Y)
                {
                    if (knot.X > next.X) next.X++;
                    else next.X--;
                }
                else
                {
                    if (knot.X > next.X) next.X++;
                    else next.X--;
                    if (knot.Y > next.Y) next.Y++;
                    else next.Y--;
                }
            }
            if (next.Next != null) FollowKnot(next.Next, next);
        }
        static void MoveKnot(string direction, Knot knot)
        {
            switch (direction)
            {
                case "R":
                    knot.X++;
                    break;
                case "L":
                    knot.X--;
                    break;
                case "U":
                    knot.Y++;
                    break;
                case "D":
                    knot.Y--;
                    break;
            }
            if (knot.Next == null) return;
            Knot next = knot.Next;
            FollowKnot(next, knot);
        }
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            string[] split = line.Split(" ");
            string direction = split[0];
            int n = int.Parse(split[1]);
            for (int i = 0; i < n; i++)
            {
                MoveKnot(direction, head);
                visited.Add((tail.X, tail.Y));
                MoveKnot(direction, head2);
                visited2.Add((tail2.X, tail2.Y));
            }
        }
        Console.WriteLine($"Short rope tail visited {visited.Count} fields.");
        Console.WriteLine($"Long rope tail visited {visited2.Count} fields.");
    }

    static void DayTen(string input)
    {
        string[] lines = input.Split('\n');
        int x = 1;
        int cycle = 0;
        HashSet<int> recordCycles = new() { 20, 60, 100, 140, 180, 220 };
        Dictionary<int, int> records = new();
        void IncrementCycle()
        {
            int cpos = cycle % 40;
            char c = Math.Abs(cpos - x) <= 1 ? '#' : '.';
            if (cpos == 39)
            {
                Console.WriteLine(c);
            }
            else
            {
                Console.Write(c);
            }
            cycle++;
            if (recordCycles.Contains(cycle)) records.Add(cycle, x);
        }
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line == "noop")
            {
                IncrementCycle();
            }
            else
            {
                int add = int.Parse(line.Split(" ")[1]);
                IncrementCycle();
                IncrementCycle();
                x += add;
            }
        }
        int sumSignalStrengths = records.Select(kvp => kvp.Key * kvp.Value).Sum();
        Console.WriteLine($"Sum of the 6 signal strengths: {sumSignalStrengths}");
    }

    static void DayEleven(string input)
    {
        static void PartOne(string input)
        {
            string[] monkeyStrings = input.Split("\n\n");
            Dictionary<int, Monkey> monkeys = new();

            foreach (string monkeyString in monkeyStrings)
            {
                Monkey monkey = new Monkey(monkeyString);
                monkeys.Add(monkey.Id, monkey);
            }
            long commonDenom = monkeys.Select(x => x.Value.TestDivisibleBy).Aggregate((x, y) => x * y);
            for (int round = 0; round < 20; round++)
            {
                foreach (Monkey monkey in monkeys.Values.OrderBy(x => x.Id))
                {
                    foreach (int item in monkey.Items)
                    {
                        //Expression exp = new(monkey.Operation.Replace("old", item.ToString()));
                        //int worry = (int)exp.Evaluate();
                        long worry = monkey.PerformOperation(item) % commonDenom;
                        monkey.TotalInspections++;
                        worry /= 3;
                        if (worry % monkey.TestDivisibleBy == 0) monkeys[monkey.IfTrue].Items.Add(worry);
                        else monkeys[monkey.IfFalse].Items.Add(worry);
                    }
                    monkey.Items.Clear();
                }
            }
            long[] meddlers = monkeys.OrderByDescending(x => x.Value.TotalInspections)
                .Select(x => x.Value.TotalInspections).Take(2).ToArray();
            Console.WriteLine($"Level of monkey business: {meddlers[0] * meddlers[1]}");
        }
        static void PartTwo(string input)
        {
            var monkeys = input.Split('\n')
                .Where(l => !string.IsNullOrWhiteSpace(l)).Chunk(6)
                .Select(l => new Monkey2(
                            new Queue<long>(l[1][18..].Split(',').Select(long.Parse)),
                            l[2][23..][0] switch
                            {
                                '*' => Op.Multiply,
                                '+' => Op.Add,
                                _ => throw new Exception($"unknown operation")
                            },
                            l[2][25..] == "old" ? null : int.Parse(l[2][25..]),
                            int.Parse(l[3][21..]),
                            int.Parse(l[4][28..]), int.Parse(l[5][29..]))
                 ).ToList();

            var divisorLimit = monkeys.Aggregate(1, (c, m) => c * m.test);

            for (var round = 0; round < 10000; ++round)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.items.Any())
                    {
                        monkey.Inspections++;
                        var worryLevel = monkey.items.Dequeue();
                        worryLevel = monkey.operation switch
                        {
                            Op.Add => worryLevel + (monkey.modifier ?? worryLevel),
                            Op.Multiply => worryLevel * (monkey.modifier ?? worryLevel),
                            _ => throw new ArgumentOutOfRangeException()
                        };
                        worryLevel %= divisorLimit;
                        var targetMonkey = worryLevel % monkey.test == 0 ? monkey.trueMonkey : monkey.falseMonkey;
                        monkeys[targetMonkey].items.Enqueue(worryLevel);
                    }
                }
            }

            monkeys.Sort((a, b) => b.Inspections.CompareTo(a.Inspections));
            Console.WriteLine($"Result {monkeys[0].Inspections * monkeys[1].Inspections}");
        }
        PartOne(input);
        PartTwo(input);
    }

    static void DayTwelve(string input)
    {
        void PartOne(string input)
        {
            string[] lines = input.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            DijkstraNode[,] grid = new DijkstraNode[lines[0].Length, lines.Length];
            DijkstraNode? startNode = null;
            DijkstraNode? endNode = null;
            for (int x = 0; x < lines[0].Length; x++)
            {
                for (int y = 0; y < lines.Length; y++)
                {
                    char c = lines[y][x];
                    DijkstraNode node;
                    if (c == 'S')
                    {
                        node = new(x, y, 'a') { Distance = 0 };
                        startNode = node;
                    }
                    else if (c == 'E')
                    {
                        node = new(x, y, 'z');
                        endNode = node;
                    }
                    else node = new(x, y, c);
                    grid[x, y] = node;
                }
            }
            if (startNode == null || endNode == null) return;
            (int x, int y)[] neighborDeltas = { (-1, 0), (0, -1), (1, 0), (0, 1) };
            List<DijkstraNode> GetNeighborNodes(DijkstraNode node)
            {
                List<DijkstraNode> neighbors = new();
                foreach (var (xd, yd) in neighborDeltas)
                {
                    int x0 = node.X + xd;
                    int y0 = node.Y + yd;
                    if (x0 < grid.GetLength(0) && y0 < grid.GetLength(1) && x0 >= 0 && y0 >= 0)
                    {
                        DijkstraNode neighbor = grid[x0, y0];
                        if (!neighbor.Visited && neighbor.Elevation <= node.Elevation + 1)
                            neighbors.Add(neighbor);
                    }
                }
                return neighbors;
            }
            DijkstraNode GetClosestUnvisitedNode()
            {
                DijkstraNode? closest = null;
                foreach (DijkstraNode node in grid)
                {
                    if (node.Visited) continue;
                    if (closest == null) closest = node;
                    else if (closest.Distance > node.Distance) closest = node;
                }
                return closest ?? throw new NotImplementedException();
            }
            DijkstraNode currentNode = startNode;
            while (currentNode != endNode)
            {
                currentNode.Visited = true;
                List<DijkstraNode> neighbors = GetNeighborNodes(currentNode);
                foreach (DijkstraNode next in neighbors)
                {
                    int newDistance = currentNode.Distance + 1;
                    if (newDistance < next.Distance)
                    {
                        next.Distance = newDistance;
                        next.Previous = currentNode;
                    }
                }
                currentNode = GetClosestUnvisitedNode();
            }
            int distance = 0;
            while (currentNode != startNode)
            {
                distance++;
                currentNode = currentNode.Previous ?? throw new NotImplementedException();
            }
            Console.WriteLine($"Shortest path has {distance} steps.");
        }

        void PartTwo(string input)
        {
            string[] lines = input.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            DijkstraNode[,] grid = new DijkstraNode[lines[0].Length, lines.Length];
            DijkstraNode? startNode = null;
            for (int x = 0; x < lines[0].Length; x++)
            {
                for (int y = 0; y < lines.Length; y++)
                {
                    char c = lines[y][x];
                    DijkstraNode node;
                    if (c == 'S')
                    {
                        node = new(x, y, 'a');
                    }
                    else if (c == 'E')
                    {
                        node = new(x, y, 'z') { Distance = 0 };
                        startNode = node;
                    }
                    else node = new(x, y, c);
                    grid[x, y] = node;
                }
            }
            if (startNode == null) return;
            (int x, int y)[] neighborDeltas = { (-1, 0), (0, -1), (1, 0), (0, 1) };
            List<DijkstraNode> GetNeighborNodes(DijkstraNode node)
            {
                List<DijkstraNode> neighbors = new();
                foreach (var (xd, yd) in neighborDeltas)
                {
                    int x0 = node.X + xd;
                    int y0 = node.Y + yd;
                    if (x0 < grid.GetLength(0) && y0 < grid.GetLength(1) && x0 >= 0 && y0 >= 0)
                    {
                        DijkstraNode neighbor = grid[x0, y0];
                        if (!neighbor.Visited && neighbor.Elevation >= node.Elevation - 1)
                            neighbors.Add(neighbor);
                    }
                }
                return neighbors;
            }
            DijkstraNode GetClosestUnvisitedNode()
            {
                DijkstraNode? closest = null;
                foreach (DijkstraNode node in grid)
                {
                    if (node.Visited) continue;
                    if (closest == null) closest = node;
                    else if (closest.Distance > node.Distance) closest = node;
                }
                return closest ?? throw new NotImplementedException();
            }
            DijkstraNode currentNode = startNode;
            while (grid.Cast<DijkstraNode>().Any(x => !x.Visited))
            {
                currentNode.Visited = true;
                List<DijkstraNode> neighbors = GetNeighborNodes(currentNode);
                foreach (DijkstraNode next in neighbors)
                {
                    int newDistance = currentNode.Distance + 1;
                    if (newDistance < next.Distance)
                    {
                        next.Distance = newDistance;
                        next.Previous = currentNode;
                    }
                }
                if (grid.Cast<DijkstraNode>().Any(x => !x.Visited)) currentNode = GetClosestUnvisitedNode();
            }
            int distance = grid.Cast<DijkstraNode>().Where(x => x.Elevation == 'a').OrderBy(x => x.Distance).First().Distance;
            Console.WriteLine($"Shortest scenic path has {distance} steps.");
        }

        PartOne(input);
        PartTwo(input);
    }

    static void DayThirteen(string input)
    {
        string[] pairs = input.Split("\n\n");
        int sumIdx = 0;
        List<Packet> packets = new();
        for (int i = 0; i < pairs.Length; i++)
        {
            string pair = pairs[i];
            if (string.IsNullOrWhiteSpace(pair)) continue;
            string[] lines = pair.Split('\n');
            Packet left = new(lines[0]);
            Packet right = new(lines[1]);
            if (left < right)
            {
                sumIdx += i + 1;
            }
            packets.Add(left);
            packets.Add(right);
        }
        Console.WriteLine($"Sum of indices: {sumIdx}");
        Packet divider0 = new("[[2]]"), divider1 = new("[[6]]");
        packets.Add(divider0);
        packets.Add(divider1);
        packets.Sort();
        int i0 = packets.IndexOf(divider0) + 1, i1 = packets.IndexOf(divider1) + 1;
        Console.WriteLine($"Index of first divider: {i0}, Index of second divider: {i1}");
        Console.WriteLine($"Decoder key: {i0 * i1}");
    }

    static void DayFourteen(string input)
    {
        static (int x, int y) ExtractPoint(string point)
        {
            string[] coords = point.Split(',');
            return (int.Parse(coords[0]), int.Parse(coords[1]));
        }
        TileType[,] grid = new TileType[600, 200];
        TileType[,] grid1 = new TileType[800, 200];
        int maxY = 0;
        string[] lines = input.Split('\n');
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            string[] points = line.Split(" -> ");
            (int x, int y) previous = ExtractPoint(points[0]);
            for (int i = 1; i < points.Length; i++)
            {
                (int x, int y) current = ExtractPoint(points[i]);
                for (int x = Math.Min(previous.x, current.x); x <= Math.Max(previous.x, current.x); x++)
                {
                    for (int y = Math.Min(previous.y, current.y); y <= Math.Max(previous.y, current.y); y++)
                    {
                        grid[x, y] = TileType.Rock;
                        grid1[x, y] = TileType.Rock;
                        if (y > maxY) maxY = y;
                    }
                }
                previous = (current.x, current.y);
            }
        }
        for (int x = 0; x < grid1.GetLength(0); x++)
        {
            grid1[x, maxY + 2] = TileType.Rock;
        }
        (int x, int y) sand = (500, 0);
        int cameToRest = 0;
        while (true)
        {
            if (sand.y + 1 >= grid.GetLength(1)) break;
            if (grid[sand.x, sand.y + 1] == TileType.Air)
            {
                sand.y++;
                continue;
            }
            else if (grid[sand.x - 1, sand.y + 1] == TileType.Air)
            {
                sand.x--;
                sand.y++;
                continue;
            }
            else if (grid[sand.x + 1, sand.y + 1] == TileType.Air)
            {
                sand.x++;
                sand.y++;
                continue;
            }
            else
            {
                grid[sand.x, sand.y] = TileType.Sand;
                cameToRest++;
                if (sand.x == 500 && sand.y == 0) break;
                sand = (500, 0);
                continue;
            }
        }
        Console.WriteLine($"{cameToRest} units of sand came to rest without floor.");
        int cameToRest1 = 0;
        sand = (500, 0);
        while (true)
        {
            if (sand.y + 1 >= grid1.GetLength(1)) break;
            if (grid1[sand.x, sand.y + 1] == TileType.Air)
            {
                sand.y++;
                continue;
            }
            else if (grid1[sand.x - 1, sand.y + 1] == TileType.Air)
            {
                sand.x--;
                sand.y++;
                continue;
            }
            else if (grid1[sand.x + 1, sand.y + 1] == TileType.Air)
            {
                sand.x++;
                sand.y++;
                continue;
            }
            else
            {
                grid1[sand.x, sand.y] = TileType.Sand;
                cameToRest1++;
                if (sand.x == 500 && sand.y == 0) break;
                sand = (500, 0);
                continue;
            }
        }
        Console.WriteLine($"{cameToRest1} units of sand came to rest with floor.");
    }

    static void DayFifteen(string input)
    {
        static int DistanceToSensor(int x, int y, SensorAndBeacon s)
        {
            return Math.Abs(s.SensorX - x) + Math.Abs(s.SensorY - y);
        }
        string[] lines = input.Split('\n');
        Regex sensorRegex = new(@"^Sensor at x=(?<xs>-?\d+), y=(?<ys>-?\d+): closest beacon is at x=(?<xb>-?\d+), y=(?<yb>-?\d+)");
        List<SensorAndBeacon> sensors = new();
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            Match match = sensorRegex.Match(line);
            sensors.Add(new(int.Parse(match.Groups["xs"].Value), int.Parse(match.Groups["ys"].Value), 
                int.Parse(match.Groups["xb"].Value), int.Parse(match.Groups["yb"].Value)));
        }
        int cantContainBeacon = 0;
        for (int x = -3000000; x < 10000000; x++)
        {
            if (sensors.Any(s => DistanceToSensor(x, 2000000, s) <= s.Distance) 
                && !sensors.Any(s => s.BeaconX == x && s.BeaconY == 2000000))
            {
                cantContainBeacon++;
            }
        }
        Console.WriteLine($"{cantContainBeacon} positions can't contain beacons in row y=2000000.");
        foreach (SensorAndBeacon sensor in sensors)
        {
            int distance = sensor.Distance + 1;
            for (int dx = -distance; dx <= distance; dx++)
            {
                int dy0 = distance - Math.Abs(dx);
                int dy1 = -(distance - Math.Abs(dx));
                int x = sensor.SensorX + dx;
                int y = sensor.SensorY + dy0;
                if (!sensors.Any(s => DistanceToSensor(x, y, s) <= s.Distance))
                {
                    Console.WriteLine($"Found beacon at {x}, {y}!");
                    Console.WriteLine($"Tuning frequency: {x * 4000000 + y}");
                    return;
                }
                y = sensor.SensorY + dy1;
                if (!sensors.Any(s => DistanceToSensor(x, y, s) <= s.Distance))
                {
                    Console.WriteLine($"Found beacon at {x}, {y}!");
                    Console.WriteLine($"Tuning frequency: {(long)x * 4000000 + y}");
                    return;
                }
            }
            Console.WriteLine("Finished another sensor!");
        }
    }

    static void DaySixteen(string input)
    {
        static long BestResult(Dictionary<string, Valve> valvesLeft, Valve current, int minutesLeft)
        {
            if (minutesLeft <= 0) return 0;
            long bestScore = current.FlowRate * minutesLeft;
            foreach (var kvp in current.Ways.Where(x => valvesLeft.ContainsKey(x.Key) && x.Value + 1 <= minutesLeft))
            {
                long score = current.FlowRate * minutesLeft
                    + BestResult(
                        new(valvesLeft.Where(x => x.Key != kvp.Key)),
                        valvesLeft[kvp.Key],
                        minutesLeft - kvp.Value - 1);
                if (score > bestScore) bestScore = score;
            }
            return bestScore;
        }
        static long BestWithElephant(Dictionary<string, Valve> valvesLeft, Valve current, int minutesLeft, Valve startValve, int startMinutes)
        {
            if (minutesLeft <= 0) return 0;
            long bestScore = current.FlowRate * minutesLeft + BestResult(valvesLeft, startValve, startMinutes);
            foreach (var kvp in current.Ways.Where(x => valvesLeft.ContainsKey(x.Key) && x.Value + 1 <= minutesLeft))
            {
                long score = current.FlowRate * minutesLeft
                    + BestWithElephant(
                        new(valvesLeft.Where(x => x.Key != kvp.Key)),
                        valvesLeft[kvp.Key],
                        minutesLeft - kvp.Value - 1,
                        startValve, startMinutes);
                if (score > bestScore) bestScore = score;
            }
            return bestScore;
        }
        string[] lines = input.Split('\n');
        Regex pipeRegex = new(@"Valve (?<valve>[A-Z]{2}) has flow rate=(?<flow>\d+); tunnels? leads? to valves? ((?<toValve>[A-Z]{2}),? ?)+");
        Dictionary<string, Valve> valves = new();
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            Match match = pipeRegex.Match(line);
            Valve valve = new(match.Groups["valve"].Value, int.Parse(match.Groups["flow"].Value),
                match.Groups["toValve"].Captures.Select(x => x.Value));
            valves.Add(valve.Id, valve);
        }
        foreach (Valve valve in valves.Values)
        {
            Dictionary<string, int> distance = new() { { valve.Id, 0 } };
            foreach (string s in valves.Keys) if (!distance.ContainsKey(s)) distance.Add(s, int.MaxValue);
            Dictionary<string, string> previous = new();
            Dictionary<string, bool> visited = new();
            while (valves.Keys.Any(x => !visited.ContainsKey(x) || !visited[x]))
            {
                string current = valves.Keys.Where(x => !visited.ContainsKey(x) || !visited[x]).OrderBy(x => distance[x]).First();
                visited[current] = true;
                foreach (string neighbor in valves[current].TunnelsToValves)
                {
                    int newDistance = distance[current] + 1;
                    if (newDistance < distance[neighbor])
                    {
                        distance[neighbor] = newDistance;
                        previous[neighbor] = current;
                    }
                }
            }
            valve.Ways = new(distance.Where(x => x.Key != valve.Id));
        }
        valves = new(valves.Where(x => x.Value.FlowRate > 0 || x.Value.Id == "AA"));
        foreach (Valve valve in valves.Values) valve.Ways = new(valve.Ways.Where(x => valves.ContainsKey(x.Key)));
        int minutesLeft = 30;
        string position = "AA";
        long bestResult = BestResult(new(valves.Where(x => x.Key != position)), valves[position], minutesLeft);
        Console.WriteLine($"Best result alone: {bestResult}");
        minutesLeft = 26;
        long bestWithElephant = BestWithElephant(new(valves.Where(x => x.Key != position)), valves[position], minutesLeft, valves[position], minutesLeft);
        Console.WriteLine($"Best result with elephant: {bestWithElephant}");
    }

    static void DaySeventeen(string input)
    {
        List<bool> directions = input.Split('\n')[0].Select(c =>
        {
            return c switch
            {
                '>' => true,
                '<' => false,
                _ => throw new NotImplementedException(),
            };
        }).ToList();
        List<bool[][]> rocks = new()
        {
            new bool[][] { new bool[]  { true, true, true, true } },
            new bool[][] {
                new bool[]  { false, true, false },
                new bool[]  { true, true, true },
                new bool[]  { false, true, false }
            },
            new bool[][]
            {
                new bool[]  { true, true, true },
                new bool[]  { false, false, true },
                new bool[]  { false, false, true }  // rocks are scanned from the bottom up
            },
            new bool[][]
            {
                new bool[]  { true },
                new bool[]  { true },
                new bool[]  { true },
                new bool[]  { true }
            },
            new bool[][]
            {
                new bool[]  { true, true },
                new bool[]  { true, true }
            }
        };
        static void MoveRock(bool isDirectionRight, List<bool[]> chamber, FallingRock currentRock)
        {
            List<bool[]> relevantRows = new();
            for (int i = 0; i < currentRock.Shape.Length; i++)
            {
                int rowIndex = currentRock.Row + i;
                while (chamber.Count <= rowIndex)
                {
                    chamber.Add(new bool[7]);
                }
                relevantRows.Add(chamber[rowIndex]);
            }
            int shift = currentRock.Column + (isDirectionRight ? 1 : -1);
            if (shift < 0 || shift + currentRock.Shape[0].Length > 7)
            {
                return;
            }
            for (int i = 0; i < relevantRows.Count; i++)
            {
                bool[] rock = currentRock.Shape[i];
                bool[] row = relevantRows[i];
                for (int j = shift; j < shift + rock.Length; j++)
                {
                    if (row[j] && rock[j - shift]) return;
                }
            }
            currentRock.Column += (isDirectionRight ? 1 : -1);
        }
        static bool RockFall(List<bool[]> chamber, FallingRock currentRock)
        {
            List<bool[]> relevantRows = new();
            for (int i = 0; i < currentRock.Shape.Length; i++)
            {
                int rowIndex = currentRock.Row + i - 1;
                while (chamber.Count <= rowIndex)
                {
                    chamber.Add(new bool[7]);
                }
                relevantRows.Add(chamber[rowIndex]);
            }
            for (int i = 0; i < relevantRows.Count; i++)
            {
                bool[] rock = currentRock.Shape[i];
                bool[] row = relevantRows[i];
                for (int j = currentRock.Column; j < currentRock.Column + rock.Length; j++)
                {
                    if (row[j] && rock[j - currentRock.Column]) return false;
                }
            }
            currentRock.Row--;
            return true;
        }
        static void RockLand(List<bool[]> chamber, FallingRock currentRock)
        {
            for (int i = 0; i < currentRock.Shape.Length; i++)
            {
                bool[] row = chamber[currentRock.Row + i];
                for (int j = 0; j < currentRock.Shape[i].Length; j++)
                {
                    if (currentRock.Shape[i][j]) row[currentRock.Column + j] = true;
                }
            }
        }
        bool print = false;
        void Print(List<bool[]> chamber, FallingRock? currentRock)
        {
            if (!print) return;
            Console.SetCursorPosition(0, 0);
            for (int i = chamber.Count - 1; i >= 0; i--)
            {
                Console.Write("|");
                for (int j = 0; j < 7; j++)
                {
                    if (chamber[i][j]) Console.Write('#');
                    else if (currentRock?.GetAt(i, j) != null)
                    {
                        bool? rock = currentRock.GetAt(i, j);
                        Console.Write(rock == true ? '@' : ' ');
                    }
                    else Console.Write(' ');
                }
                Console.WriteLine("|");
            }
        }
        static bool SearchPattern(List<bool[]> chamber)
        {
            if (chamber.Skip(1).Count() % 2 != 0) return false;
            int length = chamber.Skip(1).Count() / 2;
            IEnumerable<bool[]> maybePattern = chamber.Skip(1).Take(length);
            bool isPattern = true;
            for (int i = 0; i < length; i++)
            {
                if (!maybePattern.ElementAt(i).ContentEquals(chamber.Skip(1 + length).ElementAt(i)))
                {
                    isPattern = false;
                    break;
                }
            }
            return isPattern;
        }
        List<bool[]> chamber = new()
        {
            new bool[] { true, true, true, true, true, true, true }   // Floor
        };
        int rockIdx = 0;
        long tick = 0;
        FallingRock? currentRock = null;
        int towerHeight2022 = -1;
        int patternHeight;
        int patternRocks;
        while (true)
        {
            if (currentRock == null)
            {
                bool[][] shape = rocks[rockIdx % 5].DeepCopy();
                currentRock = new(shape, 2, chamber.Where(x => x.Any(y => y)).Count() + 3);
            }
            // Push rock
            bool isPushedRight = directions[(int)(tick++ % directions.Count)];
            MoveRock(isPushedRight, chamber, currentRock);
            Print(chamber, currentRock);
            // Rock fall
            if (!RockFall(chamber, currentRock))
            {
                RockLand(chamber, currentRock);
                currentRock = null;
                Print(chamber, currentRock);
                rockIdx++;
                if (rockIdx % 10 == 0 && SearchPattern(chamber))
                {
                    patternHeight = chamber.Skip(1).Count() / 2;
                    patternRocks = rockIdx / 2;
                    break;
                }
            }
            Print(chamber, currentRock);
            if (rockIdx == 2022)
            {
                towerHeight2022 = chamber.Skip(1).Where(x => x.Any(y => y)).Count();
                Console.WriteLine($"The tower will be {towerHeight2022} high at 2022 rocks.");
            }
        }
        long patternReps = 1000000000000 / patternRocks;
        long rocksLeft = 1000000000000 % patternRocks;
        rockIdx = 0;
        int dChamberHeight = chamber.Where(x => x.Any(y => y)).Count();
        while (rockIdx < rocksLeft)
        {
            if (currentRock == null)
            {
                bool[][] shape = rocks[rockIdx % 5].DeepCopy();
                currentRock = new(shape, 2, chamber.Where(x => x.Any(y => y)).Count() + 3);
            }
            // Push rock
            bool isPushedRight = directions[(int)(tick++ % directions.Count)];
            MoveRock(isPushedRight, chamber, currentRock);
            // Rock fall
            if (!RockFall(chamber, currentRock))
            {
                RockLand(chamber, currentRock);
                currentRock = null;
                rockIdx++;
            }
        }
        long height = patternReps * patternHeight + chamber.Where(x => x.Any(y => y)).Count() - dChamberHeight;
        Console.WriteLine($"Height after the elephants have probably gone extinct already: {height}");
    }

    static void DayEightteen(string input)
    {
        string[] lines = input.Split('\n');
        bool?[,,] droplet = new bool?[25, 25, 25];
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            string[] split = line.Split(',');
            int x = int.Parse(split[0]) + 1;
            int y = int.Parse(split[1]) + 1;
            int z = int.Parse(split[2]) + 1;
            droplet[x, y, z] = true;
        }
        long surface = 0;
        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 25; y++)
            {
                for (int z = 0; z < 25; z++)
                {
                    if (droplet[x, y, z] != true) continue;
                    if (x > 24 || (droplet[x + 1, y, z] != true)) surface++;
                    if (x == 0 || (droplet[x - 1, y, z] != true)) surface++;
                    if (y > 24 || (droplet[x, y + 1, z] != true)) surface++;
                    if (y == 0 || (droplet[x, y - 1, z] != true)) surface++;
                    if (z > 24 || (droplet[x, y, z + 1] != true)) surface++;
                    if (z == 0 || (droplet[x, y, z - 1] != true)) surface++;
                }
            }
        }
        Console.WriteLine($"Surface of droplet: {surface}");
        // simulate steam using false
        if (droplet[0, 0, 0] == true || droplet[0, 0, 24] == true || droplet[0, 24, 0] == true || droplet[24, 0, 0] == true
            || droplet[0, 24, 24] == true || droplet[24, 0, 24] == true || droplet[24, 24, 0] == true || droplet[24, 24, 24] == true)
        {
            throw new NotImplementedException("Need to choose different starting points for steam simulation!");
        }
        droplet[0, 0, 0] = false;
        droplet[0, 0, 24] = false;
        droplet[0, 24, 0] = false;
        droplet[24, 0, 0] = false;
        droplet[0, 24, 24] = false;
        droplet[24, 0, 24] = false;
        droplet[24, 24, 0] = false;
        droplet[24, 24, 24] = false;
        bool steamExpanded = true;
        while (steamExpanded)
        {
            steamExpanded = false;
            for (int x = 0; x < 25; x++)
            {
                for (int y = 0; y < 25; y++)
                {
                    for (int z = 0; z < 25; z++)
                    {
                        if (droplet[x, y, z] != false) continue;
                        (int x, int y, int z)[] coords =
                        {
                            (x + 1, y, z),
                            (x - 1, y, z),
                            (x, y + 1, z),
                            (x, y - 1, z),
                            (x, y, z + 1),
                            (x, y, z - 1)
                        };
                        foreach (var coord in coords)
                        {
                            if (coord.x < 0 || coord.y < 0 || coord.z < 0
                                || coord.x > 24 || coord.y > 24 || coord.z > 24) continue;
                            if (droplet[coord.x, coord.y, coord.z] == null)
                            {
                                droplet[coord.x, coord.y, coord.z] = false;
                                steamExpanded = true;
                            }
                        }
                        
                    }
                }
            }
        }
        // now count surface area touching steam again
        int surfaceTouchingSteam = 0;
        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 25; y++)
            {
                for (int z = 0; z < 25; z++)
                {
                    if (droplet[x, y, z] != true) continue;
                    if (x > 24 || (droplet[x + 1, y, z] == false)) surfaceTouchingSteam++;
                    if (x == 0 || (droplet[x - 1, y, z] == false)) surfaceTouchingSteam++;
                    if (y > 24 || (droplet[x, y + 1, z] == false)) surfaceTouchingSteam++;
                    if (y == 0 || (droplet[x, y - 1, z] == false)) surfaceTouchingSteam++;
                    if (z > 24 || (droplet[x, y, z + 1] == false)) surfaceTouchingSteam++;
                    if (z == 0 || (droplet[x, y, z - 1] == false)) surfaceTouchingSteam++;
                }
            }
        }
        Console.WriteLine($"Surface of droplet touching steam: {surfaceTouchingSteam}");
    }

    static void DayNineteen(string input)
    {
        static int MostGeodes(Blueprint blueprint, int minutesLeft, int oreRobots, int clayRobots, int obsidianRobots, int geodeRobots, int ore, int clay, int obsidian, int geodes)
        {
            if (minutesLeft == 0) return geodes;
            int bestOutcome = geodes;
            // when could we build the next geode robot?
            if (obsidianRobots > 0)
            {
                int oreMissing = blueprint.GeodeRobotOreCost - ore;
                int obsidianMissing = blueprint.GeodeRobotObsidianCost - obsidian;
                int minutesToProduction = 0;
                while (oreMissing > 0 || obsidianMissing > 0)
                {
                    minutesToProduction++;
                    oreMissing -= oreRobots;
                    obsidianMissing -= obsidianRobots;
                }
                int minutesSpent = minutesToProduction + 1;
                if (minutesSpent <= minutesLeft)
                {
                    int outcome = MostGeodes(blueprint, minutesLeft - minutesSpent, 
                        oreRobots, clayRobots, obsidianRobots, geodeRobots + 1,
                        ore + oreRobots * minutesSpent - blueprint.GeodeRobotOreCost,
                        clay + clayRobots * minutesSpent,
                        obsidian + obsidianRobots * minutesSpent - blueprint.GeodeRobotObsidianCost,
                        geodes + geodeRobots * minutesSpent);
                    if (outcome > bestOutcome) bestOutcome = outcome;
                }
            }
            // the next obsidian robot?
            if (clayRobots > 0 && obsidianRobots < blueprint.GeodeRobotObsidianCost)
            {
                int oreMissing = blueprint.ObsidianRobotOreCost - ore;
                int clayMissing = blueprint.ObsidianRobotClayCost - clay;
                int minutesToProduction = 0;
                while (oreMissing > 0 || clayMissing > 0)
                {
                    minutesToProduction++;
                    oreMissing -= oreRobots;
                    clayMissing -= clayRobots;
                }
                int minutesSpent = minutesToProduction + 1;
                if (minutesSpent <= minutesLeft)
                {
                    int outcome = MostGeodes(blueprint, minutesLeft - minutesSpent,
                        oreRobots, clayRobots, obsidianRobots + 1, geodeRobots,
                        ore + oreRobots * minutesSpent - blueprint.ObsidianRobotOreCost,
                        clay + clayRobots * minutesSpent - blueprint.ObsidianRobotClayCost,
                        obsidian + obsidianRobots * minutesSpent,
                        geodes + geodeRobots * minutesSpent);
                    if (outcome > bestOutcome) bestOutcome = outcome;
                }
            }
            // the next clay robot?
            if (clayRobots < blueprint.ObsidianRobotClayCost)
            {
                int oreMissing = blueprint.ClayRobotCost - ore;
                int minutesToProduction = 0;
                while (oreMissing > 0)
                {
                    minutesToProduction++;
                    oreMissing -= oreRobots;
                }
                int minutesSpent = minutesToProduction + 1;
                if (minutesSpent <= minutesLeft)
                {
                    int outcome = MostGeodes(blueprint, minutesLeft - minutesSpent,
                        oreRobots, clayRobots + 1, obsidianRobots, geodeRobots,
                        ore + oreRobots * minutesSpent - blueprint.ClayRobotCost,
                        clay + clayRobots * minutesSpent,
                        obsidian + obsidianRobots * minutesSpent,
                        geodes + geodeRobots * minutesSpent);
                    if (outcome > bestOutcome) bestOutcome = outcome;
                }
            }
            // the next ore robot?
            if (oreRobots < Math.Max(
                Math.Max(blueprint.OreRobotCost, blueprint.ClayRobotCost), 
                Math.Max(blueprint.ObsidianRobotOreCost, blueprint.GeodeRobotOreCost)
                ))
            {
                int oreMissing = blueprint.OreRobotCost - ore;
                int minutesToProduction = 0;
                while (oreMissing > 0)
                {
                    minutesToProduction++;
                    oreMissing -= oreRobots;
                }
                int minutesSpent = minutesToProduction + 1;
                if (minutesSpent <= minutesLeft)
                {
                    int outcome = MostGeodes(blueprint, minutesLeft - minutesSpent,
                        oreRobots + 1, clayRobots, obsidianRobots, geodeRobots,
                        ore + oreRobots * minutesSpent - blueprint.OreRobotCost,
                        clay + clayRobots * minutesSpent,
                        obsidian + obsidianRobots * minutesSpent,
                        geodes + geodeRobots * minutesSpent);
                    if (outcome > bestOutcome) bestOutcome = outcome;
                }
            }
            // what if we just wait until the end?
            {
                int outcome = geodes + geodeRobots * minutesLeft;
                if (outcome > bestOutcome) bestOutcome = outcome;
            }
            return bestOutcome;
        }
        string[] lines = input.Split('\n');
        List<Blueprint> blueprints = new();
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            blueprints.Add(new(line));
        }
        foreach (Blueprint blueprint in blueprints)
        {
            int mostGeodes = MostGeodes(blueprint, minutesLeft: 24, 
                oreRobots: 1, clayRobots: 0, obsidianRobots: 0, geodeRobots: 0, 
                ore: 0, clay: 0, obsidian: 0, geodes: 0);
            blueprint.QualityLevel = mostGeodes * blueprint.Id;
            Console.WriteLine($"Blueprint {blueprint.Id} has quality level {blueprint.QualityLevel} with {mostGeodes} geodes.");
        }
        Console.WriteLine($"The sum of all quality levels is {blueprints.Select(x => x.QualityLevel ?? 0).Sum()}");
        long product = 1;
        foreach (Blueprint blueprint in blueprints.Take(3))
        {
            int mostGeodes = MostGeodes(blueprint, minutesLeft: 32,
                oreRobots: 1, clayRobots: 0, obsidianRobots: 0, geodeRobots: 0,
                ore: 0, clay: 0, obsidian: 0, geodes: 0);
            Console.WriteLine($"Blueprint {blueprint.Id} can produce {mostGeodes} geodes in 32 minutes.");
            product *= mostGeodes;
        }
        Console.WriteLine($"The product of the first three blueprints in 32 minutes is {product}.");
    }
}