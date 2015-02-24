namespace WhoCanHelpMe.Web.Modules
{
    using Nancy;
    using Nancy.Security;

    public class ProfileModule : NancyModule
    {
        public ProfileModule()
        {
            this.Get["/profile"] = this.GetProfile;
        }

        private object GetProfile(object arg)
        {
            this.RequiresAuthentication();

            return HttpStatusCode.OK;
        }
    }
}