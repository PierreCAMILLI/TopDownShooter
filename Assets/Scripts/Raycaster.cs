using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    private LayerMask _layerMask;

    private Vector3 _hitPoint;
    public Vector3 HitPoint { get { return _hitPoint; } }
    private Collider _target;
    public Collider Target { get { return _target; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (_lineRenderer)
        {
            _lineRenderer.SetPositions(new Vector3[2] { transform.position, _hitPoint });
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
        {
            _hitPoint = hit.point;
            _target = hit.collider;
        }
        else
        {
            _hitPoint = transform.position + (transform.forward * 10000f);
            _target = null;
        }
    }
}
