using System.Diagnostics;
using System.Text;

namespace Day10;

class Program
{
    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];

        StringBuilder input;
        using (var streamReader = new StreamReader(inputFileName))
            input = new StringBuilder(streamReader.ReadToEnd());

        var part1Iterations = 40;
        var part2Iterations = 50;
        var part1Length = 0;
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        for (var i = 0; i < part2Iterations; ++i)
        {
            if (i == part1Iterations)
                part1Length = input.Length;

            var processedInput = new StringBuilder();

            while (input.Length > 0)
            {
                var token = input[0];
                var repetitionCount = 0;

                while (input.Length > 0 && input[0] == token)
                {
                    input.Remove(0, 1);
                    ++repetitionCount;
                }

                processedInput.Append($"{repetitionCount}{token}");
            }

            input = processedInput;
            Console.WriteLine($"Length after {i + 1} iterations is {input.Length}. Calculation takes {stopWatch.Elapsed} time");
        }

        stopWatch.Stop();

        Console.WriteLine($"\nPart 1. Length after {part1Iterations} iterations is {part1Length}");
        Console.WriteLine($"Part 2. Length after {part2Iterations} iterations is {input.Length}");
    }
}
