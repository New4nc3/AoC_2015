class ItemSet
{
    public List<Item> Items { get; }
    public int TotalCost { get; }
    public int TotalDamage { get; }
    public int TotalArmor { get; }

    public ItemSet(IEnumerable<Item> items)
    {
        Items = new List<Item>(items);
        TotalCost = items.Sum(x => x.Cost);
        TotalDamage = items.Sum(x => x.Damage);
        TotalArmor = items.Sum(x => x.Armor);
    }

    public override string ToString()
    {
        var output = "Item name\t\tCost\tDamage\tArmor\n--------------------------------\n";
        output += string.Join("\n", Items);
        output += $"\n--------------------------------\nTotal:\t\t\t{TotalCost}\t{TotalDamage}\t{TotalArmor}";
        return output;
    }
}
