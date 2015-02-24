namespace WhoCanHelpMe.Web
{
    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Bootstrappers.Ninject;
    using Nancy.Cryptography;

    using Ninject;

    using WhoCanHelpMe.Web.Configuration;

    public class Bootstrapper : NinjectNancyBootstrapper
    {
        protected override CryptographyConfiguration CryptographyConfiguration
        {
            get
            {
                return CryptographyConfigurationHelper.BuildCryptographyConfiguration();
            }
        }

        protected override void ApplicationStartup(IKernel container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            AutomapperConfiguration.CreateMaps();
        }

        protected override void ConfigureApplicationContainer(IKernel container)
        {
            base.ConfigureApplicationContainer(container);
            RavenDbConfiguration.ConfigureStore(container);
            ServiceConfiguration.ConfigureApplicationServices(container);
        }

        protected override void ConfigureRequestContainer(IKernel container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            RavenDbConfiguration.ConfigureSession(container);
            AuthConfiguration.ConfigureRequest(container);
            ServiceConfiguration.ConfigureRequestServices(container);
        }

        protected override void RequestStartup(IKernel container, IPipelines pipelines, NancyContext context)
        {
            AuthConfiguration.ConfigureFormsAuth(container, pipelines);
            RavenDbConfiguration.EnsureSessionPersistence(container, pipelines);
        }
    }
}