namespace WhoCanHelpMe.Services.Handlers
{
    using System;

    using Raven.Client;

    using WhoCanHelpMe.Common.Commands;
    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Services.Commands;

    public class AddAssertionToProfileHandler : IHandles<AddAssertionToProfileCommand>
    {
        private readonly IDocumentSession session;

        public AddAssertionToProfileHandler(IDocumentSession session)
        {
            this.session = session;
        }

        public void Handle(AddAssertionToProfileCommand arg)
        {
            var user = this.session.Load<User>(arg.UserId);

            user.AddAssertion(arg.Category, arg.Tag);
        }
    }
}