using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Utility : MonoBehaviour {

    public const int CREATE_ASSET_ORDER_ID = 200;
    
    public static ItemBase CreateItemAndRaname<T>() where T : ItemBase
    {
        ItemBase item = ScriptableObject.CreateInstance<T>();

        item.OnCreatedInInspector();

        CreateAssetAndRename(item, item.GetType() + ".asset");

        return item;
    }
    public static Object CreateAssetAndRename(Object obj)
    {
        return CreateAssetAndRename(obj, "newAsset.asset");
    }
    public static Object CreateAssetAndRename(Object obj, string fileName)
    {
        return CreateAssetAndRename(obj, GetCurrentlySelectedFolder(), fileName);
    }
    public static Object CreateAssetAndRename(Object obj, string folderPath, string fileName)
    {
        ProjectWindowUtil.CreateAsset(obj, folderPath + "/" + fileName);

        return obj;
    }
    public static string GetCurrentlySelectedFolder()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);

        if (path == "")
        {
            return "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            return path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }
        else
        {
            return path;
        }
    }
}
