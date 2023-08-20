using System.Text;
using System.Text.RegularExpressions;

namespace Day12;

partial class Program
{
    [GeneratedRegex(@"-?\d+")]
    private static partial Regex NumbersRegex();

    private const string _redProperty = "\"red\"";
    private static readonly int _redPropertyLength = _redProperty.Length;

    private const char _openArrChar = '[';
    private const char _closeArrChar = ']';
    private const char _openObjChar = '{';
    private const char _closeObjChar = '}';

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];

        StringBuilder data;
        using (var streamReader = new StreamReader(inputFileName))
            data = new StringBuilder(streamReader.ReadToEnd());

        var strData = data.ToString();
        var sumNumbers = SumAllNumbers(strData);

        Console.WriteLine($"Part 1. Total sum of numbers is: {sumNumbers}");

        int redIndex;
        while ((redIndex = strData.IndexOf(_redProperty)) != -1)
        {
            var unclosedArrays = 0;
            var unclosedObjects = 0;
            int startObjectIndex = -1;
            var isInArray = false;

            for (var i = redIndex - 1; i >= 0; --i)
            {
                var currentChar = data[i];

                if (currentChar == _closeArrChar)
                    ++unclosedArrays;
                else if (currentChar == _openArrChar)
                {
                    if (unclosedArrays == 0)
                    {
                        isInArray = true;
                        break;
                    }
                    else
                        --unclosedArrays;
                }
                else if (currentChar == _closeObjChar)
                    ++unclosedObjects;
                else if (currentChar == _openObjChar)
                {
                    if (unclosedObjects == 0)
                    {
                        startObjectIndex = i;
                        break;
                    }
                    else
                        --unclosedObjects;
                }
            }

            if (isInArray)
            {
                data.Remove(redIndex, _redPropertyLength);
                strData = data.ToString();
                continue;
            }

            var length = data.Length;
            var isEndObjectFound = false;

            for (var i = redIndex + _redPropertyLength; i < length; ++i)
            {
                var currentChar = data[i];

                if (currentChar == _openObjChar)
                    ++unclosedObjects;
                else if (currentChar == _closeObjChar)
                {
                    if (unclosedObjects == 0)
                    {
                        isEndObjectFound = true;
                        data.Remove(startObjectIndex, i - startObjectIndex + 1);
                        strData = data.ToString();
                        break;
                    }
                    else
                        --unclosedObjects;
                }
            }

            if (!isEndObjectFound)
                throw new ArgumentException("Broken input, found start of object, but there is no end, check input data");
        }

        sumNumbers = SumAllNumbers(strData);
        Console.WriteLine($"Part 2. After removing all {_redProperty} properties, total sum of numbers is: {sumNumbers}");

        static int SumAllNumbers(string data)
        {
            var sumNumbers = 0;

            foreach (var numberMatch in NumbersRegex().Matches(data).Cast<Match>())
                sumNumbers += int.Parse(numberMatch.Value);

            return sumNumbers;
        }
    }
}
