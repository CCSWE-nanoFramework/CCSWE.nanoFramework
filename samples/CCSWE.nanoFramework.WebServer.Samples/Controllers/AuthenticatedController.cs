using System;

#nullable disable // TODO: Remove this once my Attribute PR is in NuGet
namespace CCSWE.nanoFramework.WebServer.Samples.Controllers
{
    [Route("/authenticated")]
    public class AuthenticatedController : ControllerBase
    {
        [HttpGet]
        [HttpGet("{parameter}")]
        public void HandleGet(string parameter = null)
        {
            Ok(!string.IsNullOrEmpty(parameter)
                ? $"Hello from [HttpGet] at {DateTime.UtcNow.TimeOfDay}"
                : $"Hello from [HttpGet(\"{{parameter}}\")] at {DateTime.UtcNow.TimeOfDay} - {parameter}");
        }
    }
}
