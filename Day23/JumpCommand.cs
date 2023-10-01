class JumpCommand : BaseCommand
{
    private readonly int _offset;

    public JumpCommand(string command, RegistriesContainer container) : base(command, container) =>
        _offset = int.Parse(_param);

    public override void Execute() =>
        _container.Iterator += _offset;
}
