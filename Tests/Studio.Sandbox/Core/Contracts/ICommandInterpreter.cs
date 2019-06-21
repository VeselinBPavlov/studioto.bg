namespace Studio.Sandbox.Core.Contracts
{
    public interface ICommandInterpreter
    {
        string Read(string[] input);
    }
}
