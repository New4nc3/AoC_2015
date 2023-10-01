class TripleCommand : BaseCommand
{
    public TripleCommand(string command, RegistriesContainer container) : base(command, container) { }

    public override void Execute()
    {
        _container.Registries[_param] *= 3;
        ++_container.Iterator;
    }
}
