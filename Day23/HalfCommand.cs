class HalfCommand : BaseCommand
{
    public HalfCommand(string command, RegistriesContainer container) : base(command, container) { }

    public override void Execute()
    {
        _container.Registries[_param] /= 2;
        ++_container.Iterator;
    }
}
