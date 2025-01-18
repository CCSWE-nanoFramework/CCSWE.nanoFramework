using System;
using CCSWE.nanoFramework.WebServer.Authorization;

#nullable disable // TODO: Remove this once my Attribute PR is in NuGet
namespace CCSWE.nanoFramework.WebServer.Samples.Controllers
{
    [Route("/anonymous")]
    [AllowAnonymous]
    public class AllowAnonymousController: ControllerBase
    {
        [HttpGet]
        public void HandleGet()
        {
            Ok($"Hello from [HttpGet] at {DateTime.UtcNow.TimeOfDay}");
        }

        [HttpGet("{parameter}")]
        public void HandleGet(string parameter)
        {
            Ok($"Hello from [HttpGet(\"{{parameter}}\")] at {DateTime.UtcNow.TimeOfDay} - {parameter}");
        }
    }
}
