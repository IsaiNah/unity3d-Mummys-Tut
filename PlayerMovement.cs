using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 5.0f;

    private Animator _animator;
    // Update is called once per frame


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizontal,0.0f, vertical);
        movement *= Time.deltaTime * speed;
        
        
        transform.Translate(movement, Space.World);
        
        _animator.SetFloat("VelocityX", horizontal, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityZ", vertical, 0.1f, Time.deltaTime);
    }
}
 