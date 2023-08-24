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

        var bestCombination = new List<byte>();
        var bestCaloriesCombination = new List<byte>();
        var bestScore = 0;
        var bestScoreForCalories = 0;
        var ingredientsCount = ingredients.Count;

        foreach (var combination in GenerateCombination(ingredientsCount))
        {
            int totalCapacity = 0, totalDurability = 0, totalFlavor = 0, totalTexture = 0, totalCalories = 0;

            for (var i = 0; i < ingredientsCount; ++i)
            {
                var combinationAmount = combination[i];
                var ingredient = ingredients[i];

                totalCapacity += combinationAmount * ingredient.Capacity;
                totalDurability += combinationAmount * ingredient.Durability;
                totalFlavor += combinationAmount * ingredient.Flavor;
                totalTexture += combinationAmount * ingredient.Texture;
                totalCalories += combinationAmount * ingredient.Calories;
            }

            if (IsAnyNegative(totalCapacity, totalDurability, totalFlavor, totalTexture))
                continue;

            var tempScore = totalCapacity * totalDurability * totalFlavor * totalTexture;

            if (tempScore > bestScore)
            {
                bestScore = tempScore;
                bestCombination = new List<byte>(combination);
            }

            if (totalCalories == 500 && tempScore > bestScoreForCalories)
            {
                bestScoreForCalories = tempScore;
                bestCaloriesCombination = new List<byte>(combination);
            }
        }

        var ingredientNames = ingredients.Select(x => x.Name).ToList();

        Console.WriteLine($"Part 1. Best score is: {bestScore}, created by next amounts: {string.Join(", ", bestCombination)}" + 
            $" of ingredients {string.Join(", ", ingredientNames)} respectively");
        Console.WriteLine($"Part 2. Now, the best score with achieving 500 calories is: {bestScoreForCalories}, created by " + 
            $"{string.Join(", ", bestCaloriesCombination)} of {string.Join(", ", ingredientNames)} respectively");
    }

    static bool IsAnyNegative(params int[] properties) =>
        properties.Any(p => p < 0);

    static IEnumerable<IList<byte>> GenerateCombination(int count)
    {
        var combinations = new List<byte>();

        for (var i = 0; i < count; ++i)
            combinations.Add(0);

        var lastIndex = count - 1;
        int outOfRangeIndex;
        int tempSum;

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

            tempSum = 0;
            for (var i = 0; i < lastIndex; ++i)
                tempSum += combinations[i];

            if (tempSum > 100)
            {
                combinations[lastIndex - 1] = 100;
                continue;
            }

            combinations[lastIndex] = (byte)(100 - tempSum);

            yield return combinations;

            combinations[lastIndex] = 100;
        }
    }
}
