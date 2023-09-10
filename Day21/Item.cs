class Item
{
    public string Name { get; }
    public int Cost { get; }
    public int Damage { get; }
    public int Armor { get; }

    public Item(string name, int cost, int damage, int armor)
    {
        Name = name;
        Cost = cost;
        Damage = damage;
        Armor = armor;
    }

    public override string ToString() =>
        $"{Name}\t\t{Cost}\t{Damage}\t{Armor}";
}
