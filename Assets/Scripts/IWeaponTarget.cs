using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShootInfo
{
    public AWeapon Weapon { get; set; }
    public Vector3 From { get; set;  }
    public Vector3 Direction { get; set; }
    public float Distance { get; set; }
    public Vector3 Goal { get { return From + Direction * Distance; } }
    public int Power { get; set; }

    // TODO: Bullet abstract component if we add weapons that shoot visible bullet
}

public interface IWeaponTarget 
{
    public void OnShoot(ShootInfo shootInfo);
}
