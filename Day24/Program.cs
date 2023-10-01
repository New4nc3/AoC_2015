namespace Day24;

class Program
{
    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        var packages = File.ReadAllLines(inputFileName).Select(int.Parse).ToList();

        Console.WriteLine($"Total packages: {packages.Count}. Total weight: {packages.Sum()}");
    }
}
