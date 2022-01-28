using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CodeUtils
{
    public static IList<T> CopyList<T>(this IList<T> source)
    {
        IList<T> cloner = new List<T>();
        foreach (var item in source)
        {
            cloner.Add(item);
        }
        return cloner;
    }
}
