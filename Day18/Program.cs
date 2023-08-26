namespace Day18;

class Program
{
    private const char _lightChar = '#';
    private const char _emptyChar = '.';

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        var simulationsCount = inputFileName == "test.txt" ? 5 : 100;

        string[] data;
        using (var streamReader = new StreamReader(inputFileName))
            data = streamReader.ReadToEnd().Split("\r\n");

        var rows = data.Length;
        var cols = data[0].Length;
        char[,] currentState = new char[rows, cols];

        ProcessSimulation(forPart2: false);
        Console.WriteLine($"Part 1. After {simulationsCount} simulations, total lights count is: {LightsOnCount()}");

        ProcessSimulation(forPart2: true);
        Console.WriteLine($"Part 2. Now, after {simulationsCount} simulations, total lights count is: {LightsOnCount()}");

        int LightsOnCount()
        {
            int count = 0;

            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    if (currentState[i, j] == _lightChar)
                        ++count;

            return count;
        }

        void ProcessSimulation(bool forPart2)
        {
            InitCurrentState(forPart2);

            for (int simulation = 0; simulation < simulationsCount; ++simulation)
            {
                var nextState = new char[rows, cols];

                for (int i = 0; i < rows; ++i)
                    for (int j = 0; j < cols; ++j)
                        nextState[i, j] = ProcessCell(i, j, forPart2);

                currentState = nextState;
            }
        }

        void InitCurrentState(bool forPart2)
        {
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    currentState[i, j] = data[i][j];

            if (forPart2)
                currentState[0, 0] = currentState[0, cols - 1] = currentState[rows - 1, 0] = currentState[rows - 1, cols - 1] = _lightChar;
        }

        char ProcessCell(int i, int j, bool forPart2 = false)
        {
            if (forPart2 && ((i == 0 && j == 0) || (i == 0 && j == cols - 1) || (i == rows - 1 && j == 0) || (i == rows - 1 && j == cols - 1)))
                return _lightChar;

            int alive = 0;

            if (i - 1 >= 0)
            {
                if (j - 1 >= 0 && currentState[i - 1, j - 1] == _lightChar)
                    ++alive;

                if (currentState[i - 1, j] == _lightChar)
                    ++alive;

                if (j + 1 < cols && currentState[i - 1, j + 1] == _lightChar)
                    ++alive;
            }

            if (j - 1 >= 0 && currentState[i, j - 1] == _lightChar)
                ++alive;

            if (j + 1 < cols && currentState[i, j + 1] == _lightChar)
                ++alive;

            if (i + 1 < rows)
            {
                if (j - 1 >= 0 && currentState[i + 1, j - 1] == _lightChar)
                    ++alive;

                if (currentState[i + 1, j] == _lightChar)
                    ++alive;

                if (j + 1 < cols && currentState[i + 1, j + 1] == _lightChar)
                    ++alive;
            }

            return currentState[i, j] switch
            {
                _lightChar => alive == 2 || alive == 3 ? _lightChar : _emptyChar,
                _emptyChar => alive == 3 ? _lightChar : _emptyChar,
                _ => throw new ArgumentException($"Unknown current state: \"{currentState[i, j]}\""),
            };
        }
    }
}
