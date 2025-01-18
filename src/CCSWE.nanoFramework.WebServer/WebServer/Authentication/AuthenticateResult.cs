namespace CCSWE.nanoFramework.WebServer.Authentication
{
    /// <summary>
    /// Contains the result of an Authenticate call
    /// </summary>
    public class AuthenticateResult
    {
        // ReSharper disable once InconsistentNaming
        private static readonly AuthenticateResult _noResult = new() { None = true };

        /// <summary>
        /// Holds failure information from the authentication.
        /// </summary>
        public string? Failure { get; protected set; }

        /// <summary>
        /// Indicates that there was no information returned for this authentication scheme.
        /// </summary>
        public bool None { get; protected set; }

        /// <summary>
        /// If a ticket was produced, authenticate was successful.
        /// </summary>
        public bool Succeeded { get; protected set; }

        /// <summary>
        /// Indicates that there was a failure during authentication.
        /// </summary>
        /// <param name="failureMessage">The failure message.</param>
        /// <returns>The result.</returns>
        public static AuthenticateResult Fail(string? failureMessage)
        {
            return new AuthenticateResult { Failure = string.IsNullOrEmpty(failureMessage) ? "Authentication failed." : failureMessage };
        }

        /// <summary>
        /// Indicates that there was no information returned for this authentication scheme.
        /// </summary>
        /// <returns>The result.</returns>
        public static AuthenticateResult NoResult()
        {
            return _noResult;
        }

        /// <summary>
        /// Indicates that authentication was successful.
        /// </summary>
        /// <returns>The result.</returns>
        public static AuthenticateResult Success()
        {
            return new AuthenticateResult { Succeeded = true };
        }
    }
}
