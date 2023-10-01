namespace Day24;

class Program
{
    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        var packages = File.ReadAllLines(inputFileName).Select(int.Parse).OrderByDescending(x => x).ToList();
        var sum = packages.Sum() / 3;
        var group1 = new List<HashSet<int>>();

        GenerateGroups(packages, sum, 0, new List<int>(), group1);

        foreach (var list in group1)
            Console.WriteLine(string.Join(", ", list));

        Console.WriteLine($"Total packages: {packages.Count}. Total weight: {packages.Sum()}");
        Console.WriteLine($"Count of possible combinations to get {sum}: {group1.Count}");
    }

    static void GenerateGroups(List<int> packages, int desiredWeight, int sum, List<int> tempGroup, List<HashSet<int>> groups)
    {
        if (sum == desiredWeight)
        {
            tempGroup = tempGroup.OrderByDescending(x => x).ToList();
            groups.Add(tempGroup.ToHashSet());
        }
        else if (sum < desiredWeight)
        {
            var count = packages.Count;

            for (var i = 0; i < count; ++i)
            {
                var newPackages = new List<int>(packages);
                newPackages.RemoveAt(i);
                var item = packages[i];
                var newTempGroup = new List<int>(tempGroup) { item };

                GenerateGroups(newPackages, desiredWeight, sum + item, newTempGroup, groups);
            }
        }
    }
}
