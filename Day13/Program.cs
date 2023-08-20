namespace Day13;

class Program
{
    private const string _guestDelimiter = " happiness units by sitting next to ";
    private const string _happinessDelimiter = " would ";
    private const string _loseString = "lose";

    private const char _spaceDelimiter = ' ';

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        using var streamReader = new StreamReader(inputFileName);

        var guests = new Dictionary<string, Dictionary<string, int>>();
        string? data;

        while ((data = streamReader.ReadLine()) != null)
        {
            var split = data.Split(_guestDelimiter);
            var guestName = split[1][0..^1];
            var splitLeftSide = split[0].Split(_happinessDelimiter);
            var initiatorName = splitLeftSide[0];
            var splitScore = splitLeftSide[1].Split(_spaceDelimiter);
            var score = int.Parse(splitScore[1]);

            if (splitScore[0] == _loseString)
                score *= -1;

            if (!guests.ContainsKey(initiatorName))
                guests.Add(initiatorName, new Dictionary<string, int>());

            guests[initiatorName].Add(guestName, score);
        }

        var (BestCombination, MaximumHappiness) = CalculateOptimalArrangement();
        Console.WriteLine($"Part 1. Best combination is: {string.Join(" -> ", BestCombination)}. Total happiness is: {MaximumHappiness}");

        AddMeToGuests();
        (BestCombination, MaximumHappiness) = CalculateOptimalArrangement();
        Console.WriteLine($"Part 2. Now the best combination is: {string.Join(" -> ", BestCombination)}. Total happiness is: {MaximumHappiness}");

        (List<string> BestCombination, int MaximumHappiness) CalculateOptimalArrangement()
        {
            var BestCombination = new List<string>();
            var MaximumHappiness = 0;
            var lastGuestIndex = guests.Count - 1;

            foreach (var permutation in GeneratePermutations(guests.Keys.ToList(), 0, guests.Count))
            {
                var currentHappiness = 0;
                var firstGuestName = permutation[0];
                var secondGuestName = permutation[lastGuestIndex];

                currentHappiness += guests[firstGuestName][secondGuestName];
                currentHappiness += guests[secondGuestName][firstGuestName];

                for (var i = 0; i < lastGuestIndex; ++i)
                {
                    firstGuestName = permutation[i];
                    secondGuestName = permutation[i + 1];

                    currentHappiness += guests[firstGuestName][secondGuestName];
                    currentHappiness += guests[secondGuestName][firstGuestName];
                }

                if (currentHappiness > MaximumHappiness)
                {
                    MaximumHappiness = currentHappiness;
                    BestCombination = permutation.ToList();
                }
            }

            return (BestCombination, MaximumHappiness);
        }

        void AddMeToGuests()
        {
            var allGuestNames = guests.Keys.ToList();
            var me = "Me";
            guests.Add(me, new Dictionary<string, int>());

            foreach (var guestName in allGuestNames)
            {
                guests[me].Add(guestName, 0);
                guests[guestName].Add(me, 0);
            }
        }
    }

    public static IEnumerable<IList<T>> GeneratePermutations<T>(IList<T> input, int startIndex, int endIndex)
    {
        if (startIndex == endIndex)
            yield return input;
        else
        {
            for (var i = startIndex; i < endIndex; ++i)
            {
                (input[i], input[startIndex]) = (input[startIndex], input[i]);

                foreach (var permutation in GeneratePermutations(input, startIndex + 1, endIndex))
                    yield return permutation;

                (input[i], input[startIndex]) = (input[startIndex], input[i]);
            }
        }
    }
}
