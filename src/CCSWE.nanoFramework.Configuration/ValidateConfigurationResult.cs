namespace CCSWE.nanoFramework.Configuration
{
    /// <summary>
    /// Represents the result of an configuration validation.
    /// </summary>
    public class ValidateConfigurationResult
    {
        /// <summary>
        /// Validation was successful.
        /// </summary>
        public static readonly ValidateConfigurationResult Success = new() { Succeeded = true };

        /// <summary>
        /// True if validation was successful.
        /// </summary>
        public bool Succeeded { get; protected set; }

        /// <summary>
        /// True if validation failed.
        /// </summary>
        public bool Failed { get; protected set; }

        /// <summary>
        /// Used to describe why validation failed.
        /// </summary>
        public string? FailureMessage { get; protected set; }

        /// <summary>
        /// Full list of failures (can be multiple).
        /// </summary>
        public string[]? Failures { get; protected set; }

        /// <summary>
        /// Returns a failure result.
        /// </summary>
        /// <param name="failureMessage">The reason for the failure.</param>
        /// <returns>The failure result.</returns>
        public static ValidateConfigurationResult Fail(string failureMessage)
            => new() { Failed = true, FailureMessage = failureMessage, Failures = new[] { failureMessage } };

        /// <summary>
        /// Returns a failure result.
        /// </summary>
        /// <param name="failures">The reasons for the failure.</param>
        /// <returns>The failure result.</returns>
        public static ValidateConfigurationResult Fail(string[] failures)
            => new () { Failed = true, FailureMessage = Strings.Join("; ", failures), Failures = failures };
    }
}
