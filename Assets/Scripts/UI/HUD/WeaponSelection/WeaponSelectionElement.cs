using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionElement : MonoBehaviour {

    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private TMP_Text _keybindingText;
    [SerializeField]
    private TMP_Text _weaponNameText;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Button _button;

    private const float ICON_SCALE = 4;
    private const float INACTIVE_ALPHA = 0.3f;
    private const float ACTIVE_ALPHA = 1;
    
	public void Initialize(string keybinding, WeaponBase weapon)
    {
        _keybinding = keybinding;
        _keybindingText.text = Keybindings.GetElement(keybinding).KeyAbbreviation;
        _icon.enabled = weapon != null;
        _button.enabled = weapon != null;
        _weapon = weapon;

        if(weapon != null)
        {
            _weaponNameText.text = weapon.Name;

            _icon.sprite = weapon.WeaponAppearance;
            _icon.rectTransform.SetSize(_icon.sprite.rect.size * ICON_SCALE);
        }
        else
        {
            _weaponNameText.text = "";
        }
    }

    private string _keybinding;
    private WeaponBase _weapon;

    private void Update()
    {
        PollInput();
        AssignAlpha();
    }
    private void PollInput()
    {
        if (_weapon == null)
            return;

        if (Keybindings.GetKeyDown(_keybinding))
        {
            Select();
        }
    }
    private void AssignAlpha()
    {
        _canvasGroup.alpha = IsSelected() ? ACTIVE_ALPHA : INACTIVE_ALPHA;
    }
    private bool IsSelected()
    {
        if (_weapon == null)
            return false;

        return WeaponManager.SelectedWeapon == _weapon;
    }
    public void Select()
    {
        if (_weapon == null)
            return;

        WeaponManager.SelectedWeapon = _weapon;
    }
}
