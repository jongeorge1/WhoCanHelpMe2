namespace WhoCanHelpMe.Web.Configuration
{
    using System;

    using Nancy;
    using Nancy.Authentication.Forms;
    using Nancy.Security;

    using Raven.Client;

    using WhoCanHelpMe.Domain;

    public class UserMapper : IUserMapper
    {
        private readonly IDocumentSession session;

        public UserMapper(IDocumentSession session)
        {
            this.session = session;
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var user = this.session.Load<User>(identifier);

            return user != null ? new WchmUserIdentity(user) : null;
        }
    }
}