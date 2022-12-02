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
}