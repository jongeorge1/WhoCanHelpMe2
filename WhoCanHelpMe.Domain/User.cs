namespace WhoCanHelpMe.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class User
    {
        public User()
        {
            // Empty constructor for deserialization. Don't use this.
            this.OAuthProviders = new List<UserLinkedProvider>();
            this.Assertions = new List<Assertion>();
        }

        public User(string providerName, string emailAddress, string providerId, string name, string pictureUrl)
        {
            this.EmailAddress = emailAddress.ToLowerInvariant();

            this.Id = Guid.NewGuid();

            this.Assertions = new List<Assertion>();
            this.OAuthProviders = new List<UserLinkedProvider>();
            this.AddOAuthProvider(providerName, providerId, emailAddress, name, pictureUrl);
        }

        public string EmailAddress { get; private set; }

        public Guid Id { get; private set; }

        public List<UserLinkedProvider> OAuthProviders { get; private set; }

        public List<Assertion> Assertions { get; private set; } 

        public void AddOAuthProvider(string providerName, string id, string email, string name, string picture)
        {
            var provider = new UserLinkedProvider(providerName, id, email, name, picture);
            this.OAuthProviders.Add(provider);
        }

        public string GetName()
        {
            var provider = this.OAuthProviders.FirstOrDefault(x => !string.IsNullOrEmpty(x.Name));

            if (provider == null)
            {
                return this.EmailAddress;
            }

            return provider.Name;
        }

        public UserLinkedProvider GetProvider(string providerName)
        {
            return this.OAuthProviders.FirstOrDefault(x => x.Provider == providerName);
        }

        public void AddAssertion(string category, string tag)
        {
            this.Assertions.Add(new Assertion(category, tag));
        }
    }
}