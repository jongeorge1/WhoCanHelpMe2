namespace WhoCanHelpMe.Web.Configuration
{
    using System.Configuration;
    using System.Text;

    public class EncryptionSettings
    {
        public string Passphrase
        {
            get
            {
                return ConfigurationManager.AppSettings["encryption.passphrase"];
            }
        }

        public byte[] Salt
        {
            get
            {
                return Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["encryption.salt"]);
            }
        }

        public string HmacPassphrase
        {
            get
            {
                return ConfigurationManager.AppSettings["encryption.hmacPassphrase"];
            }
        }

        public byte[] HmacSalt
        {
            get
            {
                return Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["encryption.hmacSalt"]);
            }
        }
    }
}