using System.Reflection;
using CCSWE.nanoFramework;

// ReSharper disable CheckNamespace
namespace System;

/// <summary>
/// Extension methods for <see cref="MethodInfo"/>
/// </summary>
public static class MethodInfoExtensions
{
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