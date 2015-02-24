namespace WhoCanHelpMe.Services.Handlers
{
    using Raven.Client;

    using WhoCanHelpMe.Common.Commands;
    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Services.Commands;
    using WhoCanHelpMe.Services.Queries;

    public class RegisterUserFromOAuthProviderHandler : IHandles<RegisterUserFromOAuthProviderCommand>
    {
        private readonly IDocumentSession session;

        public RegisterUserFromOAuthProviderHandler(IDocumentSession session)
        {
            this.session = session;
        }

        public void Handle(RegisterUserFromOAuthProviderCommand arg)
        {
            var user = new User(arg.ProviderName, arg.Email, arg.ProviderId, arg.Name, arg.PictureUrl);
            this.session.Store(user);

            // We don't normally need to call this manually, but it's highly likely that the user will be re-requested
            // within the same request for logon purposes, so...
            this.session.SaveChanges();
        }
    }
}