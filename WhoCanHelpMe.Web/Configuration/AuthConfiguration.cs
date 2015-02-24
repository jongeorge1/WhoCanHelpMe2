namespace WhoCanHelpMe.Web.Configuration
{
    using System;
    using System.Configuration;

    using Nancy;
    using Nancy.Authentication.Forms;
    using Nancy.Bootstrapper;
    using Nancy.Cryptography;
    using Nancy.Helpers;
    using Nancy.SimpleAuthentication;

    using Ninject;

    using WhoCanHelpMe.Web.Modules;

    public class AuthConfiguration
    {
        public const string FormsAuthCookieName = "_wchm";
        
        public static void ConfigureFormsAuth(IKernel container, IPipelines pipelines)
        {
            var formsAuthConfig = new FormsAuthenticationConfiguration
            {
                CryptographyConfiguration = container.Get<CryptographyConfiguration>(),
                UserMapper = container.Get<IUserMapper>(),
                DisableRedirect = false,
                RedirectUrl = "/login"
            };

            FormsAuthentication.FormsAuthenticationCookieName = FormsAuthCookieName;
            FormsAuthentication.Enable(pipelines, formsAuthConfig);
            pipelines.AfterRequest += EnsureSlidingExpiry;
        }

        public static void ConfigureRequest(IKernel container)
        {
            container.Bind<IAuthenticationCallbackProvider>().To<OAuthCallback>();
            container.Bind<IUserMapper>().To<UserMapper>();
        }

        public static void EnsureSlidingExpiry(NancyContext context)
        {
            var copyExistingCookie = context.Request.Cookies.ContainsKey(FormsAuthCookieName)
                                     && !context.Request.Path.Contains("/sign-out") && context.CurrentUser != null;
            if (copyExistingCookie)
            {
                var formsAuthCookie = HttpUtility.UrlDecode(context.Request.Cookies[FormsAuthCookieName]);
                var sessionLength = int.Parse(ConfigurationManager.AppSettings["userSessionDurationInMinutes"]);
                context.Response.WithCookie(
                    FormsAuthCookieName, 
                    formsAuthCookie, 
                    DateTime.UtcNow.AddMinutes(sessionLength));
            }
        }
    }
}