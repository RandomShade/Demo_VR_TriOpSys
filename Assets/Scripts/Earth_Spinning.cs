using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_Spinning : MonoBehaviour
{
    private bool _isFreezed = false;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) >= 6)
        {
            transform.position = new Vector3(-0.01876929f, 1.488453f, 1.075341f);
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.velocity = Vector3.zero;
        }
    }

    public void PressedButtonUI()
    {
        _isFreezed = !_isFreezed;
        if (_isFreezed)
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        else
            _rigidbody.constraints = RigidbodyConstraints.None;
    }
}
