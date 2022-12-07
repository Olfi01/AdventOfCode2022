﻿using AdventOfCode;
using AdventOfCode.DaySeven;
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
        switch(day)
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
            switch((opponent, me))
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
            switch(me)
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
            } else if (line.StartsWith("$ ls"))
            {
                continue;
            } else if (line.StartsWith("dir "))
            {
                string path = line[4..];
                if (!currentDir.Content.ContainsKey(path))
                    currentDir.Content[path] = new(new Directory(currentDir));
            } else
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
}