namespace twitter.api.application.Models.Security
{
    public class SecurityServiceConfiguration
    {
        #region Constructors

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public SecurityServiceConfiguration() { }

        /// <summary>
        /// Contructor with parameters.
        /// </summary>
        /// <param name="audience">A URL to validate audience.</param>
        /// <param name="issuer">A URL to validate issuer.</param>
        /// <param name="secret">The encryption secret.</param>
        public SecurityServiceConfiguration(string audience, string issuer, string secret, int accesTokenDuration, int refreshTokenDuration)
        {
            Audience = audience;
            Issuer = issuer;
            Secret = secret;
            AccesTokenDuration = accesTokenDuration;
            RefreshTokenDuration = refreshTokenDuration;
        }

        #endregion

        #region Properties

        public string Audience { get; set; }

        public string Issuer { get; set; }

        public string Secret { get; set; }

        public int AccesTokenDuration { get; set; }

        public int RefreshTokenDuration { get; set; }

        #endregion
    }
}
