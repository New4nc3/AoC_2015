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

        InitCurrentState();
        //PrintState();

        for (int simulation = 0; simulation < simulationsCount; ++simulation)
        {
            var nextState = new char[rows, cols];

            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    nextState[i, j] = ProcessCell(i, j);

            currentState = nextState;
            //PrintState();
        }

        Console.WriteLine($"Part 1. After {simulationsCount} simulations, total lights count is: {LightsOnCount()}");

        void PrintState()
        {
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                    Console.Write(currentState[i, j]);

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        char ProcessCell(int i, int j)
        {
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

        int LightsOnCount()
        {
            int count = 0;

            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    if (currentState[i, j] == _lightChar)
                        ++count;

            return count;
        }

        void InitCurrentState()
        {
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    currentState[i, j] = data[i][j];
        }
    }
}
