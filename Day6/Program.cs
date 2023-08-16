namespace Day6;

class Program
{
    private const string _turnOn = "turn on ";
    private const string _turnOff = "turn off ";
    private const string _toggle = "toggle ";
    private const string _throughDelimiter = " through ";
    private const char _coordsDelimiter = ',';

    private static readonly int _turnOnLength = _turnOn.Length;
    private static readonly int _toggleLength = _toggle.Length;
    private static readonly int _turnOffLength = _turnOff.Length;

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        using var streamReader = new StreamReader(inputFileName);

        var lights1 = new bool[1000, 1000];
        var lights2 = new int[1000, 1000];

        string? current;
        (int, int, int, int) coords;
        Action<int, int> command1;
        Action<int, int> command2;

        while ((current = streamReader.ReadLine()) != null)
        {
            if (current.StartsWith(_turnOn))
            {
                coords = ParseCoords(current.Remove(0, _turnOn.Length));
                command1 = TurnOn1;
                command2 = TurnOn2;
            }
            else if (current.StartsWith(_turnOff))
            {
                coords = ParseCoords(current.Remove(0, _turnOffLength));
                command1 = TurnOff1;
                command2 = TurnOff2;
            }
            else if (current.StartsWith(_toggle))
            {
                coords = ParseCoords(current.Remove(0, _toggleLength));
                command1 = Toggle1;
                command2 = Toggle2;
            }
            else
            {
                throw new ArgumentException($"Unknown command \"{current}\". Check input data");
            }

            for (int i = coords.Item1; i <= coords.Item3; ++i)
            {
                for (int j = coords.Item2; j <= coords.Item4; ++j)
                {
                    command1(i, j);
                    command2(i, j);
                }
            }
        }

        var lightsCount = 0;
        var totalBrightness = 0;

        for (var i = 0; i < 1000; ++i)
        {
            for (var j = 0; j < 1000; ++j)
            {
                if (lights1[i, j])
                    ++lightsCount;

                totalBrightness += lights2[i, j];
            }
        }

        Console.WriteLine($"Part 1. Lights count is: {lightsCount}");
        Console.WriteLine($"Part 2. Total brightness is: {totalBrightness}");

        (int, int, int, int) ParseCoords(string input)
        {
            var separated = input.Split(_throughDelimiter);
            var ij = separated[0].Split(_coordsDelimiter);
            var counts = separated[1].Split(_coordsDelimiter);

            return (int.Parse(ij[0]), int.Parse(ij[1]), int.Parse(counts[0]), int.Parse(counts[1]));
        }

        void TurnOn1(int i, int j) => lights1[i, j] = true;
        void TurnOff1(int i, int j) => lights1[i, j] = false;
        void Toggle1(int i, int j) => lights1[i, j] = !lights1[i, j];

        void TurnOn2(int i, int j) => ++lights2[i, j];
        void TurnOff2(int i, int j)
        {
            int temp = lights2[i, j];

            if (temp > 0)
                --lights2[i, j];
        }
        void Toggle2(int i, int j) => lights2[i, j] += 2;
    }
}
