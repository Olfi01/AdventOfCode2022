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

    private static void UpdateTop3(int currentElf, ref int maxCalories, ref int secondMostCalories, ref int thirdMostCalories)
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
}