abstract class BaseCommand
{
    protected const string _spaceDelimiter = " ";

    protected readonly string _param;
    protected readonly RegistriesContainer _container;

    public BaseCommand(string command, RegistriesContainer container)
    {
        _param = ExtractParamFromCommand(command);
        _container = container;
    }

    protected virtual string ExtractParamFromCommand(string command) =>
        command.Split(_spaceDelimiter)[1];

    public abstract void Execute();
}
