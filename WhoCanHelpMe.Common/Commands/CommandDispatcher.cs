namespace WhoCanHelpMe.Common.Commands
{
    using System.Linq;

    using Ninject;

    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IKernel serviceLocator;

        public CommandDispatcher(IKernel serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public void Dispatch<T>(T args) where T : ICommand
        {
            if (this.serviceLocator != null)
            {
                var handlers = this.serviceLocator.GetAll<IHandles<T>>().ToArray();

                if (handlers.Length == 0)
                {
                    throw new MissingHandlerException();
                }

                foreach (var handler in handlers)
                {
                    handler.Handle(args);
                }
            }
        }
    }
}