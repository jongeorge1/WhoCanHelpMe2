namespace WhoCanHelpMe.Web.Configuration
{
    using Nancy.Cryptography;

    public static class CryptographyConfigurationHelper
    {
        public static CryptographyConfiguration BuildCryptographyConfiguration()
        {
            var encryptionSettings = new EncryptionSettings();
            var encryptionPassPhrase = encryptionSettings.Passphrase;
            var encryptionSalt = encryptionSettings.Salt;
            var hmacPassphrase = encryptionSettings.HmacPassphrase;
            var hmacSalt = encryptionSettings.HmacSalt;
            return
                new CryptographyConfiguration(
                    new RijndaelEncryptionProvider(new PassphraseKeyGenerator(encryptionPassPhrase, encryptionSalt)),
                    new DefaultHmacProvider(new PassphraseKeyGenerator(hmacPassphrase, hmacSalt)));
        }
    }
}