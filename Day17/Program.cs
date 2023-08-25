namespace Day17;

class Program
{
    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        var storeAmount = inputFileName == "test.txt" ? 25 : 150;
        List<int> containers = File.ReadLines(inputFileName).Select(int.Parse).OrderByDescending(x => x).ToList();

        foreach (var combination in ProcessCombination(0, -1, 0))
        {
            Console.WriteLine(combination);
        }

        IEnumerable<int> ProcessCombination(int sum, int index, int depth)
        {
            if (index >= 0)
                sum += containers[index];

            if (sum == storeAmount)
                yield return depth;
            else if (sum < storeAmount)
                for (int j = index; j < containers.Count - 1; ++j)
                    foreach (var tempCombination in ProcessCombination(sum, j + 1, depth + 1))
                        yield return tempCombination;
        }
    }
}
