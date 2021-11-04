using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : AFireArm
{
    [SerializeField]
    private float _power = 5f;

    public override void OnShootHold()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnShootDown()
    {
        Hero.Animator.SetBool("Shoot", true);

        RaycastHit hit;
        if (Raycast(out hit, Mathf.Infinity, Hero.ActorMask))
        {
            if (hit.collider.attachedRigidbody)
            {
                hit.collider.attachedRigidbody.AddForce(Forward * _power, ForceMode.VelocityChange);
                IWeaponTarget gunTarget = hit.collider.GetComponent<IWeaponTarget>();
                if (gunTarget != null)
                {
                    ShootInfo shootInfo = GetShootInfo(hit.distance, 1);
                    gunTarget.OnShoot(shootInfo);
                }
            }
        }
    }

    public override void OnShootUp()
    {
        Hero.Animator.SetBool("Shoot", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
