namespace Day24;

class ArrangementContainer
{
    public List<int> First { get; private set; }
    public List<int>? Second { get; private set; } = null;
    public List<int>? Third { get; private set; } = null;

    public long QE { get; private set; }

    public virtual bool Initialized => First != null && Second != null && Third != null;

    public ArrangementContainer(IEnumerable<int> firstGroup)
    {
        First = new List<int>(firstGroup);

        long product = 1;
        First.ForEach(x => product *= x);
        QE = product;
    }

    public virtual bool TryInitGroup(IEnumerable<int> group)
    {
        if (Second == null)
        {
            Second = new List<int>(group);
            return true;
        }

        if (Third == null)
        {
            Third = new List<int>(group);
            return true;
        }

        return false;
    }

    public override string ToString() =>
        $"({string.Join(", ", First)}); ({string.Join(", ", Second)}); ({string.Join(", ", Third)}) with QE = {QE}";
}

class ArrangementContainer2 : ArrangementContainer
{
    public List<int>? Fourth { get; private set; } = null;

    public ArrangementContainer2(IEnumerable<int> firstGroup) : base(firstGroup) { }

    public override bool TryInitGroup(IEnumerable<int> group)
    {
        if (!base.TryInitGroup(group) && Fourth == null)
        {
            Fourth = new List<int>(group);
            return true;
        }

        return false;
    }
}

class Program
{
    private static readonly List<List<int>> _uniqueGroups = new();
    private static readonly List<List<int>> _uniqueSubgroups = new();

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        var packages = File.ReadAllLines(inputFileName).Select(int.Parse).OrderByDescending(x => x).ToList();
        var sumPackages = packages.Sum();
        var sumPart1 = sumPackages / 3;
        var sumPart2 = sumPackages / 4;

        Console.WriteLine($"Total packages: {packages.Count}. Total weight: {packages.Sum()}. Required weights: ({sumPart1}; {sumPart2})\n");

        Solve(packages, sumPart1, part1: true);
        Solve(packages, sumPart2, part1: false);
    }

    private static void Solve(List<int> packages, int desiredWeight, bool part1)
    {
        ArrangementContainer? bestArrangement1 = null;
        ArrangementContainer2? bestArrangement2 = null;

        ArrangementContainer? temp1 = null;
        ArrangementContainer2? temp2 = null;

        foreach (var candidate in GenerateCandidatesRecursively(packages, desiredWeight, 0, new List<int>()))
        {
            if (!TryAddToUniqueGroups(candidate, _uniqueGroups))
                continue;

            if (part1)
            {
                temp1 = new ArrangementContainer(candidate);

                if (bestArrangement1 != null && (bestArrangement1.First.Count < candidate.Count || bestArrangement1.QE < temp1.QE))
                    continue;
            }
            else
            {
                temp2 = new ArrangementContainer2(candidate);

                if (bestArrangement2 != null && (bestArrangement2.First.Count < candidate.Count || bestArrangement2.QE < temp2.QE))
                    continue;
            }

            try
            {
                TryInitArrangementRecursively(packages.Except(candidate).ToList(), desiredWeight, 0, new List<int>(), part1 ? temp1 : temp2);
            }
            catch { }

            if (part1)
            {
                if (temp1.Initialized && (bestArrangement1 == null || temp1.QE < bestArrangement1.QE))
                {
                    bestArrangement1 = temp1;
                    Console.WriteLine($"Part 1. Best arrangement to get {desiredWeight}: {bestArrangement1}");
                    break;
                }
            }
            else
            {
                if (temp2.Initialized && (bestArrangement2 == null || temp2.QE < bestArrangement2.QE))
                {
                    bestArrangement2 = temp2;
                    Console.WriteLine($"Part 2. Best arrangement to get {desiredWeight}: {bestArrangement2}");
                    break;
                }
            }
        }
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

            var sumOfRemaining = remainingPackages.Sum(x => x);

            if (sumOfRemaining == desiredWeight)
            {
                container.TryInitGroup(remainingPackages);
                TryAddToUniqueGroups(remainingPackages, _uniqueSubgroups);
                throw new NotImplementedException();
            }
            else if (sumOfRemaining > desiredWeight)
            {
                TryInitArrangementRecursively(remainingPackages, desiredWeight, 0, new List<int>(), container);
            }
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
