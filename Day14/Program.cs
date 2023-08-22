namespace Day14;

class Program
{
    private const string _middleDelimiter = " seconds, but then must rest for ";
    private const string _canFlyDelimiter = " can fly ";
    private const string _speedDurationDelimiter = " km/s for ";
    private const char _spaceDelimiter = ' ';

    static void Main(string[] args)
    {
        var iterationsCount = 2503;
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        using var streamReader = new StreamReader(inputFileName);
        var reindeers = new List<Reindeer>();

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

            reindeers.Add(new Reindeer(name, speed, duration, restTime));
        }

        for (var i = 0; i < iterationsCount; ++i)
        {
            foreach (var reindeer in reindeers)
                reindeer.ProcessSecond();
            
            var maxDistance = reindeers.Max(x => x.TotalDistance);
            var leaders = reindeers
                .OrderByDescending(x => x.TotalDistance)
                .TakeWhile(x => x.TotalDistance == maxDistance)
                .ToList();

            foreach (var leader in leaders)
                leader.IncreaseForLeading();
        }

        var leader1 = reindeers.OrderByDescending(x => x.TotalDistance).First();
        var leader2 = reindeers.OrderByDescending(x => x.ScorePart2).First();

        Console.WriteLine($"Part 1. After {iterationsCount} iterations, the best one is {leader1.Name}, it has {leader1.TotalDistance} points");
        Console.WriteLine($"Part 2. Now leader is {leader2.Name}, it has {leader2.ScorePart2} points");
    }
}
