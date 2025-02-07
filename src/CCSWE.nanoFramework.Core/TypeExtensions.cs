using System;

// ReSharper disable CheckNamespace
namespace System;

/// <summary>
/// Extension methods for <see cref="Type"/>
/// </summary>
public static class TypeExtensions
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


}