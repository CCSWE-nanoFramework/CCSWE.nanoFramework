using System;

namespace CCSWE.nanoFramework.WebServer.Authentication
{
    internal class AuthenticationHandlerDescriptor
    {
        public AuthenticationHandlerDescriptor(Type implementationType)
        {
            ImplementationType = implementationType;
        }

        public Type ImplementationType { get; }
    }
}
