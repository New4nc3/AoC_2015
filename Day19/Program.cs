using System.Text;

namespace Day19;

class Program
{
    private const string _replacementDelimiter = " => ";
    private const string _rn = "Rn";
    private const string _ar = "Ar";
    private const string _y = "Y";
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

        var allElements = SplitToTokens(molecule);
        var rnArElements = allElements.Where(x => x == _rn || x == _ar).ToList();
        var yElements = allElements.Where(x => x == _y).ToList();
        var steps = allElements.Count - rnArElements.Count - 2 * yElements.Count - 1;

        if (rnArElements.Count == 0 && yElements.Count == 0)
            ++steps;

        Console.WriteLine($"Part 1. Count of distinct molecules is: {distinctMolecules.Count}");
        Console.WriteLine($"Part 2. From electron, molecule can be created in {steps} steps");
    }

    static List<int> FindAllEntrances(string element, string molecula)
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

    static List<string> SplitToTokens(string molecula)
    {
        var tokens = new List<string>();
        var tempToken = string.Empty;

        foreach (var character in molecula)
        {
            if (char.IsUpper(character) && !string.IsNullOrEmpty(tempToken))
            {
                tokens.Add(tempToken);
                tempToken = string.Empty;
            }

            tempToken += character;
        }

        tokens.Add(tempToken);
        return tokens;
    }
}
