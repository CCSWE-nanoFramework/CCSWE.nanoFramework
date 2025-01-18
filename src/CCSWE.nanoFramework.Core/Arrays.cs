using System;

namespace CCSWE.nanoFramework
{
    /// <summary>
    /// Extension methods for arrays.
    /// </summary>
    public static class Arrays
    {
        /// <summary>
        /// Copies the elements of the <see cref="Array"/> to a new array of the specified element type.
        /// </summary>
        /// <param name="source">The source <see cref="Array"/>.</param>
        /// <param name="type">The element type of the destination array to create and copy elements to.</param>
        /// <returns>An array of the specified element type containing copies of the elements of the <see cref="Array"/>.</returns>
        /// <remarks>The element type of the source array must be a supertype of <paramref name="type"/>.</remarks>
public static Array ToArray(this Array source, Type type)
{
    var output = Array.CreateInstance(type, source.Length);

    source.CopyTo(output, 0);

    return output;
}
    }
}
