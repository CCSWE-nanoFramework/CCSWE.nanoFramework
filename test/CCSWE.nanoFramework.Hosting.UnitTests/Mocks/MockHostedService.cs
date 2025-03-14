//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace CCSWE.nanoFramework.Hosting.UnitTests.Mocks
{
    public class MockHostedService : IHostedService
    {
        private readonly bool _startThrows;
        private readonly bool _stopThrows;

        public bool IsStarted { get; private set; }
        public bool IsStopped { get; private set; }

        public MockHostedService(bool startThrows = false, bool stopThrows = false)
        {
            _startThrows = startThrows;
            _stopThrows = stopThrows;
        }

        public void StartAsync(CancellationToken cancellationToken)
        {
            if (_startThrows)
            {
                throw new Exception();
            }

            IsStarted = true;
        }

        public void StopAsync(CancellationToken cancellationToken)
        {
            if (_stopThrows)
            {
                throw new Exception();
            }

            IsStopped = true;
        }
    }
}
