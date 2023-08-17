class CircuitProcessor
{
    private const string _instructionDelimiter = " -> ";
    private const string _notCommand = "NOT ";
    private const string _andCommand = "AND";
    private const string _orCommand = "OR";
    private const string _lShiftCommand = "LSHIFT";
    private const string _rShiftCommand = "RSHIFT";
    private const string _endSetBInstruction = " -> b";

    private const char _spaceDelimiter = ' ';

    private static readonly int _notCommandLength = _notCommand.Length;

    public string[] Instructions { get; }
    public Dictionary<string, ushort> Wires { get; }

    public CircuitProcessor(string inputFileName)
    {
        Instructions = ReadInstructions(inputFileName);
        Wires = new Dictionary<string, ushort>();
    }

    private string[] ReadInstructions(string inputFileName)
    {
        using (var streamReader = new StreamReader(inputFileName))
            return streamReader.ReadToEnd().Split("\r\n");
    }

    public void SolvePart1()
    {
        RunInstructions();

        if (Wires.TryGetValue("a", out ushort aValue))
            Console.WriteLine($"Part 1. Value on wire \"a\": {aValue}");
        else
            Console.WriteLine($"Part 1. There is no wire \"a\"");
    }

    public void SolvePart2()
    {
        if (!Wires.TryGetValue("a", out ushort aValue))
        {
            Console.WriteLine($"Part 2. There is no wire \"a\"");
            return;
        }

        var instructionToOverride = $"{aValue} -> b";
        var settedUp = false;
        int length = Instructions.Length;

        for (int i = 0; i < length; ++i)
        {
            if (Instructions[i].EndsWith(_endSetBInstruction))
            {
                Instructions[i] = instructionToOverride;
                settedUp = true;
                break;
            }
        }

        if (!settedUp)
        {
            Console.WriteLine($"There were no instruction which ends with \"{_endSetBInstruction}\"");
            return;
        }

        Wires.Clear();
        RunInstructions();

        Console.WriteLine($"Part 2. Value on wire \"a\": {Wires["a"]}");
    }

    private void RunInstructions()
    {
        var instructionsQueue = new Queue<string>(Instructions);

        while (instructionsQueue.Count > 0)
        {
            var instruction = instructionsQueue.Dequeue();
            var split = instruction.Split(_instructionDelimiter);
            var leftSide = split[0];
            var destinationWire = split[1];

            if (ParseOrTryGetValue(leftSide, out var leftSideValue))
            {
                SetValue(destinationWire, leftSideValue);
                continue;
            }

            if (leftSide.StartsWith(_notCommand))
            {
                var inputToComplement = leftSide.Remove(0, _notCommandLength);

                if (ParseOrTryGetValue(inputToComplement, out var valueToComplement))
                    SetValue(destinationWire, (ushort)~valueToComplement);
                else
                    instructionsQueue.Enqueue(instruction);

                continue;
            }

            var parameters = leftSide.Split(_spaceDelimiter);

            if (ParseOrTryGetValue(parameters[0], out var firstArg) && ParseOrTryGetValue(parameters[2], out var secondArg))
            {
                var command = parameters[1];
                var result = command switch
                {
                    _andCommand => (ushort)(firstArg & secondArg),
                    _orCommand => (ushort)(firstArg | secondArg),
                    _lShiftCommand => (ushort)(firstArg << secondArg),
                    _rShiftCommand => (ushort)(firstArg >> secondArg),
                    _ => throw new ArgumentException($"Unknown command in instruction \"{instruction}\". Check input data"),
                };

                SetValue(destinationWire, result);
                continue;
            }

            instructionsQueue.Enqueue(instruction);
        }
    }

    private bool ParseOrTryGetValue(string input, out ushort value)
    {
        if (ushort.TryParse(input, out value))
            return true;

        return Wires.TryGetValue(input, out value);
    }

    private void SetValue(string wire, ushort value)
    {
        if (!Wires.ContainsKey(wire))
            Wires.Add(wire, value);
        else
            Wires[wire] = value;
    }
}
