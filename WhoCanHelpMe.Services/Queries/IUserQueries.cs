namespace WhoCanHelpMe.Services.Queries
{
    using WhoCanHelpMe.Domain;

    public interface IUserQueries
    {
        User GetByEmailAddress(string emailAddress);
    }
}