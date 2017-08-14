using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rarity.asset", menuName = "Items/Rarity", order = Utility.CREATE_ASSET_ORDER_ID)]
public class Rarity : ScriptableObject{

	public string Name { get { return _displayName; } }
    public Color Color { get { return _color; } }

    [SerializeField]
    private string _displayName;
    [SerializeField]
    private Color _color = Color.white;
}
