using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Actor : MonoBehaviour
{
    public Collider Collider { get; private set; }

    protected virtual void Awake()
    {
        this.Collider = GetComponent<Collider>();
    }
}
