namespace Day19;

class Program
{
    private const string _replacementDelimiter = " => ";

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
            if (!cachedReplacementPositions.TryGetValue(Item, out var positions))
            {
                positions = new List<int>();
                var nameLength = Item.Length;
                int index;

                // while ((index = molecule.IndexOf(Item)) != -1)
                // {
                //     positions.Add(index);
                //     molecule = molecule.Remove(index, nameLength);
                //     molecule.Insert(index, Enumerable.Range()
                // }
            }
        }

        Console.WriteLine(replacements.Count);
    }
}
