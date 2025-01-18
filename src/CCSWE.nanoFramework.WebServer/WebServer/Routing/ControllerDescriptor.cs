using System;

namespace CCSWE.nanoFramework.WebServer.Routing
{
    internal class ControllerDescriptor
    {
        public ControllerDescriptor(Type implementationType)
        {
            ImplementationType = implementationType;
        }

        public Type ImplementationType { get; }
    }
}
