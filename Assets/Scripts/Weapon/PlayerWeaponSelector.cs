using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerWeaponSelector : MonoBehaviour
{
    [SerializeField]
    private GunType gunType;
    [SerializeField]
    private Transform gunParent;
    [SerializeField]
    private List<WeaponScriptableObject> weapons;

    [Space]
    [Header("Runtime Filled")]
    public WeaponScriptableObject activeWeapon;

    private void Start()
    {
        WeaponScriptableObject weapon = weapons.Find(weapon => weapon.type == gunType);

        if (weapon == null)
        {
            Debug.LogError($"No WeaponScriptableObject found for {weapon}");
        }

        activeWeapon = weapon;
        activeWeapon.Spawn(gunParent, this);
    }
}
