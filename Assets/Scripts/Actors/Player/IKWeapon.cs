using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKWeapon : MonoBehaviour
{
    [System.Serializable]
    public struct HumanBone
    {
        public HumanBodyBones bone;
        [Range(0f, 1f)]
        public float weight;
    }

    private Animator _animator;

    public Vector3 Target { get; set; }
    public Transform AimTransform { get; set; }

    [SerializeField]
    [Range(1, 10)]
    private int _iterations = 5;

    [SerializeField]
    private float _angleLimit = 90f;
    [SerializeField]
    private float _distanceLimit = 2f;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private HumanBone[] _humanBones;
    private Transform[] _boneTransforms;

    private Vector3 _currentTarget;
    private Vector3 _currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        _boneTransforms = new Transform[_humanBones.Length];
        for (int i = 0; i < _boneTransforms.Length; ++i)
        {
            _boneTransforms[i] = _animator.GetBoneTransform(_humanBones[i].bone);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (AimTransform)
        {
            if (_speed == 0f)
            {
                _currentTarget = GetTargetPosition();
            }
            else
            {
                _currentTarget = Vector3.SmoothDamp(_currentTarget, GetTargetPosition(), ref _currentVelocity, 1f / _speed, Mathf.Infinity, Time.deltaTime);
            }
            for (int i = 0; i < _iterations; ++i)
            {
                for (int j = 0; j < _boneTransforms.Length; ++j)
                {
                    AimAtTarget(_boneTransforms[j], _currentTarget, _humanBones[j].weight);
                }
            }
        }
    }

    private void AimAtTarget(Transform bone, Vector3 targetPosition, float weight)
    {
        Vector3 aimDirection = AimTransform.forward;
        Vector3 targetDirection = targetPosition - AimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);

        bone.rotation = blendedRotation * bone.rotation;
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetDirection = Target - AimTransform.position;
        Vector3 aimDirection = AimTransform.forward;
        float blendOut = 0.0f;

        float targetAngle = Vector3.Angle(targetDirection, aimDirection);
        if (targetAngle > _angleLimit)
        {
            blendOut += (targetAngle - _angleLimit) / 50.0f;
        }

        float targetDistance = targetDirection.magnitude;
        if (targetDistance < _distanceLimit)
        {
            blendOut += _distanceLimit - targetDistance;
        }

        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);
        return AimTransform.position + direction;
    }
}
