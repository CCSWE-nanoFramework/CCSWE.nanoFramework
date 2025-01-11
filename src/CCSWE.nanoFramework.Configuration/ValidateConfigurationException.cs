using System;

namespace CCSWE.nanoFramework.Configuration
{
    /// <summary>
    /// Represents a failure from an <see cref="IValidateConfiguration"/> validation.
    /// </summary>
    public class ValidateConfigurationException: Exception
    {
        /// <summary>
        /// Created a new <see cref="ValidateConfigurationException"/> from a <see cref="ValidateConfigurationResult"/>.
        /// </summary>
        public ValidateConfigurationException(ValidateConfigurationResult result): base("Configuration validation failed")
        {
            Result = result;
        }

        /// <summary>
        /// The results of the validation.
        /// </summary>
        public ValidateConfigurationResult Result { get; }
    }
}
