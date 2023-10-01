class IncrementCommand : BaseCommand
{
    public IncrementCommand(string command, RegistriesContainer container) : base(command, container) { }

    public override void Execute()
    {
        ++_container.Registries[_param];
        ++_container.Iterator;
    }
}
