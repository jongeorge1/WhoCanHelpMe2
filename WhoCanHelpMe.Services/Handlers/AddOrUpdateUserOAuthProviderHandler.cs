namespace WhoCanHelpMe.Services.Handlers
{
    using Raven.Client;

    using WhoCanHelpMe.Common.Commands;
    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Services.Commands;
    using WhoCanHelpMe.Services.Exceptions;

    public class AddOrUpdateUserOAuthProviderHandler : IHandles<AddOrUpdateUserOAuthProviderCommand>
    {
        private readonly IDocumentSession session;

        public AddOrUpdateUserOAuthProviderHandler(IDocumentSession session)
        {
            this.session = session;
        }

        public void Handle(AddOrUpdateUserOAuthProviderCommand arg)
        {
            var user = this.session.Load<User>(arg.UserId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var existingProvider = user.GetProvider(arg.ProviderName);

            if (existingProvider == null)
            {
                user.AddOAuthProvider(arg.ProviderName, arg.ProviderId, arg.Email, arg.Name, arg.PictureUrl);
            }
            else
            {
                existingProvider.Update(arg.Name, arg.PictureUrl);
            }

            // We don't normally need to call this manually, but it's highly likely that the user will be re-requested
            // within the same request for logon purposes, so...
            this.session.SaveChanges();
        }
    }
}