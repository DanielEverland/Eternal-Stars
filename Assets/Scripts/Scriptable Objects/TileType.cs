using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile.asset", menuName = "Tile", order = Utility.CREATE_ASSET_ORDER_ID)]
public class TileType : ScriptableObject {
    
    [SerializeField]
    private Texture2D texture;

    public Texture2D Texture { get; set; }

#if UNITY_EDITOR
    private void Awake()
    {
        TileTypeManager.Add(this);
    }
#endif
}
