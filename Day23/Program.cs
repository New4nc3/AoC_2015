namespace Day23;

class Program
{
    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        string[] rawCommands = File.ReadAllLines(inputFileName);
        Dictionary<string, uint> registries = new Dictionary<string, uint>
        {
            { "a", 0 },
            { "b", 0 },
        };
        //uint a = 0, b = 0;
        int i = 0, length = rawCommands.Length;

        while (i < length)
        {
            var command = rawCommands[i];

            if (command.StartsWith("inc"))
            {
                var reg = command.Split(" ")[1];
                ++registries[reg];
                ++i;
            }
            else if (command.StartsWith("tpl"))
            {
                var reg = command.Split(" ")[1];
                registries[reg] *= 3;
                ++i;
            }
            else if (command.StartsWith("hlf"))
            {
                var reg = command.Split(" ")[1];
                registries[reg] /= 2;
                ++i;
            }
            else if (command.StartsWith("jmp"))
            {
                // to do 
            }
        }

        Console.WriteLine($"Part 1. Value in b register is {registries["b"]}");
    }
}
