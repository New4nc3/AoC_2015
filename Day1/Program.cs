namespace Day1;

class Program
{
    private const char _upFloor = '(';
    private const char _downFloor = ')';

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        string data;

        using (var streamReader = new StreamReader(inputFileName))
            data = streamReader.ReadToEnd();

        var currentFloor = 0;
        var firstBasementOccurenceIndex = -1;
        var length = data.Length;

        for (var i = 0; i < length; ++i)
        {
            var floor = data[i];

            if (floor == _upFloor)
                ++currentFloor;
            else if (floor == _downFloor)
                --currentFloor;
            else
                throw new ArgumentException($"Unknown floor '{floor}', check input data");

            if (currentFloor == -1 && firstBasementOccurenceIndex == -1)
                firstBasementOccurenceIndex = i + 1;
        }

        Console.WriteLine($"Part 1. Santa is on {currentFloor} floor");
        Console.WriteLine($"Part 2. First basement occurence is on position {firstBasementOccurenceIndex}");
    }
}
