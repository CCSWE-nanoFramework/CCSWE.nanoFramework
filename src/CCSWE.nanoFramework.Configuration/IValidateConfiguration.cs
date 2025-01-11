namespace CCSWE.nanoFramework.Configuration
{
    /// <summary>
    /// Interface used to validate configuration.
    /// </summary>
    public interface IValidateConfiguration
    {
        /// <summary>
        /// Validates a specific configuration instance.
        /// </summary>
        /// <param name="configuration">The configuration instance.</param>
        /// <returns>The <see cref="ValidateConfigurationResult"/> result.</returns>
        ValidateConfigurationResult Validate(object? configuration);
    }
}
