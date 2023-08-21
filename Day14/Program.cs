namespace Day14;

class Program
{
    private const string _middleDelimiter = " seconds, but then must rest for ";
    private const string _canFlyDelimiter = " can fly ";
    private const string _speedDurationDelimiter = " km/s for ";
    private const char _spaceDelimiter = ' ';

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        using var streamReader = new StreamReader(inputFileName);

        string? input;
        while ((input = streamReader.ReadLine()) != null)
        {
            var parts = input.Split(_middleDelimiter);
            var splitLeftSide = parts[0].Split(_canFlyDelimiter);
            var name = splitLeftSide[0];
            var speedDurationData = splitLeftSide[1].Split(_speedDurationDelimiter);
            var speed = int.Parse(speedDurationData[0]);
            var duration = int.Parse(speedDurationData[1]);
            var restTime = int.Parse(parts[1].Split(_spaceDelimiter)[0]);
        }
    }
}
