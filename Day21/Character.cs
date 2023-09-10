class Character
{
    public string Name { get; }
    public int HitPoints { get; private set; }
    public int Damage { get; }
    public int Armor { get; }

    public Character(string name, int hp, int damage, int armor)
    {
        Name = name;
        HitPoints = hp;
        Damage = damage;
        Armor = armor;
    }

    public Character(string name, int hp, ItemSet items)
        : this(name, hp, items.TotalDamage, items.TotalArmor) { }

    public bool DieAfterTakingDamage(int damage)
    {
        HitPoints -= damage;
        return HitPoints <= 0;
    }

    public override string ToString() =>
        $"{Name}\t{HitPoints} HP; {Damage} Attack; {Armor} Armor";
}
