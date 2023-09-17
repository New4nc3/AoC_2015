namespace Day22;

class Program
{
    private static int _minManaSpent = int.MaxValue;
    private static List<string> _castedSpells = new();

    private const string _statsDelimiter = ": ";

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];

        string[] data;
        using (var streamReader = new StreamReader(inputFileName))
            data = streamReader.ReadToEnd().Split("\r\n");

        var bossHP = int.Parse(data[0].Split(_statsDelimiter)[1]);
        var bossDamage = int.Parse(data[1].Split(_statsDelimiter)[1]);

        if (inputFileName == "test.txt")
            SimulateTurn(10, 250, 0, bossHP, bossDamage, 0, 0, 0, 0, new List<string>(), false, false);
        else
            SimulateTurn(50, 500, 0, bossHP, bossDamage, 0, 0, 0, 0, new List<string>(), false, false);

        Console.WriteLine($"Part 1. {_minManaSpent} \n{string.Join(", ", _castedSpells)}");

        _minManaSpent = int.MaxValue;

        if (inputFileName == "test.txt")
            SimulateTurn(10, 250, 0, bossHP, bossDamage, 0, 0, 0, 0, new List<string>(), true, false);
        else
            SimulateTurn(50, 500, 0, bossHP, bossDamage, 0, 0, 0, 0, new List<string>(), true, false);

        Console.WriteLine($"Part 2. {_minManaSpent} \n{string.Join(", ", _castedSpells)}");
    }

    private static void SimulateTurn(
        int playerHP,
        int playerMana,
        int playerArmor,
        int bossHP,
        int bossDamage,
        int shieldDur,
        int poisonDur,
        int rechargeDur,
        int totalManaSpent,
        List<string> spells,
        bool isPart2,
        bool isBossTurn = true)
    {
        if (totalManaSpent > _minManaSpent)
            return;

        if (isPart2 && !isBossTurn && --playerHP == 0)
            return;

        if (poisonDur > 0)
        {
            --poisonDur;
            bossHP -= 3;

            if (bossHP <= 0)
            {
                // Console.WriteLine($"Boss dies from poison. Total spent mana: {totalManaSpent}" +
                //     $"\n\nList of casted spells: {string.Join(", ", spells)}");
                if (totalManaSpent < _minManaSpent)
                {
                    _minManaSpent = totalManaSpent;
                    _castedSpells = spells;
                }
                return;
            }
        }

        if (rechargeDur > 0)
        {
            --rechargeDur;
            playerMana += 101;
        }

        if (shieldDur > 0)
        {
            --shieldDur;
            playerArmor += 7;
        }

        if (isBossTurn)
        {
            var damageToDeal = Math.Max(bossDamage - playerArmor, 1);

            if (playerHP - damageToDeal <= 0)
            {
                //Console.WriteLine("Player dies from boss attack");
                return;
            }

            SimulateTurn(playerHP - damageToDeal, playerMana, playerArmor, bossHP, bossDamage, shieldDur, poisonDur, rechargeDur, totalManaSpent, spells, isPart2, false);
        }
        else
        {
            if (playerMana - 53 >= 0)
            {
                var tempSpells = new List<string>(spells) { "Magic Missile" };

                if (bossHP - 4 <= 0)
                {
                    // Console.WriteLine($"Boss dies from Magic Missile. Total spent mana: {totalManaSpent + 53}" +
                    //     $"\n\nList of casted spells: {string.Join(", ", tempSpells)}");
                    if (totalManaSpent + 53 < _minManaSpent)
                    {
                        _minManaSpent = totalManaSpent + 53;
                        _castedSpells = tempSpells;
                    }
                    return;
                }
                else
                    SimulateTurn(playerHP, playerMana - 53, playerArmor, bossHP - 4, bossDamage, shieldDur, poisonDur, rechargeDur, totalManaSpent + 53, tempSpells, isPart2);
            }

            if (playerMana - 73 >= 0)
            {
                var tempSpells = new List<string>(spells) { "Drain" };

                if (bossHP - 2 <= 0)
                {
                    // Console.WriteLine($"Boss dies from Drain. Total spent mana: {totalManaSpent + 73}" +
                    //     $"\n\nList of casted spells: {string.Join(", ", tempSpells)}");
                    if (totalManaSpent + 73 < _minManaSpent)
                    {
                        _minManaSpent = totalManaSpent + 73;
                        _castedSpells = tempSpells;
                    }
                    return;
                }
                else
                    SimulateTurn(playerHP + 2, playerMana - 73, playerArmor, bossHP - 2, bossDamage, shieldDur, poisonDur, rechargeDur, totalManaSpent + 73, tempSpells, isPart2);
            }

            if (shieldDur == 0 && playerMana - 113 >= 0)
                SimulateTurn(playerHP, playerMana - 113, playerArmor, bossHP, bossDamage, 6, poisonDur, rechargeDur, totalManaSpent + 113, new List<string>(spells) { "Shield" }, isPart2);

            if (poisonDur == 0 && playerMana - 173 >= 0)
                SimulateTurn(playerHP, playerMana - 173, playerArmor, bossHP, bossDamage, shieldDur, 6, rechargeDur, totalManaSpent + 173, new List<string>(spells) { "Poison" }, isPart2);

            if (rechargeDur == 0 && playerMana - 229 >= 0)
                SimulateTurn(playerHP, playerMana - 229, playerArmor, bossHP, bossDamage, shieldDur, poisonDur, 5, totalManaSpent + 229, new List<string>(spells) { "Recharge" }, isPart2);
        }
    }
}
