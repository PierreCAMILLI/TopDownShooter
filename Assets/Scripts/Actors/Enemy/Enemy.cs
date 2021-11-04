using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor, IWeaponTarget
{
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private ParticleSystem _particleSystem;

    public void OnShoot()
    {
        _meshRenderer.enabled = false;
        _particleSystem.Play();
        Destroy(gameObject, 5f);
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
