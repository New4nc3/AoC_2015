namespace Day5;

class Program
{
    private static readonly List<string> _forbiddenStrings = new() { "ab", "cd", "pq", "xy", };
    private static readonly List<char> _requiredVowels = new() { 'a', 'e', 'i', 'o', 'u', };

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        using var streamReader = new StreamReader(inputFileName);

        var niceStrCount1 = 0;
        var niceStrCount2 = 0;
        string? current;

        while ((current = streamReader.ReadLine()) != null)
        {
            // Checking rules for part 1
            if (!_forbiddenStrings.Any(x => current.Contains(x)))
            {
                var hasThreeVowels = false;
                var sumVowels = 0;

                foreach (var vowel in _requiredVowels)
                {
                    sumVowels += current.Count(x => x == vowel);

                    if (sumVowels >= 3)
                    {
                        hasThreeVowels = true;
                        break;
                    }
                }

                if (hasThreeVowels)
                {
                    var beforeLast = current.Length - 1;

                    for (var i = 0; i < beforeLast; ++i)
                    {
                        if (current[i] == current[i + 1])
                        {
                            ++niceStrCount1;
                            break;
                        }
                    }
                }
            }

            // Checking for part 2
            var beforeBeforeLast = current.Length - 2;
            var containsRepeatLetter = false;

            for (var i = 0; i < beforeBeforeLast; ++i)
            {
                if (current[i] == current[i + 2])
                {
                    containsRepeatLetter = true;
                    break;
                }
            }

            if (containsRepeatLetter)
            {
                for (var i = 0; i < beforeBeforeLast; ++i)
                {
                    var candidate = current.Substring(i, 2);
                    var stringToCheck = current.Substring(i + 2);

                    if (stringToCheck.Contains(candidate))
                    {
                        ++niceStrCount2;
                        break;
                    }
                }
            }
        }

        Console.WriteLine($"Part 1. Count of nice strings is: {niceStrCount1}");
        Console.WriteLine($"Part 2. Count of nice strings is: {niceStrCount2}");
    }
}
