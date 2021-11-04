using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryWeapon : MonoBehaviour
{
    [SerializeField]
    private Hero _hero;
    public Hero Hero { get { return _hero; } }

    public AWeapon[] Weapons { get; private set; }

    private void Awake()
    {
        BuildWeaponsArray();
        foreach(AWeapon weapon in Weapons)
        {
            weapon.Init(_hero);
        }
    }

    public AWeapon GetNextWeapon()
    {
        if (Weapons.Count() > 0)
        {
            return Weapons[0];
        }
        return null;
    }

    public AWeapon GetNextWeapon(AWeapon weapon, bool previous = false)
    {

        if (Weapons.Count() > 0)
        {
            if (previous)
            {
                IEnumerable<AWeapon> weapons = Weapons.TakeWhile(w => w != weapon);
                if (weapons.Count() > 0)
                {
                    return weapons.Last();
                }
                else
                {
                    return Weapons.Last();
                }
            }
            else
            {
                IEnumerable<AWeapon> weapons = Weapons.SkipWhile(w => w != weapon).Skip(1);
                if (weapons.Count() > 0)
                {
                    return weapons.First();
                }
                else
                {
                    return Weapons.First();
                }
            }
        }
        return null;
    }

    private void BuildWeaponsArray()
    {
        Weapons = GetComponentsInChildren<AWeapon>();
    }
}
