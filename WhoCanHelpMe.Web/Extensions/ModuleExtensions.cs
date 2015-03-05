namespace WhoCanHelpMe.Web.Extensions
{
    using Nancy;

    using WhoCanHelpMe.Web.Configuration;

    public static class ModuleExtensions
    {
        public static WchmUserIdentity CurrentWchmUser(this NancyModule module)
        {
            return module.Context.CurrentUser as WchmUserIdentity;
        }
    }
}