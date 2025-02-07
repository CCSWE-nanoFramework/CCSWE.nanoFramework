

// ReSharper disable CheckNamespace
namespace System.Collections;

/// <summary>
/// Extension methods for <see cref="ArrayList"/>.
/// </summary>
public static class ArrayListExtensions
{
    /// <summary>
    /// Adds the elements of the given collection to the end of this list.
    /// </summary>
    public static void AddRange(this ArrayList arrayList, IEnumerable collection)
    {
        foreach (var value in collection)
        {
            arrayList.Add(value);
        }
    }
}