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
        var cachedMolecule = molecule;

        foreach (var (Item, Replacement) in replacements)
        {
            var nameLength = Item.Length;

            if (!cachedReplacementPositions.TryGetValue(Item, out var positions))
            {
                positions = new List<int>();

                var tempReplacer = string.Empty;
                for (int i = 0; i < nameLength; ++i)
                    tempReplacer += _charToReplace;

                int index;
                while ((index = cachedMolecule.IndexOf(Item)) != -1)
                {
                    positions.Add(index);
                    cachedMolecule = cachedMolecule.Remove(index, nameLength).Insert(index, tempReplacer);
                }

                cachedReplacementPositions.Add(Item, positions);
            }

            foreach (var position in positions)
            {
                var candidate = new StringBuilder(molecule);
                candidate.Remove(position, nameLength).Insert(position, Replacement);
                distinctMolecules.Add(candidate.ToString());
            }
        }

        Console.WriteLine($"Part 1. Count of distinct molecules is: {distinctMolecules.Count}");
    }
}
