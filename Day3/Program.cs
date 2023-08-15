namespace Day3;

class Program
{
    private const char _up = '^';
    private const char _right = '>';
    private const char _down = 'v';
    private const char _left = '<';

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        string data;

        using (var stream = new StreamReader(inputFileName))
            data = stream.ReadToEnd();

        var santaPosition1 = new Position();
        var santaPosition2 = new Position();
        var roboPosition = new Position();
        var visitedBySanta = new HashSet<Position>() { santaPosition1 };
        var visitedWithRoboSanta = new HashSet<Position>() { santaPosition2 };
        var length = data.Length;

        for (var i = 0; i < length; ++i)
        {
            var direction = data[i];

            switch (direction)
            {
                case _up:
                    santaPosition1.MoveUp();
                    visitedBySanta.Add(santaPosition1);

                    if (i % 2 == 0)
                    {
                        santaPosition2.MoveUp();
                        visitedWithRoboSanta.Add(santaPosition2);
                    }
                    else
                    {
                        roboPosition.MoveUp();
                        visitedWithRoboSanta.Add(roboPosition);
                    }
                    break;

                case _right:
                    santaPosition1.MoveRight();
                    visitedBySanta.Add(santaPosition1);

                    if (i % 2 == 0)
                    {
                        santaPosition2.MoveRight();
                        visitedWithRoboSanta.Add(santaPosition2);
                    }
                    else
                    {
                        roboPosition.MoveRight();
                        visitedWithRoboSanta.Add(roboPosition);
                    }
                    break;

                case _down:
                    santaPosition1.MoveDown();
                    visitedBySanta.Add(santaPosition1);

                    if (i % 2 == 0)
                    {
                        santaPosition2.MoveDown();
                        visitedWithRoboSanta.Add(santaPosition2);
                    }
                    else
                    {
                        roboPosition.MoveDown();
                        visitedWithRoboSanta.Add(roboPosition);
                    }
                    break;

                case _left:
                    santaPosition1.MoveLeft();
                    visitedBySanta.Add(santaPosition1);

                    if (i % 2 == 0)
                    {
                        santaPosition2.MoveLeft();
                        visitedWithRoboSanta.Add(santaPosition2);
                    }
                    else
                    {
                        roboPosition.MoveLeft();
                        visitedWithRoboSanta.Add(roboPosition);
                    }
                    break;

                default:
                    throw new ArgumentException($"Unknown direction '{direction}'. Check input data");
            }
        }

        Console.WriteLine($"Part 1. At least 1 present will receive {visitedBySanta.Count} houses");
        Console.WriteLine($"Part 2. At least 1 present will receive {visitedWithRoboSanta.Count} houses");
    }
}
