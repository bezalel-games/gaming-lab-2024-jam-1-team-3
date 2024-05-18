using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    private Rigidbody2D _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 v = _rb.velocity;
        var angle = Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Meteor"))
        {
            Debug.Log("YOU LOOSE!");
            Debug.Log("EXPLOSION!");
            Destroy(transform.parent.gameObject);
        }
    }
}
