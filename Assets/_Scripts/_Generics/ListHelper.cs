using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public static class ListHelper
{
    public static int CountTimes< T >(this List< T > list, T item)
    {
        return ((from t in list where t.Equals(item) select t).Count());
    }
    
    public static void Shuffle<T>(this IList<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }    
        
    public static T RandomItem<T>(this IList<T> list)
    {
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list");
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
        
    public static T RemoveRandom<T>(this IList<T> list)
    {
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot remove a random item from an empty list");
        int index = UnityEngine.Random.Range(0, list.Count);
        T item = list[index];
        list.RemoveAt(index);
        return item;
    }
    
    public static void ListDestroyAndClear<T>(this IList<T> list)
    {
        foreach (var variable in list)
        {
            UnityEngine.Object.Destroy(variable as Object);
        }
        list.Clear();
    }

    public static void IndexDestroyAndRemove<T>(this IList<T> list, int index)
    {
        UnityEngine.Object.Destroy(list[index] as Object);
        list.RemoveAt(index);
    }    
    
    public static bool IndexDestroyAndRemove<T>(this IList<T> list, T item)
    {
        int index = list.IndexOf(item);
        if (index < 0)
            return false;
        UnityEngine.Object.Destroy(list[index] as Object);
        list.RemoveAt(index);
        return true;
    }
    
    
    public static T RandomHashItem<T>(this HashSet<T> list)
    {
        Random random = new Random();
        return list.ElementAt(random.Next(list.Count));
    }

    public static T RandomRemoveHashItem<T>(this HashSet<T> list)
    {
        Random random = new Random();
        T item = list.ElementAt(random.Next(list.Count));
        list.Remove(item);
        return item;
    }

}
