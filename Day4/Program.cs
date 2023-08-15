using System.Security.Cryptography;
using System.Text;

namespace Day4;

class Program
{
    private const string _fiveZeroes = "00000";
    private const string _sixZeroes = "000000";

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        string input;

        using (var streamReader = new StreamReader(inputFileName))
            input = streamReader.ReadToEnd();

        var fiveZeroesGenerated = -1;
        var sixZeroesGenerated = -1;

        using (var md5 = MD5.Create())
        {
            var i = 1;

            while (true)
            {
                var inputBytes = Encoding.UTF8.GetBytes($"{input}{i}");
                var hashBytes = md5.ComputeHash(inputBytes);
                var hash = Convert.ToHexString(hashBytes);

                if (hash.StartsWith(_fiveZeroes) && fiveZeroesGenerated == -1)
                    fiveZeroesGenerated = i;
                
                if (hash.StartsWith(_sixZeroes) && sixZeroesGenerated == -1)
                    sixZeroesGenerated = i;

                if (fiveZeroesGenerated != -1 && sixZeroesGenerated != -1)
                    break;

                ++i;
            }
        }

        Console.WriteLine($"Part 1. First number which generates five zeroes at start md5 is: {fiveZeroesGenerated}");
        Console.WriteLine($"Part 2. First number which generates six zeroes at start md5 is: {sixZeroesGenerated}");
    }
}
