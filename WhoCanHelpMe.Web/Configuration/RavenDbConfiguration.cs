namespace WhoCanHelpMe.Web.Configuration
{
    using Nancy.Bootstrapper;

    using Ninject;

    using Raven.Abstractions.Exceptions;
    using Raven.Client;
    using Raven.Client.Document;
    using Raven.Client.Indexes;

    using WhoCanHelpMe.Services.Indexes;

    public static class RavenDbConfiguration
    {
        public static void ConfigureSession(IKernel container)
        {
            var store = container.Get<IDocumentStore>();
            var documentSession = store.OpenSession();
            container.Bind<IDocumentSession>().ToConstant(documentSession);
        }

        public static void ConfigureStore(IKernel container)
        {
            container.Bind<IDocumentStore>().ToConstant(GetDocumentStore());
        }

        public static void EnsureSessionPersistence(IKernel container, IPipelines pipelines)
        {
            pipelines.AfterRequest += ctx =>
                {
                    try
                    {
                        container.Get<IDocumentSession>().SaveChanges();
                    }
                    catch (ConcurrencyException)
                    {
                        // raven thinks there is a concurrent document update, and doesn't know what the last one is, so it picks one and throws on the other.
                        // todo: should we disable optimistic concurrency and let last write win? or implement some kind of throttle/queue?
                    }
                };
        }

        private static IDocumentStore GetDocumentStore()
        {
            var documentStore = new DocumentStore { ConnectionStringName = "raven" }.Initialize();

            if (!documentStore.Identifier.Contains("fakedb"))
            {
                IndexCreation.CreateIndexes(typeof(IRavenDbIndexMarker).Assembly, documentStore);
            }

            return documentStore;
        }
    }
}