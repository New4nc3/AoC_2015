namespace Day15;

class Program
{
    private const string _nameDelimiter = ": ";
    private const string _ingredientsDelimiter = ", ";
    private const char _spaceDelimiter = ' ';

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        using var streamReader = new StreamReader(inputFileName);

        var ingredients = new List<Ingredient>();
        string? input;

        while ((input = streamReader.ReadLine()) != null)
        {
            var parts = input.Split(_nameDelimiter);
            var name = parts[0];
            var properties = parts[1].Split(_ingredientsDelimiter);

            var capacity = int.Parse(properties[0].Split(_spaceDelimiter)[1]);
            var durability = int.Parse(properties[1].Split(_spaceDelimiter)[1]);
            var flavor = int.Parse(properties[2].Split(_spaceDelimiter)[1]);
            var texture = int.Parse(properties[3].Split(_spaceDelimiter)[1]);
            var calories = int.Parse(properties[4].Split(_spaceDelimiter)[1]);

            ingredients.Add(new Ingredient(name, capacity, durability, flavor, texture, calories));
        }

        foreach (var combination in GenerateCombination(ingredients.Count))
        {
            Console.Write($"{string.Join(" ", combination)}\t");
        }
    }

    static IEnumerable<IList<byte>> GenerateCombination(int count)
    {
        var combinations = new List<byte>();

        for (var i = 0; i < count; ++i)
            combinations.Add(0);

        var lastIndex = count - 1;
        int outOfRangeIndex = 0;

        while (true)
        {
            ++combinations[lastIndex];

            while ((outOfRangeIndex = combinations.IndexOf(101)) != -1)
            {
                if (outOfRangeIndex == 0)
                    yield break;
                
                combinations[outOfRangeIndex] = 0;
                ++combinations[outOfRangeIndex - 1];
            }

            if (combinations.Sum(x => x) == 100)
                yield return combinations;
        }
    }
}
