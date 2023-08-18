using System.Text;
using System.Text.RegularExpressions;

namespace Day8;

class Program
{
    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        string[] inputData;

        using (var streamReader = new StreamReader(inputFileName))
            inputData = streamReader.ReadToEnd().Split("\r\n");

        var charsInCode = 0;
        var charsInMemory = 0;
        var encodedCharsInCode = 0;
        StringBuilder stringBuilder;

        foreach (var input in inputData)
        {
            charsInCode += input.Length;
            charsInMemory += Regex.Unescape(input[1..^1]).Length;
            encodedCharsInCode += Encode(input).Length;
        }

        Console.WriteLine($"Part 1. Difference is: {charsInCode - charsInMemory} characters");
        Console.WriteLine($"Part 2. Now difference is: {encodedCharsInCode - charsInCode} characters");

        string Encode(string str)
        {
            stringBuilder = new StringBuilder("\"");

            foreach (var character in str)
            {
                switch (character)
                {
                    case '\"':
                    case '\\':
                        stringBuilder.Append('\\');
                        break;
                }

                stringBuilder.Append(character);
            }

            stringBuilder.Append('\"');
            return stringBuilder.ToString();
        }
    }
}
