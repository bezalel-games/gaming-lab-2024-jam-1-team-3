using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lefthand : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Vector3 leftThrust ;
    public Vector3 rightThrust ;
    public Vector3 downThrust;
    public Vector3 upThrust;
    private Rigidbody2D _rb;
    public float _brakeFactor;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {

       
        if (Input.GetKey(KeyCode.Space)) {
            _rb.AddForce(-_brakeFactor * _rb.velocity);
       
            // if (_rb.velocity.sqrMagnitude < 0.1) {
            //     _rb.Sleep();
            // }
        }
        
    }

    private void Update()
    {
        Controls();
    }

    private void Controls()
    {
        Vector3 force = Vector3.zero;
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            force+=leftThrust;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            force+=rightThrust;
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            force+=upThrust;
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            force+=downThrust;
        }
        GetComponent<Rigidbody2D>().AddForce(force);
    }
}
