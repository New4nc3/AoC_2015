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

        string? input;
        while ((input = streamReader.ReadLine()) != null)
        {
            var parts = input.Split(_nameDelimiter);
            var name = parts[0];
            var ingredients = parts[1].Split(_ingredientsDelimiter);

            var capacity = int.Parse(ingredients[0].Split(_spaceDelimiter)[1]);
            var durability = int.Parse(ingredients[1].Split(_spaceDelimiter)[1]);
            var flavor = int.Parse(ingredients[2].Split(_spaceDelimiter)[1]);
            var texture = int.Parse(ingredients[3].Split(_spaceDelimiter)[1]);
            var calories = int.Parse(ingredients[4].Split(_spaceDelimiter)[1]);
        }
    }
}
