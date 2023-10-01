class RegistriesContainer
{
    public readonly Dictionary<string, uint> Registries = new()
    {
        { "a", 0 },
        { "b", 0 },
    };

    public int Iterator { get; set; }

    public void ResetForPart2()
    {
        Iterator = 0;
        Registries["a"] = 1;
        Registries["b"] = 0;
    }
}
