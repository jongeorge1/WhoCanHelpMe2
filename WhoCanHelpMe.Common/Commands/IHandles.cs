namespace WhoCanHelpMe.Common.Commands
{
    public interface IHandles<in T>
        where T : ICommand
    {
        void Handle(T arg);
    }
}