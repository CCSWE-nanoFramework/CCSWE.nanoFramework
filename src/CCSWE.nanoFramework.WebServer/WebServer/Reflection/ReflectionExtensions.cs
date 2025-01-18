using System;
using System.Reflection;

namespace CCSWE.nanoFramework.WebServer.Reflection
{
    /// <summary>
    /// Helpful reflection extensions.
    /// </summary>
    // TODO: Should this go in Core? Probably a new library...
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Determines whether the current <see cref="Type"/> implements <paramref name="interfaceType"/>.
        /// </summary>
        /// <param name="type">The source type.</param>
        /// <param name="interfaceType">The <see cref="Type"/> of the target interface.</param>
        /// <returns><see langword="true"/> if the current <see cref="Type"/> implements <paramref name="interfaceType"/>; otherwise, <see langword="false"/>.</returns>
        public static bool IsImplementationOf(this Type type, Type interfaceType)
        {
            var interfaces = type.GetInterfaces();
            foreach (var current in interfaces)
            {
                if (current == interfaceType)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a string that represents the <see cref="MethodInfo"/>.
        /// </summary>
        /// <returns>A string that represents the <see cref="MethodInfo"/>.</returns>
        public static string ToDisplayName(this MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();
            var parameterTypes = new string[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                parameterTypes[i] = parameters[i].ParameterType.Name;
            }

            return parameterTypes.Length > 0 ? 
                $"{methodInfo.DeclaringType!.Name}.{methodInfo.Name}({Strings.Join(", ", parameterTypes)})" : 
                $"{methodInfo.DeclaringType!.Name}.{methodInfo.Name}()";
        }
    }
}
