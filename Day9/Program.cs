namespace Day9;

class Program
{
    private const string _distanceDelimiter = " = ";
    private const string _citiesDelimiter = " to ";

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        using var streamReader = new StreamReader(inputFileName);
        string? data;
        var cities = new Dictionary<string, Dictionary<string, int>>();

        while ((data = streamReader.ReadLine()) != null)
        {
            var split = data.Split(_distanceDelimiter);
            var parsedCities = split[0].Split(_citiesDelimiter);

            AddCities(parsedCities[0], parsedCities[1], int.Parse(split[1]));
        }

        var shortestRoute = new HashSet<string>();
        var longestRoute = new HashSet<string>();
        var cityNames = cities.Keys.ToList();
        var shortestDistance = int.MaxValue;
        var longestDistance = 0;

        foreach (var cityName in cityNames)
        {
            var route1 = new HashSet<string> { cityName };
            var route2 = new HashSet<string> { cityName };
            var candidates1 = new HashSet<string>(cityNames.Except(route1));
            var candidates2 = new HashSet<string>(cityNames.Except(route2));
            var currentDistance1 = 0;
            var currentDistance2 = 0;

            while (candidates1.Count > 0)
            {
                var (NearestCity, Distance) = NearestFromDictionary(route1);
                route1.Add(NearestCity);
                candidates1.Remove(NearestCity);
                currentDistance1 += Distance;
            }

            while (candidates2.Count > 0)
            {
                var (FarthestCity, Distance) = FarthestFromDictionary(route2);
                route2.Add(FarthestCity);
                candidates2.Remove(FarthestCity);
                currentDistance2 += Distance;
            }

            if (currentDistance1 < shortestDistance)
            {
                shortestRoute = new HashSet<string>(route1);
                shortestDistance = currentDistance1;
            }

            if (currentDistance2 > longestDistance)
            {
                longestRoute = new HashSet<string>(route2);
                longestDistance = currentDistance2;
            }
        }

        Console.WriteLine($"Part 1. Shortest route is: {string.Join(" -> ", shortestRoute)}. Distance is: {shortestDistance}");
        Console.WriteLine($"Part 2. Longest route is: {string.Join(" -> ", longestRoute)}. Distance is: {longestDistance}");

        void AddCities(string city1, string city2, int distance)
        {
            if (!cities.ContainsKey(city1))
                cities.Add(city1, new Dictionary<string, int>());

            if (!cities[city1].ContainsKey(city2))
                cities[city1].Add(city2, distance);

            if (!cities.ContainsKey(city2))
                cities.Add(city2, new Dictionary<string, int>());

            if (!cities[city2].ContainsKey(city1))
                cities[city2].Add(city1, distance);
        }

        (string NearestCity, int Distance) NearestFromDictionary(IEnumerable<string> route)
        {
            var cityData = cities[route.Last()];
            var nearestCityName = string.Empty;
            var nearestCityDistance = int.MaxValue;

            foreach (var distanceKvp in cityData)
            {
                var distanceKey = distanceKvp.Key;
                var distanceValue = distanceKvp.Value;

                if (!route.Contains(distanceKey) && distanceValue < nearestCityDistance)
                {
                    nearestCityName = distanceKey;
                    nearestCityDistance = distanceValue;
                }
            }

            if (nearestCityDistance == int.MaxValue)
                throw new ArgumentException("City base does not return values. Check input or params");

            return (nearestCityName, nearestCityDistance);
        }

        (string FarthestCity, int Distance) FarthestFromDictionary(IEnumerable<string> route)
        {
            var cityData = cities[route.Last()];
            var farthestCityName = string.Empty;
            var farthestCityDistance = -1;

            foreach (var distanceKvp in cityData)
            {
                var distanceKey = distanceKvp.Key;
                var distanceValue = distanceKvp.Value;

                if (!route.Contains(distanceKey) && distanceValue > farthestCityDistance)
                {
                    farthestCityName = distanceKey;
                    farthestCityDistance = distanceValue;
                }
            }

            if (farthestCityDistance == -1)
                throw new ArgumentException("City base does not return values. Check input or params");

            return (farthestCityName, farthestCityDistance);
        }
    }
}
