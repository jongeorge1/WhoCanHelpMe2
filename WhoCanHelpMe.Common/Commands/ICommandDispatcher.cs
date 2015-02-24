namespace WhoCanHelpMe.Common.Commands
{
    public interface ICommandDispatcher
    {
        void Dispatch<T>(T args) where T : ICommand;
    }
}