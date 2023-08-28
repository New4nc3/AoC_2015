using System.Text;

namespace Day19;

class Program
{
    private const string _replacementDelimiter = " => ";
    private const char _charToReplace = '#';

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        using var streamReader = new StreamReader(inputFileName);
        var replacements = new List<(string Item, string Replacement)>();
        string? data;

        while ((data = streamReader.ReadLine()) != string.Empty)
        {
            if (data == null)
                throw new ArgumentException("Wrong input file format. Check \"test.txt\" for example");

            var split = data.Split(_replacementDelimiter);
            replacements.Add((split[0], split[1]));
        }

        string molecule = streamReader.ReadLine() ?? throw new ArgumentException("Wrong input molecula format. Check \"test.txt\" for example");
        var distinctMolecules = new HashSet<string>();
        var cachedReplacementPositions = new Dictionary<string, List<int>>();

        foreach (var (Item, Replacement) in replacements)
        {
            var nameLength = Item.Length;

            if (!cachedReplacementPositions.TryGetValue(Item, out var positions))
            {
                positions = new List<int>(FindAllEntrances(Item, molecule));
                cachedReplacementPositions.Add(Item, positions);
            }

            foreach (var position in positions)
            {
                var candidate = new StringBuilder(molecule);
                candidate.Remove(position, nameLength).Insert(position, Replacement);
                distinctMolecules.Add(candidate.ToString());
            }
        }

        var steps = 0;
        var queueCandidates = new Queue<string>();
        queueCandidates.Enqueue("e");

        // todo - split molecule into elements and replace each one for each replacements (without duplicates)
        // while (queueCandidates.Count > 0)
        // {
        //     string tempMolecule = queueCandidates.Dequeue();

        //     if (tempMolecule == molecule)
        //         break;

        //     ++steps;
        // }

        Console.WriteLine($"Part 1. Count of distinct molecules is: {distinctMolecules.Count}");
        Console.WriteLine($"Part 2. From electron, molecule can be created in {steps} moves");
    }

    static IEnumerable<int> FindAllEntrances(string element, string molecula)
    {
        var positions = new List<int>();
        var tempReplacer = new StringBuilder();
        var nameLength = element.Length;

        for (int i = 0; i < nameLength; ++i)
            tempReplacer.Append(_charToReplace);

        int index;
        while ((index = molecula.IndexOf(element)) != -1)
        {
            positions.Add(index);
            molecula = molecula.Remove(index, nameLength).Insert(index, tempReplacer.ToString());
        }

        return positions;
    }
}
