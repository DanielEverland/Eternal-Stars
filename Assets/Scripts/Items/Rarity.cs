using System.Linq;
using UnityEditor;
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
    [Tooltip("Used to order entries in dropdown")]
    [SerializeField]
    private byte _orderWeigth = 0;

    public static List<Rarity> AllRarities
    {
        get
        {
            List<Rarity> rarities = new List<Rarity>();

            string[] allGUIDs = AssetDatabase.FindAssets("t:Rarity");

            for (int i = 0; i < allGUIDs.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(allGUIDs[i]);

                Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));

                if(obj is Rarity)
                {
                    rarities.Add(obj as Rarity);
                }
            }

            return rarities.OrderBy(x => x._orderWeigth).ToList();
        }
    }
}
