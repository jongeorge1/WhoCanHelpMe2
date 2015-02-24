namespace WhoCanHelpMe.Services.Commands
{
    using WhoCanHelpMe.Common.Commands;

    public class RegisterUserFromOAuthProviderCommand : ICommand
    {
        public string ProviderName { get; set; }

        public string ProviderId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }
    }
}