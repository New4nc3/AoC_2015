class JumpIfEvenCommand : BaseCommand
{
    private readonly string[] _delimiters = new string[] { " ", ", " };

    protected readonly int _offset;

    public JumpIfEvenCommand(string command, RegistriesContainer container) : base(command, container) =>
        _offset = int.Parse(command.Split(_delimiters, StringSplitOptions.None)[2]);

    protected override string ExtractParamFromCommand(string command) =>
        command.Split(_delimiters, StringSplitOptions.None)[1];

    public override void Execute()
    {
        if (_container.Registries[_param] % 2 == 0)
            _container.Iterator += _offset;
        else
            ++_container.Iterator;
    }
}
