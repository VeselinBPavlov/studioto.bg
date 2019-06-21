namespace Studio.Sandbox.Core.Contracts
{
    public interface ICommand
    {
        string Execute(string[] args);
    }
}
