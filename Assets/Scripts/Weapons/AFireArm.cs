using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AFireArm : AWeapon
{
    [SerializeField]
    private Transform _gunpoint;
    public Transform GunPoint { get { return _gunpoint; } }
    public Vector3 Forward { get { return _gunpoint.forward; } }

    public override void OnEquip()
    {
        Hero.IKWeapon.AimTransform = _gunpoint;
    }

    public override void OnUnequip()
    {
        Hero.IKWeapon.AimTransform = null;
    }

    public bool Raycast(out RaycastHit hit, float distance, LayerMask layerMask)
    {
        if (_gunpoint)
        {
            return Physics.Raycast(_gunpoint.position, _gunpoint.forward, out hit, distance, layerMask);
        }
        hit = new RaycastHit();
        return false;
    }

    public Vector3 GetPoint(float distance)
    {
        return _gunpoint.position + (_gunpoint.forward * distance);
    }

    protected ShootInfo GetShootInfo(float distance, int power)
    {
        return new ShootInfo
        {
            Weapon = this,
            From = GunPoint.position,
            Direction = Forward,
            Distance = distance,
            Power = power
        };
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        LayerMask mask = 0;
        if (Hero)
        {
            mask = Hero.ActorMask;
        }
        RaycastHit hit;
        if (Raycast(out hit, Mathf.Infinity, mask))
        {
            Gizmos.DrawRay(GunPoint.position, GunPoint.forward * hit.distance);
        }
        else
        {
            Gizmos.DrawRay(GunPoint.position, GunPoint.forward * 10000f);
        }
    }
}
