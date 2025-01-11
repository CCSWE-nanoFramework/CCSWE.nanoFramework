namespace CCSWE.nanoFramework.Configuration.UnitTests.Mocks
{
    internal class ConfigurationMockValidator: IValidateConfiguration
    {
        private readonly bool _validatesSuccessfully;

        public ConfigurationMockValidator(bool validatesSuccessfully)
        {
            _validatesSuccessfully = validatesSuccessfully;
        }

        public bool ValidateCalled { get; set; }

        public ValidateConfigurationResult Validate(object configuration)
        {
            ValidateCalled = true;

            return _validatesSuccessfully
                ? ValidateConfigurationResult.Success
                : ValidateConfigurationResult.Fail("Failure");
        }
    }
}
