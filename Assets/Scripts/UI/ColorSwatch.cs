using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwatch : MonoBehaviour {

    public ColorBlock Swatch { get { return _colorSwatch; } }

    [SerializeField]
    private ColorBlock _colorSwatch = ColorBlock.defaultColorBlock;
}
