namespace WhoCanHelpMe.Web.Modules
{
    using System.Diagnostics;

    using Nancy;
    using Nancy.Authentication.Forms;
    using Nancy.Responses;
    using Nancy.SimpleAuthentication;

    using WhoCanHelpMe.Common.Commands;
    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Services.Commands;
    using WhoCanHelpMe.Services.Queries;

    public class OAuthCallback : IAuthenticationCallbackProvider
    {
        private readonly ICommandDispatcher dispatcher;

        private readonly IUserQueries userQueries;

        public OAuthCallback(ICommandDispatcher dispatcher, IUserQueries userQueries)
        {
            this.dispatcher = dispatcher;
            this.userQueries = userQueries;
        }

        public dynamic Process(NancyModule nancyModule, AuthenticateCallbackData model)
        {
            if (model == null || model.AuthenticatedClient == null || model.Exception != null)
            {
                // Fail. Hand back to the long page.
                Debug.WriteLine("Could not process OAuth callback");
                if (model != null && model.Exception != null)
                {
                    Debug.WriteLine(model.Exception.ToString());
                }

                return new RedirectResponse("/login");
            }

            // Get user with matching email address.
            var user = this.userQueries.GetByEmailAddress(model.AuthenticatedClient.UserInformation.Email);

            // If the user doesn't exist, we are registering them...
            if (user == null)
            {
                this.RegisterUser(model);
            }
            else
            {
                this.AddOrUpdateUserProvider(model, user);
            }

            user = this.userQueries.GetByEmailAddress(model.AuthenticatedClient.UserInformation.Email);

            return nancyModule.LoginAndRedirect(user.Id, fallbackRedirectUrl: "/profile");
        }

        public dynamic OnRedirectToAuthenticationProviderError(NancyModule nancyModule, string errorMessage)
        {
            Debug.WriteLine("OAuthCallback Failure - " + errorMessage);

            return new RedirectResponse("/login");
        }

        private void AddOrUpdateUserProvider(AuthenticateCallbackData model, User user)
        {
            var userInfo = model.AuthenticatedClient.UserInformation;

            var command = new AddOrUpdateUserOAuthProviderCommand
            {
                UserId = user.Id,
                ProviderName = model.AuthenticatedClient.ProviderName,
                ProviderId = userInfo.Id,
                Email = userInfo.Email,
                Name = userInfo.Name,
                PictureUrl = userInfo.Picture
            };

            this.dispatcher.Dispatch(command);
        }

        private void RegisterUser(AuthenticateCallbackData model)
        {
            var userInfo = model.AuthenticatedClient.UserInformation;

            var command = new RegisterUserFromOAuthProviderCommand
            {
                ProviderName =
                    model.AuthenticatedClient.ProviderName,
                Email = userInfo.Email,
                ProviderId = userInfo.Id,
                Name = userInfo.Name,
                PictureUrl = userInfo.Picture
            };

            this.dispatcher.Dispatch(command);
        }
    }
}