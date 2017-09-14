using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UI Colors.asset", menuName = "UI/Colors", order = Utility.CREATE_ASSET_ORDER_ID)]
public class UIColors : ScriptableObject {

	private static UIColors Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = (UIColors)Resources.Load("UI Colors");
            }

            return _instance;
        }
    }
    private static UIColors _instance;

    [SerializeField]
    private List<ColorEntry> _colorEntries = new List<ColorEntry>();

    public static List<string> Keys
    {
        get
        {
            if(_keys == null || IsNewVersion())
            {
                _keys = new List<string>(Instance._colorEntries.Select(x => x.Key));
            }

            return _keys;
        }
    }
    private static List<string> _keys;

    private static int _oldHashCode;

    public static Color GetColor(string key)
    {
        return Instance._colorEntries.Find(x => x.Key == key).Color;
    }
    private static bool IsNewVersion()
    {
        if(_oldHashCode != Instance.GetHashCode())
        {
            _oldHashCode = Instance.GetHashCode();
            return true;
        }

        return false;
    }

    [Serializable]
    private struct ColorEntry
    {
        public string Key;
        public Color Color;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 13;

            for (int i = 0; i < _colorEntries.Count; i++)
            {
                hash *= 17 * (_colorEntries[i].Color.GetHashCode() + _colorEntries[i].Key.GetHashCode());
            }

            return hash;
        }
    }
}
