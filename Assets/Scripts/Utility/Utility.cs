using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Utility : MonoBehaviour {

    public const int CREATE_ASSET_ORDER_ID = 200;

    public static IntVector2 WorldToChunkPosition(Vector2 worldPosition, int chunkSize)
    {
        return new IntVector2()
        {
            x = Mathf.FloorToInt(worldPosition.x / chunkSize),
            y = Mathf.FloorToInt(worldPosition.y / chunkSize),
        };
    }
    public static IntVector2 WorldToChunkLocalPosition(Vector2 worldPosition, int chunkSize)
    {
        return new IntVector2()
        {
            x = Mathf.RoundToInt(worldPosition.x % chunkSize),
            y = Mathf.RoundToInt(worldPosition.y % chunkSize),
        };
    }
    public static string KeyCodeToProperString(KeyCode keycode)
    {
        switch (keycode)
        {
            case KeyCode.Alpha0:
                return "0";
            case KeyCode.Alpha1:
                return "1";
            case KeyCode.Alpha2:
                return "2";
            case KeyCode.Alpha3:
                return "3";
            case KeyCode.Alpha4:
                return "4";
            case KeyCode.Alpha5:
                return "5";
            case KeyCode.Alpha6:
                return "6";
            case KeyCode.Alpha7:
                return "7";
            case KeyCode.Alpha8:
                return "8";
            case KeyCode.Alpha9:
                return "9";
            default:
                return keycode.ToString();
        }
    }
    public static T CreateObject<T>(System.Type type, ScriptableObject target) where T : ScriptableObject
    {
        T newAction = ScriptableObject.CreateInstance(type) as T;
        newAction.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newAction, target);
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newAction));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        return newAction;
    }
    public static ItemBase CreateItemAndRename<T>() where T : ItemBase
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
