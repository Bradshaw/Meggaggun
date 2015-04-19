using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ListExtensions {

    public static List<T> Shuffle<T>(this List<T> l)
    {
        List<T> filler = new List<T>();
        while (l.Count > 0)
            filler.Add(l.PopRandom());
        foreach (T t in filler)
            l.Add(t);
        return filler;
    }

    public static List<T> Swap<T>(this List<T> l, int a, int b)
    {
        T store = l[a];
        l[a] = l[b];
        l[b] = store;
        return l;
    }
    public static T PickRandom<T>(this List<T> l)
    {
        return l[Random.Range(0, l.Count)];
    }

    public static T PopRandom<T>(this List<T> l)
    {
        int index = Random.Range(0, l.Count);
        T res = l[index];
        l.RemoveAt(index);
        return res;
    }
}
