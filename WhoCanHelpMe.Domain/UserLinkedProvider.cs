namespace WhoCanHelpMe.Domain
{
    public class UserLinkedProvider
    {
        public UserLinkedProvider()
        {
            // Empty constructor for deserialization. Don't use this.
        }

        public UserLinkedProvider(string providerName, string userId, string email, string name, string pictureUrl)
        {
            this.Provider = providerName;
            this.UserId = userId;
            this.Email = email;
            this.Name = name;
            this.PictureUrl = pictureUrl;
        }

        public string Email { get; private set; }

        public string Name { get; private set; }

        public string PictureUrl { get; private set; }

        public string UserId { get; private set; }

        public string Provider { get; private set; }

        public void Update(string name, string picture)
        {
            this.Name = name;
            this.PictureUrl = picture;
        }
    }
}