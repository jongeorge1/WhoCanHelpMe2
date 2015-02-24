namespace WhoCanHelpMe.Web.Modules
{
    using Nancy;
    using Nancy.Authentication.Forms;

    public class StaticPageModule : NancyModule
    {
        public StaticPageModule()
        {
            this.Get["/"] = this.GetHome;
            this.Get["/about"] = this.GetAbout;
            this.Get["/login"] = this.GetLogin;
            this.Get["/sign-out"] = this.GetSignOut;
        }

        private object GetSignOut(object arg)
        {
            return this.LogoutAndRedirect("/");
        }

        private object GetLogin(object arg)
        {
            return this.View["Login"];
        }

        private object GetAbout(object arg)
        {
            return this.View["About"];
        }

        private object GetHome(object arg)
        {
            return this.View["Home"];
        }
    }
}