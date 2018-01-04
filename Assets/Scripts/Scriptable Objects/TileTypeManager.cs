using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = assetName + ".asset", menuName = "Tile Manager", order = Utility.CREATE_ASSET_ORDER_ID)]
public class TileTypeManager : ScriptableObject
{
    public List<TileType> AllTypes
    {
        get
        {
            if (_allTypes == null)
                CreateReferences();

            return _allTypes;
        }
    }

    [SerializeField]
    private List<TileType> _allTypes;

    private const string assetName = "TileTypeContainer";

    public static TileTypeManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GetInstance();

            return _instance;
        }
    }
    private static TileTypeManager _instance;

    
    [ContextMenu("Create References")]
    private void CreateReferences()
    {
        #region UNITY_EDITOR
        _allTypes = new List<TileType>();

        Debug.Log("Creating Tile Manager");
        
        string[] assetIDs = UnityEditor.AssetDatabase.FindAssets("t:TileType");

        for (int i = 0; i < assetIDs.Length; i++)
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(assetIDs[i]);
            Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>(path);

            if(obj is TileType)
            {
                _allTypes.Add(obj as TileType);
            }            
        }

        Debug.Log("Created " + _allTypes.Count + " references");
        #endregion
    }
#if UNITY_EDITOR
    [UnityEditor.Callbacks.PostProcessBuildAttribute()]
#endif
    [ContextMenu("Remove Unused Assets")]
    private void CheckForUnusedAssets()
    {
        int amountPrior = _allTypes.Count;

        _allTypes = new List<TileType>(_allTypes.Where(x => x != null));

        int difference = amountPrior - _allTypes.Count;

        if(difference > 0)
        {
            Debug.Log("Removed " + difference + " unused tile " + (difference == 1 ? "type" : "types"));
        }
    }

    private static TileTypeManager GetInstance()
    {
        TileTypeManager manager = Resources.Load<TileTypeManager>(assetName);

        if (manager == null)
        {
            return CreateInstance();
        }

        return manager;
    }
    private static TileTypeManager CreateInstance()
    {
        TileTypeManager manager = CreateInstance<TileTypeManager>();

        #region UNITY_EDITOR

        UnityEditor.AssetDatabase.CreateAsset(manager, "Resources/" + assetName);

        #endregion

        return manager;
    }
    public static void Add(TileType type)
    {
        Instance.CheckForUnusedAssets();

        Instance._allTypes.Add(type);
    }
    public static void Refresh()
    {
        Instance.CheckForUnusedAssets();
    }
}
