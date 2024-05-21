using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private GameObject astronaut;
    private Astronaut _astronaut;

     private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _astronaut = astronaut.GetComponent<Astronaut>();

    }

    void Update()
    {
        Vector2 v = _rb.velocity;
        var angle = Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Meteor"))
        {
            _astronaut.Stun();
        }
    }
}
