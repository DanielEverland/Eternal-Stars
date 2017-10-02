using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSelectedWeaponManager : MonoBehaviour {

    [SerializeField]
    private TMP_Text _ammoCount;
    [SerializeField]
    private TMP_Text _weaponName;
    [SerializeField]
    private Image _lineImage;

    private WeaponBase CurrentWeapon { get { return WeaponManager.SelectedWeapon; } }
    private Color _targetWeaponColor = Color.white;

    private const float COLOR_LERP_SPEED = 5;
    
    private void Awake()
    {
        WeaponManager.OnSelectedWeaponChanged += WeaponChanged;

        WeaponChanged(WeaponManager.SelectedWeapon);
    }
    private void WeaponChanged(WeaponBase newWeapon)
    {
        if(newWeapon != null)
        {
            InitializeValuse();
        }
        else
        {
            AssignEmptyValues();
        }
        
    }
    private void Update()
    {
        LerpColor();
    }
    private void LerpColor()
    {
        _lineImage.color = Color.Lerp(_lineImage.color, _targetWeaponColor, COLOR_LERP_SPEED * Time.unscaledDeltaTime);
    }
    private void InitializeValuse()
    {
        _ammoCount.text = CurrentWeapon.AmmoCountText;
        _weaponName.text = CurrentWeapon.Name;
        _targetWeaponColor = CurrentWeapon.Rarity.Color;
    }
    private void AssignEmptyValues()
    {
        _ammoCount.text = "";
        _weaponName.text = "None Equipped";
        _targetWeaponColor = Color.white;
    }
    private void OnDestroyed()
    {
        WeaponManager.OnSelectedWeaponChanged -= WeaponChanged;
    }
}
