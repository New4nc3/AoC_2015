class JumpIfOneCommand : JumpIfEvenCommand
{
    public JumpIfOneCommand(string command, RegistriesContainer container) : base(command, container) { }

    public override void Execute()
    {
        if (_container.Registries[_param] == 1)
            _container.Iterator += _offset;
        else
            ++_container.Iterator;
    }
}
