namespace WhoCanHelpMe.Web.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ninject;
    using Ninject.Extensions.Conventions;

    using WhoCanHelpMe.Common.Commands;
    using WhoCanHelpMe.Services.Handlers;
    using WhoCanHelpMe.Services.Queries;

    public static class ServiceConfiguration
    {
        private static List<Tuple<Type, Type>> commandHandlers;

        private static List<Tuple<Type, Type>> queries;

        public static void ConfigureApplicationServices(IKernel container)
        {
            commandHandlers = FindMarkedTypes(typeof(ICommandHandlersMarker));
            queries = FindMarkedTypes(typeof(IQueriesMarker));
        }

        public static void ConfigureRequestServices(IKernel container)
        {
            InstallTypes(container, commandHandlers);
            InstallTypes(container, queries);

            container.Bind<ICommandDispatcher>().ToConstant(new CommandDispatcher(container));
        }

        private static List<Tuple<Type, Type>> FindMarkedTypes(Type marker)
        {
            var candidateTypes = marker.Assembly.GetExportedTypes();
            var validTypes = candidateTypes.Where(x => x.Namespace == marker.Namespace && x != marker && !x.IsInterface);

            return validTypes.Select(x => new Tuple<Type, Type>(x.GetInterfaces()[0], x)).ToList();
        }

        private static void InstallTypes(IKernel container, IEnumerable<Tuple<Type, Type>> types)
        {
            foreach (var current in types)
            {
                container.Bind(current.Item1).To(current.Item2);
            }
        }
    }
}