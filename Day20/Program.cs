using System.Diagnostics;

namespace Day20;

class Program
{
    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        int presentsInput;

        using (var streamReader = new StreamReader(inputFileName))
            presentsInput = int.Parse(streamReader.ReadToEnd());

        int minHousePart1 = -1, minHousePart2 = -1;
        var tempHouseNumber = 1;
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        while (minHousePart1 == -1 || minHousePart2 == -1)
        {
            var maxDivider = tempHouseNumber / 2;
            int tempSumPart1 = tempHouseNumber * 10, tempSumPart2 = tempHouseNumber * 11;

            for (var i = 1; i <= maxDivider; ++i)
            {
                if (tempHouseNumber % i == 0)
                {
                    tempSumPart1 += i * 10;

                    if (i * 50 >= tempHouseNumber)
                        tempSumPart2 += i * 11;
                }
            }

            if (tempSumPart1 >= presentsInput && minHousePart1 == -1)
                minHousePart1 = tempHouseNumber;

            if (tempSumPart2 >= presentsInput && minHousePart2 == -1)
                minHousePart2 = tempHouseNumber;

            if (tempHouseNumber % 10000 == 0)
            {
                var found = minHousePart1 != -1 ? $" Found Part 1: ({minHousePart1})" : string.Empty;
                Console.Clear();
                Console.WriteLine($"Checked: {tempHouseNumber}.{found} Elapsed: {stopWatch.Elapsed}");
            }

            ++tempHouseNumber;
        }

        stopWatch.Stop();

        Console.Clear();
        Console.WriteLine($"Part 1. Lowest house number which got {presentsInput} presents is: {minHousePart1}");
        Console.WriteLine($"Part 2. Now, the lowest house number is: {minHousePart2}");
        Console.WriteLine($"Total elapsed: {stopWatch.Elapsed}");
    }
}
