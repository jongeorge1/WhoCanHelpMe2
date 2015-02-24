namespace WhoCanHelpMe.Services.Queries
{
    using System.Linq;

    using Raven.Client;

    using WhoCanHelpMe.Domain;

    public class UserQueries : IUserQueries
    {
        private readonly IDocumentSession documentSession;

        public UserQueries(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }

        public User GetByEmailAddress(string emailAddress)
        {
            // For pretty much all scenarios where we are retrieving a user by email address - e.g. ensure no
            // registered user with the same address, pulling a user to log them in - we need to be sure 
            // that we get the latest results. hence use of the WaitForNonStaleResults
            return this.documentSession.Query<User, Indexes.UserByEmailAddress>()
                    .Customize(x => x.WaitForNonStaleResults())
                    .FirstOrDefault(x => x.EmailAddress == emailAddress.ToLowerInvariant());
        }
    }
}