using System.Text;

namespace Day11;

class Program
{
    private static readonly char[] _forbiddenSymbols = new char[] { 'i', 'o', 'l' };
    private const char _outOfRangeSymbol = (char)('z' + 1);

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];

        StringBuilder input;
        using (var streamReader = new StreamReader(inputFileName))
            input = new StringBuilder(streamReader.ReadToEnd());

        var firstValidPassword = string.Empty;
        string secondValidPassword;
        var candidate = input.ToString();

        while (true)
        {
            if (IsValidPassword(candidate))
            {
                if (string.IsNullOrEmpty(firstValidPassword))
                {
                    firstValidPassword = candidate;
                }
                else
                {
                    secondValidPassword = candidate;
                    break;
                }
            }

            ++input[^1];
            candidate = input.ToString();
            int outOfRangeIndex;

            while ((outOfRangeIndex = candidate.IndexOf(_outOfRangeSymbol)) != -1)
            {
                input[outOfRangeIndex] = 'a';

                var previousIndex = outOfRangeIndex - 1;
                if (previousIndex >= 0)
                    ++input[previousIndex];
                else
                    input.Insert(0, 'a');

                candidate = input.ToString();
            }

            if (ContainsForbiddenChar(candidate))
            {
                var forbiddenIndexes = new List<int>();

                foreach (var forbiddenChar in _forbiddenSymbols)
                {
                    var forbiddenIndex = candidate.IndexOf(forbiddenChar);
                    if (forbiddenIndex != -1)
                        forbiddenIndexes.Add(forbiddenIndex);
                }

                var minForbiddenIndex = forbiddenIndexes.Min();
                ++input[minForbiddenIndex];

                var length = candidate.Length;

                for (var i = minForbiddenIndex + 1; i < length; ++i)
                    input[i] = 'a';

                candidate = input.ToString();
            }
        }

        Console.WriteLine($"Part 1. First valid password is \"{firstValidPassword}\"");
        Console.WriteLine($"Part 2. Second valid password is \"{secondValidPassword}\"");

        static bool ContainsForbiddenChar(string candidate)
        {
            foreach (var forbiddenChar in _forbiddenSymbols)
                if (candidate.Contains(forbiddenChar))
                    return true;

            return false;
        }

        static bool IsValidPassword(string candidate)
        {
            if (ContainsForbiddenChar(candidate))
                return false;

            var length = candidate.Length;
            var beforeBeforeLast = length - 2;
            var hasThreeIncreasingLetters = false;

            for (var i = 0; i < beforeBeforeLast; ++i)
            {
                var middle = candidate[i + 1];

                if ((candidate[i] + 1 == middle) && (candidate[i + 2] - 1) == middle)
                {
                    hasThreeIncreasingLetters = true;
                    break;
                }
            }

            if (hasThreeIncreasingLetters)
            {
                var beforeLengthMinus3 = length - 3;
                var hasFirstRepetitive = false;
                char firstRepetitiveChar = '-';
                int i;

                for (i = 0; i < beforeLengthMinus3; ++i)
                    if (candidate[i] == candidate[i + 1])
                    {
                        firstRepetitiveChar = candidate[i];
                        hasFirstRepetitive = true;
                        break;
                    }

                if (hasFirstRepetitive)
                {
                    var beforeLast = length - 1;
                    
                    for (var j = i + 2; j < beforeLast; ++j)
                        if (candidate[j] == candidate[j + 1] && candidate[j] != firstRepetitiveChar)
                            return true;
                }
            }

            return false;
        }
    }
}
