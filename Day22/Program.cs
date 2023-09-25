namespace Day22;

class Program
{
    private static int _minManaSpent = int.MaxValue;
    private static int _bossDamage = 0;
    private static List<string> _castedSpells = new();

    private const string _statsDelimiter = ": ";

    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];

        string[] data;
        using (var streamReader = new StreamReader(inputFileName))
            data = streamReader.ReadToEnd().Split("\r\n");

        var bossHP = int.Parse(data[0].Split(_statsDelimiter)[1]);
        _bossDamage = int.Parse(data[1].Split(_statsDelimiter)[1]);

        if (inputFileName == "test.txt")
            SimulateTurn(10, 250, 0, bossHP, 0, 0, 0, 0, new List<string>(), false, false);
        else
            SimulateTurn(50, 500, 0, bossHP, 0, 0, 0, 0, new List<string>(), false, false);

        Console.WriteLine($"Part 1. Min mana spent: {_minManaSpent} \n{string.Join(", ", _castedSpells)}");

        _minManaSpent = int.MaxValue;
        _castedSpells = new();

        if (inputFileName == "test.txt")
            SimulateTurn(10, 250, 0, bossHP, 0, 0, 0, 0, new List<string>(), true, false);
        else
            SimulateTurn(50, 500, 0, bossHP, 0, 0, 0, 0, new List<string>(), true, false);

        Console.WriteLine($"Part 2. {_minManaSpent} \n{string.Join(", ", _castedSpells)}");
    }

    private static void SimulateTurn(
        int playerHP,
        int playerMana,
        int playerArmor,
        int bossHP,
        int shieldDur,
        int poisonDur,
        int rechargeDur,
        int totalManaSpent,
        List<string> spells,
        bool isPart2,
        bool isBossTurn = true)
    {
        if (totalManaSpent >= _minManaSpent && isBossTurn)
            return;

        if (isPart2 && !isBossTurn && --playerHP == 0)
            return;

        if (poisonDur > 0)
        {
            --poisonDur;
            bossHP -= 3;

            if (bossHP <= 0)
            {
                if (totalManaSpent < _minManaSpent)
                {
                    _minManaSpent = totalManaSpent;
                    _castedSpells = new List<string>(spells) { $"Poison kills Boss" };
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
            playerArmor = 7;
        }
        else
            playerArmor = 0;

        if (isBossTurn)
        {
            var damageToDeal = Math.Max(_bossDamage - playerArmor, 1);

            if (playerHP - damageToDeal <= 0)
                return;

            var tempSpells = new List<string>(spells) { $"Boss deals {damageToDeal} damage ({playerHP - damageToDeal}; {playerArmor}; {playerMana}; {bossHP})" };
            SimulateTurn(playerHP - damageToDeal, playerMana, playerArmor, bossHP, shieldDur, poisonDur, rechargeDur, totalManaSpent, tempSpells, isPart2, false);
        }
        else
        {
            if (playerMana - 53 >= 0)
            {
                var tempSpells = new List<string>(spells) { "Magic Missile" };

                if (bossHP - 4 <= 0)
                {
                    if (totalManaSpent + 53 < _minManaSpent)
                    {
                        _minManaSpent = totalManaSpent + 53;
                        _castedSpells = new List<string>(spells) { $"Magic Missile kills Boss ({playerHP}; {playerArmor}; {playerMana - 53}; {bossHP - 4})" };;
                    }

                    return;
                }
                else
                    SimulateTurn(playerHP, playerMana - 53, playerArmor, bossHP - 4, shieldDur, poisonDur, rechargeDur, totalManaSpent + 53, tempSpells, isPart2);
            }

            if (playerMana - 73 >= 0)
            {
                var tempSpells = new List<string>(spells) { "Drain" };

                if (bossHP - 2 <= 0)
                {
                    if (totalManaSpent + 73 < _minManaSpent)
                    {
                        _minManaSpent = totalManaSpent + 73;
                        _castedSpells = new List<string>(spells) { $"Drain kills Boss ({playerHP + 2}; {playerArmor}; {playerMana - 73}; {bossHP - 2})" };;
                    }

                    return;
                }
                else
                    SimulateTurn(playerHP + 2, playerMana - 73, playerArmor, bossHP - 2, shieldDur, poisonDur, rechargeDur, totalManaSpent + 73, tempSpells, isPart2);
            }

            if (shieldDur == 0 && playerMana - 113 >= 0)
                SimulateTurn(playerHP, playerMana - 113, playerArmor, bossHP, 6, poisonDur, rechargeDur, totalManaSpent + 113, new List<string>(spells) { "Shield" }, isPart2);

            if (poisonDur == 0 && playerMana - 173 >= 0)
                SimulateTurn(playerHP, playerMana - 173, playerArmor, bossHP, shieldDur, 6, rechargeDur, totalManaSpent + 173, new List<string>(spells) { "Poison" }, isPart2);

            if (rechargeDur == 0 && playerMana - 229 >= 0)
                SimulateTurn(playerHP, playerMana - 229, playerArmor, bossHP, shieldDur, poisonDur, 5, totalManaSpent + 229, new List<string>(spells) { "Recharge" }, isPart2);
        }
    }
}
