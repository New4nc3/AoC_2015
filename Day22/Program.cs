namespace Day22;

class Program
{
    static void Main(string[] args)
    {
        var inputFileName = args.Length == 0 ? "test.txt" : args[0];
        SimulateTurn(10, 250, 0, 13, 8, 0, 0, 0, 0, new List<string>(), false);
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
        bool isBossTurn = true)
    {
        if (poisonDur > 0)
        {
            --poisonDur;
            bossHP -= 3;

            if (bossHP <= 0)
            {
                Console.WriteLine($"Boss dies from poison. Total spent mana: {totalManaSpent}" +
                    $"\nList of casted spells:{string.Join("\r\n", spells)}");
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
                Console.WriteLine("Player dies from boss attack");
                return;
            }

            SimulateTurn(playerHP - damageToDeal, playerMana, playerArmor, bossHP, bossDamage, shieldDur, poisonDur, rechargeDur, totalManaSpent, spells, false);
        }
        else
        {
            if (playerMana - 53 >= 0)
            {
                var tempSpells = new List<string>(spells) { "Magic Missile" };

                if (bossHP - 4 <= 0)
                {
                    Console.WriteLine($"Boss dies from Magic Missile. Total spent mana: {totalManaSpent + 53}" +
                        $"\nList of casted spells:{string.Join("\r\n", tempSpells)}");
                    return;
                }
                else
                    SimulateTurn(playerHP, playerMana - 53, playerArmor, bossHP - 4, bossDamage, shieldDur, poisonDur, rechargeDur, totalManaSpent + 53, tempSpells);
            }

            if (playerMana - 73 >= 0)
            {
                var tempSpells = new List<string>(spells) { "Drain" };

                if (bossHP - 2 <= 0)
                {
                    Console.WriteLine($"Boss dies from Drain. Total spent mana: {totalManaSpent + 73}" +
                        $"\nList of casted spells:{string.Join("\r\n", tempSpells)}");
                    return;
                }
                else
                    SimulateTurn(playerHP + 2, playerMana - 73, playerArmor, bossHP - 2, bossDamage, shieldDur, poisonDur, rechargeDur, totalManaSpent + 73, tempSpells);
            }

            if (shieldDur == 0 && playerMana - 113 >= 0)
                SimulateTurn(playerHP, playerMana - 113, playerArmor, bossHP, bossDamage, 6, poisonDur, rechargeDur, totalManaSpent + 113, new List<string>(spells) { "Shield" });

            if (poisonDur == 0 && playerMana - 173 >= 0)
                SimulateTurn(playerHP, playerMana - 173, playerArmor, bossHP, bossDamage, shieldDur, 6, rechargeDur, totalManaSpent + 173, new List<string>(spells) { "Poison" });

            if (rechargeDur == 0 && playerMana - 229 >= 0)
                SimulateTurn(playerHP, playerMana - 229, playerArmor, bossHP, bossDamage, shieldDur, poisonDur, 5, totalManaSpent + 229, new List<string>(spells) { "Recharge" });
        }
    }
}
