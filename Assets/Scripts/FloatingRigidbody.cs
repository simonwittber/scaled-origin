using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatingRigidbody : FloatingComponent
{
    new Rigidbody rigidbody;
    Collider[] colliders;
    bool enableColliders = false;

    protected override void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }

    void FixedUpdate()
    {
        if (isOOB)
            OutOfBounds();
        else
            InBounds();
    }

    void OutOfBounds()
    {
        isOOB = (Math.Abs(x) > MAXRANGE || Math.Abs(y) > MAXRANGE || Math.Abs(z) > MAXRANGE);
        if (!isOOB)
        {
            enableColliders = true;
            gameObject.layer = normalLayer;
            translationScale = 1f;
            transform.localScale = Vector3.one * (float)originalScale;
        }
        if (enableColliders)
        {
            foreach (var i in colliders)
                i.enabled = true;
            enableColliders = false;
        }
        transform.position = new Vector3((float)x * translationScale, (float)y * translationScale, (float)z * translationScale);
        rigidbody.MovePosition(transform.position);
    }

    void InBounds()
    {
        isOOB = (Math.Abs(x) > MAXRANGE || Math.Abs(y) > MAXRANGE || Math.Abs(z) > MAXRANGE);
        if (isOOB)
        {
            isOOB = true;
            foreach (var i in colliders)
                i.enabled = false;
            gameObject.layer = skyboxLayer;
            translationScale = 1.0f / MAXRANGE;
            transform.localScale = Vector3.one * (float)originalScale * translationScale;
        }
        transform.position = new Vector3((float)x * translationScale, (float)y * translationScale, (float)z * translationScale);
        rigidbody.MovePosition(transform.position);
    }

}
