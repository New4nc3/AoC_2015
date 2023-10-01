namespace Day23;

class Program
{
    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        string[] rawCommands = File.ReadAllLines(inputFileName);

        var parsedCommands = new List<BaseCommand>();
        var registriesContainer = new RegistriesContainer();
        var length = rawCommands.Length;

        for (var i = 0; i < length; ++i)
        {
            var command = rawCommands[i];

            if (command.StartsWith("inc"))
                parsedCommands.Add(new IncrementCommand(command, registriesContainer));
            else if (command.StartsWith("tpl"))
                parsedCommands.Add(new TripleCommand(command, registriesContainer));
            else if (command.StartsWith("hlf"))
                parsedCommands.Add(new HalfCommand(command, registriesContainer));
            else if (command.StartsWith("jmp"))
                parsedCommands.Add(new JumpCommand(command, registriesContainer));
            else if (command.StartsWith("jie"))
                parsedCommands.Add(new JumpIfEvenCommand(command, registriesContainer));
            else if (command.StartsWith("jio"))
                parsedCommands.Add(new JumpIfOneCommand(command, registriesContainer));
            else
                throw new ArgumentException($"Unknown command, check input data: \"{command}\"");
        }

        SimulateCommandsProcession();
        Console.WriteLine($"Part 1. Value in b register is {registriesContainer.Registries["b"]}");

        registriesContainer.ResetForPart2();
        SimulateCommandsProcession();
        Console.WriteLine($"Part 2. Now value in b register is {registriesContainer.Registries["b"]}");

        void SimulateCommandsProcession()
        {
            while (registriesContainer.Iterator < length)
                parsedCommands[registriesContainer.Iterator].Execute();
        }        
    }
}
