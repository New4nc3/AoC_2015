namespace Day7;

class Program
{
    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        var circuitProcessor = new CircuitProcessor(inputFileName);

        circuitProcessor.SolvePart1();
        circuitProcessor.SolvePart2();
    }
}
