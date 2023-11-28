using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterShot : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;

    void Start()
    {
        Destroy(gameObject, 5.0f); // Destory this object after 5 sec
    }
    
    public void Launch(Vector3 direction)
    {
        direction.Normalize();
        transform.up = direction;
        GetComponent<Rigidbody>().velocity = direction * _speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
    
   
}
