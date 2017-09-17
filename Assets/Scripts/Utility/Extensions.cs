using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {

    private const int MAX_OUTPUT_ARRAY_LENGTH = 100;
    
    public static bool HasMultiple(this Sprite sprite)
    {
        string assetPath = AssetDatabase.GetAssetPath(sprite);
        
        return AssetDatabase.LoadAllAssetsAtPath(assetPath).OfType<Sprite>().ToArray().Length > 1;
    }
    public static bool IsReadable(this Texture2D texture)
    {
        try
        {
            texture.GetPixel(0, 0);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public static Texture2D ToTextureSafe(this Sprite sprite)
    {
        if (sprite.texture.IsReadable())
        {
            return ToTexture(sprite);
        }
        else
        {
            return sprite.texture;
        }
    }
    public static Texture2D ToTexture(this Sprite sprite)
    {
        if (!sprite.HasMultiple())
            return sprite.texture;

        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                (int)sprite.textureRect.y,
                                                (int)sprite.textureRect.width,
                                                (int)sprite.textureRect.height);
        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }
    public static string ToHex(this Color color)
    {
        return string.Format("#{0}{1}{2}{3}",
            ((int)(color.r * 255)).ToString("X2"),
            ((int)(color.g * 255)).ToString("X2"),
            ((int)(color.b * 255)).ToString("X2"),
            ((int)(color.a * 255)).ToString("X2"));
    }
    public static Rect GetWorldRect(this RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        Vector3 topLeft = corners[0];

        return new Rect(topLeft, rectTransform.rect.size);
    }
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
