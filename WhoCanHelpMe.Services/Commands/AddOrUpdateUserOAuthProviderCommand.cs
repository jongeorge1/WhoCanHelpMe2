namespace WhoCanHelpMe.Services.Commands
{
    using System;

    using WhoCanHelpMe.Common.Commands;

    public class AddOrUpdateUserOAuthProviderCommand : ICommand
    {
        public Guid UserId { get; set; }

        public string ProviderName { get; set; }

        public string ProviderId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }
    }
}