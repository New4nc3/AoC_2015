using System.Text.RegularExpressions;

namespace Day25;

partial class Program
{
    private const string _digitsPattern = @"\d+";

    [GeneratedRegex(_digitsPattern)]
    private static partial Regex NumbersRegex();

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        string? rawData;

        using (var streamReader = new StreamReader(inputFileName))
            rawData = streamReader.ReadLine();

        if (string.IsNullOrEmpty(rawData))
            throw new ArgumentException("Check input file and try again");

        var inputNumbers = new int[2];
        var i = 0;

        foreach (Match match in NumbersRegex().Matches(rawData).Cast<Match>())
            inputNumbers[i++] = Convert.ToInt32(match.Value);

        var destRow = inputNumbers[0] - 1;
        var destCol = inputNumbers[1] - 1;
        var diagonal = 0;
        var isTest = inputFileName == "test.txt";
        long currentValue = isTest ? 1 : 20151125;
        long multiplier = 252533;
        long divider = 33554393;
        int tempRowIdx = 0, tempColIdx = 0;

        while (tempRowIdx != destRow || tempColIdx != destCol)
        {
            ++diagonal;
            tempRowIdx = diagonal;
            tempColIdx = 0;

            do
            {
                currentValue = isTest ? currentValue + 1 : currentValue * multiplier % divider;

                if (tempRowIdx == destRow && tempColIdx == destCol)
                    break;

                if (tempRowIdx - 1 >= 0)
                {
                    --tempRowIdx;
                    ++tempColIdx;
                }
                else break;
            } while (true);
        }

        Console.WriteLine($"Part 1. Value on row {inputNumbers[0]} and column {inputNumbers[1]} was found on diagonal #{diagonal + 1} and equals {currentValue}");
    }
}
