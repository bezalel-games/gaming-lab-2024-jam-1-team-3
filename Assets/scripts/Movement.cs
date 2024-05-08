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

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
           
        }
        else
        {
            _rb.constraints = RigidbodyConstraints2D.None;
        }
        
    }

    private void Update()
    {
        Controls();
    }

    private void Controls()
    {
        Vector3 force = Vector3.zero;
        if(Input.GetKey(KeyCode.A))
        {
            force+=leftThrust;
        }
        if(Input.GetKey(KeyCode.D))
        {
            force+=rightThrust;
        }
        if(Input.GetKey(KeyCode.W))
        {
            force+=upThrust;
        }
        if(Input.GetKey(KeyCode.S))
        {
            force+=downThrust;
        }
        GetComponent<Rigidbody2D>().AddForce(force);
    }
}
