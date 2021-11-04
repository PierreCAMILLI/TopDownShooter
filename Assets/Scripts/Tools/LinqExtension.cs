using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class LinqExtension
{
    public static void ForEach<T>(this IEnumerable<T> enumerable, System.Action<T> action)
    {
        foreach (T item in enumerable)
        {
            action(item);
        }
    }
}
