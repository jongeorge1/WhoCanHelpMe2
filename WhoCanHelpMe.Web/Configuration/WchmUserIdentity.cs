namespace WhoCanHelpMe.Web.Configuration
{
    using System.Collections.Generic;

    using Nancy.Security;

    using WhoCanHelpMe.Domain;

    public class WchmUserIdentity : IUserIdentity
    {
        public WchmUserIdentity(User user)
        {
            this.UserName = user.EmailAddress;
            this.Claims = new string[0];
        }

        public string UserName { get; private set; }

        public IEnumerable<string> Claims { get; private set; }
    }
}