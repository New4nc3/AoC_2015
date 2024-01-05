using System.Diagnostics;

namespace Day24;

class ArrangementContainer
{
    public List<int> First { get; private set; }
    public List<int> Second { get; private set; }
    public List<int> Third { get; private set; }

    public long QE { get; private set; }
    public bool Initialized => First != null && Second != null && Third != null;

    public ArrangementContainer(IEnumerable<int> firstGroup)
    {
        First = new List<int>(firstGroup);

        long product = 1;
        First.ForEach(x => product *= x);
        QE = product;
    }

    public void TryInitGroup(IEnumerable<int> group)
    {
        if (Second == null)
            Second = new List<int>(group);
        else if (Third == null)
            Third = new List<int>(group);
    }

    public override string ToString() =>
        $"({string.Join(", ", First)}); ({string.Join(", ", Second)}); ({string.Join(", ", Third)}) with QE = {QE}";
}

class Program
{
    private static List<List<int>> _uniqueSubgroups = new();

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        var packages = File.ReadAllLines(inputFileName).Select(int.Parse).OrderByDescending(x => x).ToList();
        var sum = packages.Sum() / 3;
        var uniqueGroups = new List<List<int>>();

        ArrangementContainer? bestArrangement1 = null;

        foreach (var candidate in GenerateCandidatesRecursively(packages, sum, 0, new List<int>()))
        {
            if (!TryAddToUniqueGroups(candidate, uniqueGroups))
                continue;

            var tempArrangement = new ArrangementContainer(candidate);

            if (bestArrangement1 != null && (bestArrangement1.First.Count < candidate.Count || bestArrangement1.QE < tempArrangement.QE))
                continue;

            try
            {
                TryInitArrangementRecursively(packages.Except(candidate).ToList(), sum, 0, new List<int>(), tempArrangement);
            }
            catch { }

            if (tempArrangement.Initialized && (bestArrangement1 == null || tempArrangement.QE < bestArrangement1.QE))
            {
                bestArrangement1 = tempArrangement;
                break;
            }
        }

        Console.WriteLine($"Total packages: {packages.Count}. Total weight: {packages.Sum()};");

        if (bestArrangement1 != null)
            Console.WriteLine($"Best arrangement to get {sum}: {bestArrangement1}");
    }

    private static bool TryAddToUniqueGroups(List<int> group, List<List<int>> groups)
    {
        var sorted = group.OrderByDescending(x => x).ToList();
        var count = sorted.Count;

        foreach (var currentGroup in groups)
        {
            var currentCount = currentGroup.Count;

            if (currentCount != count)
                continue;

            var skipToNext = false;

            for (var i = 0; i < count; ++i)
            {
                if (sorted[i] != currentGroup[i])
                {
                    skipToNext = true;
                    break;
                }
            }

            if (skipToNext)
                continue;
            else
                return false;
        }

        groups.Add(sorted);
        return true;
    }

    static void TryInitArrangementRecursively(List<int> remainingPackages, int desiredWeight, int currentWeight, List<int> currentGroup, ArrangementContainer container)
    {
        if (currentWeight == desiredWeight && !container.Initialized && TryAddToUniqueGroups(currentGroup, _uniqueSubgroups))
        {
            container.TryInitGroup(currentGroup);

            if (remainingPackages.Sum(x => x) == desiredWeight)
            {
                container.TryInitGroup(remainingPackages);
                TryAddToUniqueGroups(remainingPackages, _uniqueSubgroups);
                throw new NotImplementedException();
            }

            // if (remainingPackages.Count > 0)
            //     TryInitArrangementRecursively(remainingPackages, desiredWeight, 0, new List<int>(), container);
        }
        else if (currentWeight < desiredWeight)
        {
            var count = remainingPackages.Count;

            for (var i = 0; i < count; ++i)
            {
                var newPackages = new List<int>(remainingPackages);
                newPackages.RemoveAt(i);
                var item = remainingPackages[i];
                var newTempGroup = new List<int>(currentGroup) { item };

                TryInitArrangementRecursively(newPackages, desiredWeight, currentWeight + item, newTempGroup, container);
            }
        }
    }

    static IEnumerable<List<int>> GenerateCandidatesRecursively(List<int> packages, int desiredWeight, int currentWeight, List<int> currentGroup)
    {
        if (currentWeight == desiredWeight)
        {
            yield return currentGroup;
        }
        else if (currentWeight < desiredWeight)
        {
            var count = packages.Count;

            for (var i = 0; i < count; ++i)
            {
                var newPackages = new List<int>(packages);
                newPackages.RemoveAt(i);
                var item = packages[i];
                var newTempGroup = new List<int>(currentGroup) { item };

                foreach (var candidate in GenerateCandidatesRecursively(newPackages, desiredWeight, currentWeight + item, newTempGroup))
                    yield return candidate;
            }
        }
    }
}
