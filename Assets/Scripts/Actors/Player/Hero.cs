using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(IKWeapon))]
public class Hero : Character
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    public Animator Animator { get { return _animator; } }

    public IKWeapon IKWeapon { get; private set; }

    [SerializeField]
    private Transform _rightHandTransform;
    [SerializeField]
    private Transform _horizontalAimReference;
    [SerializeField]
    private InventoryWeapon _inventoryWeapon;

    [Space]
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private LayerMask _groundMask;
    [SerializeField]
    private LayerMask _actorMask;
    public LayerMask ActorMask { get { return _actorMask; } }

    public float MaxSpeed { get { return _maxSpeed; } set { _maxSpeed = value; } }
    public Vector2 MoveDirection { get; set; }
    public Vector2 LookDirection { get; set; }
    public AWeapon Weapon { get; private set; }

    // Targets
    public Collider LookTargetCollider { get; private set; }
    public Transform LookTargetTransform { get; private set; }
    public Vector3 LookTarget { get; private set; }

    // Move handler
    public Vector2 Speed { get; private set; }
    private Vector2 _moveVelocity;
    [SerializeField]
    private float _moveDrag;
    public float MoveDrag { get { return _moveDrag; } set { _moveDrag = value; } }
    // End - Move handler

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        IKWeapon = GetComponent<IKWeapon>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetWeapon(_inventoryWeapon.GetNextWeapon());
    }

    private void Update()
    {
        float horizontal = Vector2.Dot(transform.right.XZ(), Speed);
        float vertical = Vector2.Dot(transform.forward.XZ(), Speed);
        _animator.SetFloat("Horizontal", horizontal);
        _animator.SetFloat("Vertical", vertical);
        IKWeapon.Target = LookTarget;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateLookAtTarget();
        UpdateSpeedSmoothDamp(Time.fixedDeltaTime);
    }

    private void UpdateLookAtTarget()
    {
        if (LookTargetCollider)
        {
            UpdateLookDirectionAndTarget(LookTargetCollider.bounds.center);
        }
        else if (LookTargetTransform)
        {
            UpdateLookDirectionAndTarget(LookTargetTransform.position);
        }

        if (LookDirection.sqrMagnitude > 0f)
        {
            transform.rotation = Quaternion.LookRotation(LookDirection.XZ(), Vector3.up);
        }
    }

    private void UpdateLookDirectionAndTarget(Vector3 target)
    {
        LookDirection = (target - transform.position).XZ();
        LookTarget = target;
    }
    private Vector2 UpdateSpeedSmoothDamp(float deltaTime)
    {
        return Speed = Vector2.SmoothDamp(Speed, MoveDirection, ref _moveVelocity, _moveDrag > 0f ? 1f / _moveDrag : Mathf.Infinity, Mathf.Infinity, deltaTime);
    }

    public void SetWeapon(AWeapon weapon)
    {
        if (Weapon)
        {
            Weapon.transform.SetParent(_inventoryWeapon.transform, false);
            Weapon.OnUnequip();
            Weapon.gameObject.SetActive(false);
        }

        Weapon = weapon;
        if (weapon)
        {
            weapon.transform.SetParent(_rightHandTransform, false);
            weapon.gameObject.SetActive(true);
            weapon.OnEquip();
        }
    }

    public void SetTarget(Collider collider)
    {
        LookTargetCollider = collider;
        LookTargetTransform = null;
        if (collider)
        {
            LookTarget = collider.bounds.center;
        }
    }

    public void SetTarget(Transform transform)
    {
        LookTargetCollider = null;
        LookTargetTransform = transform;
        if (transform)
        {
            LookTarget = transform.position;
        }
    }

    public void SetTarget(Vector3 position)
    {
        LookTargetCollider = null;
        LookTargetTransform = null;
        LookTarget = position;
    }

    public void Shoot()
    {
        if (Weapon)
        {
            Weapon.OnShootHold();
        }
    }

    public void ShootDown()
    {
        if (Weapon)
        {
            Weapon.OnShootDown();
        }
    }

    public void ShootUp()
    {
        if (Weapon)
        {
            Weapon.OnShootUp();
        }
    }

    #region Input Messages
    public void OnMovement(InputValue value)
    {
        MoveDirection = value.Get<Vector2>();
    }

    public void OnPointer(InputValue value)
    {
        Ray ray = Camera.main.ScreenPointToRay(value.Get<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _actorMask))
        {
            SetTarget(hit.collider);
        }
        else
        {
            LookTargetTransform = null;
            Plane plane = new Plane(Vector3.up, _horizontalAimReference.position);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                SetTarget(ray.GetPoint(distance));
                LookDirection = (LookTarget - transform.position).XZ();
            }
        }
    }

    public void OnShoot(InputValue value)
    {
        Shoot();
    }

    public void OnShootDown(InputValue value)
    {
        ShootDown();
    }

    public void OnShootUp(InputValue value)
    {
        ShootUp();
    }

    public void OnNextWeapon(InputValue value)
    {
        if (value.Get<float>() > 0f)
        {
            SetWeapon(_inventoryWeapon.GetNextWeapon(Weapon));
        }
        else if (value.Get<float>() < 0f)
        {
            SetWeapon(_inventoryWeapon.GetNextWeapon(Weapon, true));
        }
    }
    #endregion
}
