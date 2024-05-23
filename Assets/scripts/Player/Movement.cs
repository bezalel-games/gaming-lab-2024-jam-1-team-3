using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static bool isStunned;
    [SerializeField] private float _thrustForce;
    private Vector3 leftThrust ;
    private Vector3 rightThrust ;
    private Vector3 downThrust;
    private Vector3 upThrust;
    private Rigidbody2D _rb;
    public float _brakeFactor;
    private bool _brakeIsOn = false;
    private Vector3 _force;
    private float stunDuration = 2f; // Duration of stun in seconds
    private float stunTimer = 0f;


    private void Start()
    {

        leftThrust = new Vector3(-_thrustForce, 0, 0);
        rightThrust = new Vector3(_thrustForce, 0, 0);
        downThrust = new Vector3(0, -_thrustForce,  0);
        upThrust = new Vector3(0, _thrustForce,  0);


        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        if (isStunned) return;
        if (_brakeIsOn)
        {
            _rb.AddForce(-_brakeFactor * _rb.velocity);
        }
        _rb.AddForce(_force);

    }

    private void Update()
    {
        if (isStunned)
        {
            _force = Vector3.zero; // Stop the spaceship
            _rb.velocity = Vector2.zero;
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                isStunned = false;
            }
            return;
        }
        Controls();
    }

    private void Controls()
    {
        _force = Vector3.zero;
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _force+=leftThrust;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _force+=rightThrust;
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _force+=upThrust;
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _force+=downThrust;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _brakeIsOn = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _brakeIsOn = false;
        }
    }

    

    public void StunMovement()
    {
        isStunned = true;
        stunTimer = stunDuration;
        _force = Vector3.zero; // Stop the spaceship
        _rb.velocity = Vector2.zero; // Stop all movement
        
    }
}
