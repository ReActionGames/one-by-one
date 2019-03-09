using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExtensions
{
    /// <summary>
    /// Gets a random element from the given list.
    /// </summary>
    /// <typeparam name="T">The type of the list.</typeparam>
    /// <param name="list">The list in which to pick an element.</param>
    /// <returns>A random element from this list.</returns>
    public static T PickRandom<T>(this IEnumerable<T> list)
    {
        int index;
        index = Random.Range(0, list.Count());
        return list.ElementAt(index);
    }

    public static float GetAverage(this List<float> list)
    {
        if (list.Count <= 0) return 0;

        float sum = 0;
        foreach (float number in list)
        {
            sum += number;
        }

        return sum / list.Count;
    }

    public static void SetSize<T>(this List<T> list, int size)
    {
        while(list.Count < size)
        {
            list.Add(default);
        }
        while(list.Count > size)
        {
            list.RemoveAt(list.Count - 1);
        }
    }

    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}