using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Zombie : Character, IWeaponTarget
{
    private Animator _animator;

    [SerializeField]
    private Transform _target;
    public Transform Target { get { return _target; } }

    [SerializeField]
    private float _speed;
    [SerializeField]
    [Range(0f, 1f)]
    private float _forwardAnimation;
    [SerializeField]
    private float _maxDegreeDelta;
    public float MaxDegreeDelta { get { return _maxDegreeDelta; } }

    public void OnShoot(ShootInfo shootInfo)
    {
        _animator.applyRootMotion = false;
        _animator.SetTrigger("Dead");
    }

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _animator.SetFloat("Speed", _speed);
        _animator.SetFloat("Forward", _target ? _forwardAnimation : 0f);

    }
}
