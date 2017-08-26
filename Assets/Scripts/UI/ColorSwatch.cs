using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwatch : MonoBehaviour {

    public ColorBlock Swatch { get { return _colorSwatch; } }
    //
    // Summary:
    //     ///
    //     Normal Color.
    //     ///
    public Color normalColor { get { return Swatch.normalColor; } }
    //
    // Summary:
    //     ///
    //     Highlighted Color.
    //     ///
    public Color highlightedColor { get { return Swatch.highlightedColor; } }
    //
    // Summary:
    //     ///
    //     Pressed Color.
    //     ///
    public Color pressedColor { get { return Swatch.pressedColor; } }
    //
    // Summary:
    //     ///
    //     Disabled Color.
    //     ///
    public Color disabledColor { get { return Swatch.disabledColor; } }
    //
    // Summary:
    //     ///
    //     Multiplier applied to colors (allows brightening greater then base color).
    //     ///
    public float colorMultiplier { get { return Swatch.colorMultiplier; } }
    //
    // Summary:
    //     ///
    //     How long a color transition should take.
    //     ///
    public float fadeDuration { get { return Swatch.fadeDuration; } }

    [SerializeField]
    private ColorBlock _colorSwatch = ColorBlock.defaultColorBlock;
}
