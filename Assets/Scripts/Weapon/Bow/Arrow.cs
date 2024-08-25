using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Arrow : MonoBehaviour {

    private Rigidbody _rigidbody;
    private Collider _collider;

    private float _damage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if (_rigidbody.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            transform.SetParent(enemy.transform);
            Debug.Log(Convert.ToInt32(_damage));
        }

        _collider.enabled = false;
        _rigidbody.isKinematic = true;
    }

    public void SetToRope(Transform ropeTransform)
    {
        transform.parent = ropeTransform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        _rigidbody.isKinematic = true;
    }

    public void Shot(float velocity)
    {
        _damage = velocity;

        _collider.enabled = true;
        transform.parent = null;
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = transform.forward * velocity;
    }
}
