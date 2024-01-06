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

        Console.WriteLine(string.Join(", ", inputNumbers) + $"\n{destRow}; {destCol}");
    }
}
