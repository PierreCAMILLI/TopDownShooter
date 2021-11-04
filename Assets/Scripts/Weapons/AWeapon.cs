using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeapon : MonoBehaviour
{
    public Hero Hero { get; private set; }


    public void Init(Hero hero)
    {
        Hero = hero;
        gameObject.SetActive(false);
    }

    public virtual void OnEquip() { }
    public virtual void OnUnequip() { }

    public abstract void OnShootHold();
    public abstract void OnShootDown();
    public abstract void OnShootUp();
}
