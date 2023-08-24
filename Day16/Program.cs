namespace Day16;

class Program
{
    private const string _nameDelimiter = ": ";
    private const string _compoundsDelimiter = ", ";
    private const string _children = "children";
    private const string _cats = "cats";
    private const string _samoyeds = "samoyeds";
    private const string _pomeranians = "pomeranians";
    private const string _akitas = "akitas";
    private const string _vizslas = "vizslas";
    private const string _goldfish = "goldfish";
    private const string _trees = "trees";
    private const string _cars = "cars";
    private const string _perfumes = "perfumes";

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "input.txt" : args[0];
        using var streamReader = new StreamReader(inputFileName);
        int children, cats, samoyeds, pomeranians, akitas, vizslas, goldfish, trees, cars, perfumes;
        var aunts = new List<AuntSue>();
        var auntInfo = new AuntSue(-1, 3, 7, 2, 3, 0, 0, 5, 3, 2, 1);

        string? data;
        while ((data = streamReader.ReadLine()) != null)
        {
            var parts = data.Remove(0, 4).Split(new string[] { _nameDelimiter, _compoundsDelimiter }, StringSplitOptions.RemoveEmptyEntries);
            children = cats = samoyeds = pomeranians = akitas = vizslas = goldfish = trees = cars = perfumes = -1;

            var number = int.Parse(parts[0]);
            ProcessPair(parts[1], int.Parse(parts[2]));
            ProcessPair(parts[3], int.Parse(parts[4]));
            ProcessPair(parts[5], int.Parse(parts[6]));
            aunts.Add(new AuntSue(number, children, cats, samoyeds, pomeranians, akitas, vizslas, goldfish, trees, cars, perfumes));
        }

        var aunt1 = aunts.First(x =>
            (x.Children == auntInfo.Children || x.Children == -1) &&
            (x.Cats == auntInfo.Cats || x.Cats == -1) &&
            (x.Samoyeds == auntInfo.Samoyeds || x.Samoyeds == -1) &&
            (x.Pomeranians == auntInfo.Pomeranians || x.Pomeranians == -1) &&
            (x.Akitas == auntInfo.Akitas || x.Akitas == -1) &&
            (x.Vizslas == auntInfo.Vizslas || x.Vizslas == -1) &&
            (x.Goldfish == auntInfo.Goldfish || x.Goldfish == -1) &&
            (x.Trees == auntInfo.Trees || x.Trees == -1) &&
            (x.Cars == auntInfo.Cars || x.Cars == -1) &&
            (x.Perfumes == auntInfo.Perfumes || x.Perfumes == -1));

        var aunt2 = aunts.First(x =>
            (x.Children == auntInfo.Children || x.Children == -1) &&
            (x.Cats > auntInfo.Cats || x.Cats == -1) &&
            (x.Samoyeds == auntInfo.Samoyeds || x.Samoyeds == -1) &&
            (x.Pomeranians < auntInfo.Pomeranians || x.Pomeranians == -1) &&
            (x.Akitas == auntInfo.Akitas || x.Akitas == -1) &&
            (x.Vizslas == auntInfo.Vizslas || x.Vizslas == -1) &&
            (x.Goldfish < auntInfo.Goldfish || x.Goldfish == -1) &&
            (x.Trees > auntInfo.Trees || x.Trees == -1) &&
            (x.Cars == auntInfo.Cars || x.Cars == -1) &&
            (x.Perfumes == auntInfo.Perfumes || x.Perfumes == -1));

        Console.WriteLine($"Part 1. Aunt number is {aunt1.Number}");
        Console.WriteLine($"Part 2. Now, aunt number is {aunt2.Number}");

        void ProcessPair(string name, int value)
        {
            switch (name)
            {
                case _children:
                    children = value;
                    break;

                case _cats:
                    cats = value;
                    break;

                case _samoyeds:
                    samoyeds = value;
                    break;

                case _pomeranians:
                    pomeranians = value;
                    break;

                case _akitas:
                    akitas = value;
                    break;

                case _vizslas:
                    vizslas = value;
                    break;

                case _goldfish:
                    goldfish = value;
                    break;

                case _trees:
                    trees = value;
                    break;

                case _cars:
                    cars = value;
                    break;

                case _perfumes:
                    perfumes = value;
                    break;

                default:
                    throw new ArgumentException($"Unknown property: \"{name}\"");
            }
        }
    }
}
