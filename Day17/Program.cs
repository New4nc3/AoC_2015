namespace Day17;

class Program
{
    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        var storeAmount = inputFileName == "test.txt" ? 25 : 150;

        List<int> containers = File.ReadLines(inputFileName).Select(int.Parse).OrderByDescending(x => x).ToList();
        var beforeLastIndex = containers.Count - 1;
        var allCombinations = ProcessCombination(new List<int>(), 0, -1).ToList();
        var part2 = allCombinations.GroupBy(x => x.Count).OrderBy(x => x.Key).First();

        Console.WriteLine($"Part 1. All possible combinations to store exactly {storeAmount} liters: {allCombinations.Count}");
        Console.WriteLine($"Part 2. There are {part2.Count()} different ways to use {part2.First().Count} containers to store {storeAmount} liters");

        IEnumerable<IList<int>> ProcessCombination(IList<int> current, int sum, int index)
        {
            if (index >= 0)
            {
                var tempValue = containers[index];
                current.Add(tempValue);
                sum += tempValue;
            }

            if (sum == storeAmount)
                yield return current;
            else if (sum < storeAmount)
                for (int i = index; i < beforeLastIndex; ++i)
                    foreach (var combination in ProcessCombination(new List<int>(current), sum, i + 1))
                        yield return combination;
        }
    }
}
