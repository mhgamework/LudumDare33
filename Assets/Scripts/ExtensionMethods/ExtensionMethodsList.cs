using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethodsList
{
    // -- PUBLIC

    // .. FUNCTIONS

    public static bool AddIfNotContains<T>(this List<T> list, T obj)
    {
        if (!list.Contains(obj))
        {
            list.Add(obj);
            return true;
        }

        return false;
    }
}
