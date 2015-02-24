namespace WhoCanHelpMe.Services.Indexes
{
    using System.Linq;

    using Raven.Client.Indexes;

    using WhoCanHelpMe.Domain;

    public class UserByEmailAddress : AbstractIndexCreationTask<User, User>
    {
        public UserByEmailAddress()
        {
            this.Map = users => from user in users select new { user.EmailAddress };
        }
    }
}