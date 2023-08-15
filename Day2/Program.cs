namespace Day2;

class Program
{
    private const char _dimensionSplitter = 'x';

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        using var streamReader = new StreamReader(inputFileName);

        var totalSquare = 0;
        var totalSquareRibbon = 0;
        string? input;
        Box box;

        while ((input = streamReader.ReadLine()) != null)
        {
            var dimensions = input.Split(_dimensionSplitter);
            box = new Box(int.Parse(dimensions[0]), int.Parse(dimensions[1]), int.Parse(dimensions[2]));

            totalSquare += box.Square;
            totalSquareRibbon += box.SquareRibbon;
        }

        Console.WriteLine($"Part 1. Total square feet of wrapping paper is {totalSquare}");
        Console.WriteLine($"Part 2. Total feet of ribbon to order is {totalSquareRibbon}");
    }
}
