using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.Mediator.UnitTests.Mocks
{
    internal class LoggerMock: ILogger
    {
        public void Log(LogLevel logLevel, EventId eventId, string state, Exception exception, MethodInfo format)
        {

        }

        public bool IsEnabled(LogLevel logLevel) => false;
    }
}
