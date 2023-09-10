namespace Day21;

class Program
{
    private const string _player = "Player";
    private const string _statsDelimiter = ": ";

    private static readonly List<Item> Weapons = new()
    {
        new Item("Dagger Weapon", 8, 4, 0),
        new Item("Shortsword", 10, 5, 0),
        new Item("Warhammer", 25, 6, 0),
        new Item("Longsword", 40, 7, 0),
        new Item("Greataxe", 74, 8, 0),
    };

    private static readonly List<Item> Armors = new()
    {
        new Item("Without Armor", 0, 0, 0),
        new Item("Leather", 13, 0, 1),
        new Item("Chainmail", 31, 0, 2),
        new Item("Splintmail", 53, 0, 3),
        new Item("Bandedmail", 75, 0, 4),
        new Item("Platemail", 102, 0, 5),
    };

    private static readonly List<Item> Rings = new()
    {
        new Item("Empty Ring Slot", 0, 0, 0),
        new Item("Damage +1", 25, 1, 0),
        new Item("Damage +2", 50, 2, 0),
        new Item("Damage +3", 100, 3, 0),
        new Item("Defense +1", 20, 0, 1),
        new Item("Defense +2", 40, 0, 2),
        new Item("Defense +3", 80, 0, 3),
    };

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        string[] data;

        using (var streamReader = new StreamReader(inputFileName))
            data = streamReader.ReadToEnd().Split("\r\n");

        var bossHp = int.Parse(data[0].Split(_statsDelimiter)[1]);
        var bossDamage = int.Parse(data[1].Split(_statsDelimiter)[1]);
        var bossArmor = int.Parse(data[2].Split(_statsDelimiter)[1]);
        var boss = new Character("Boss", bossHp, bossDamage, bossArmor);
        var player = inputFileName == "test.txt" ? new Character(_player, 8, 5, 5) : new Character(_player, 100, 0, 0);

        var allItemSets = Weapons.SelectMany(weapon =>
                Armors.SelectMany(armor =>
                Rings.SelectMany(ring1 =>
                Rings.Where(x => x != ring1 || x.Cost == 0)
            .Select(ring2 => new ItemSet(new List<Item> { weapon, armor, ring1, ring2 })))));

        var cheapestSetToWin = allItemSets.Where(x => PlayerWinBattle(boss, player, x)).OrderBy(x => x.TotalCost).First();
        var mostExpensiveSetToLose = allItemSets.Where(x => !PlayerWinBattle(boss, player, x)).OrderByDescending(x => x.TotalCost).First();

        Console.WriteLine($"Part 1. The cheapest set where player still win costs {cheapestSetToWin.TotalCost}.\n{cheapestSetToWin}");
        Console.WriteLine($"\nPart 2. The most expensive set for player to lose costs {mostExpensiveSetToLose.TotalCost}.\n{mostExpensiveSetToLose}");
    }

    private static bool PlayerWinBattle(Character initialBoss, Character initialPlayer, ItemSet playerItems)
    {
        var player = new Character(_player, initialPlayer.HitPoints, playerItems);
        var boss = new Character(initialBoss.Name, initialBoss.HitPoints, initialBoss.Damage, initialBoss.Armor);
        var playerRawDamage = Math.Max(player.Damage - boss.Armor, 1);
        var bossRawDamage = Math.Max(boss.Damage - player.Armor, 1);

        while (true)
        {
            if (boss.DieAfterTakingDamage(playerRawDamage))
                return true;
            
            if (player.DieAfterTakingDamage(bossRawDamage))
                return false;
        }
    }
}
