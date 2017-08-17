using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {

    private const int MAX_OUTPUT_ARRAY_LENGTH = 100;

    public static void AddToWorld(this GameObject obj)
    {
        World.AddToWorld(obj);
    }
    public static bool Encompasses(this Rect rect, Rect other)
    {
        return
            rect.xMin < other.xMin && rect.xMax > other.xMax
            &&
            rect.yMin < other.yMin && rect.yMax > other.yMax;
    }
    public static bool IsPowerOfTwo(this uint value)
    {
        return IsPowerOfTwo((int)value);
    }
    public static bool IsPowerOfTwo(this int value)
    {
        return (value != 0) && ((value & (value - 1)) == 0);
    }
    public static void Output<T>(this IEnumerable<T> obj, Func<T, string> expression)
    {
        Debug.Log("Outputting object " + obj.ToString() + " with " + obj.Count() + " elements");

        if (obj.Count() > MAX_OUTPUT_ARRAY_LENGTH)
        {
            Debug.LogWarning("Can't output all elements, too long");
        }

        int length = Mathf.Clamp(obj.Count(), 0, 100);

        for (int i = 0; i < length; i++)
        {
            Debug.Log(expression(obj.ElementAt(i)));
        }
    }
    public static void Output<T>(this IEnumerable<T> obj)
    {
        Debug.Log("Outputting object " + obj.ToString() + " with " + obj.Count() + " elements");

        if (obj.Count() > MAX_OUTPUT_ARRAY_LENGTH)
        {
            Debug.LogWarning("Can't output all elements, too long");
        }

        int length = Mathf.Clamp(obj.Count(), 0, 100);

        for (int i = 0; i < length; i++)
        {
            Debug.Log(obj.ElementAt(i));
        }
    }
    public static void Output(this object[] obj)
    {
        Debug.Log("Outputting object " + obj.ToString() + " with " + obj.Length + " elements");

        if (obj.Length > MAX_OUTPUT_ARRAY_LENGTH)
        {
            Debug.LogWarning("Can't output all elements, too long");
        }

        int length = Mathf.Clamp(obj.Length, 0, 100);

        for (int i = 0; i < length; i++)
        {
            Debug.Log(obj[i]);
        }
    }
}
