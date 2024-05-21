using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Astronaut : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool isStunned = false;
    private float stunDuration = 2f; // Duration of stun in seconds
    private float stunTimer = 0f;
    [SerializeField] private GameObject movement;
    private Movement _movement;

     private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movement = movement.GetComponent<Movement>();

    }


    private void Update()
    {
        if (isStunned)
                {
                    stunTimer -= Time.deltaTime;
                    if (stunTimer <= 0)
                    {
                        isStunned = false;
                        GetComponent<SpriteRenderer>().color = Color.white; // Reset color to white
                    }
                    return;
                }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Meteor"))
        {
            Stun();
        }
    }

    public void Stun()
    {
        _movement.StunMovement();
        isStunned = true;
        stunTimer = stunDuration;
        GetComponent<SpriteRenderer>().color = Color.red;   
    }
}

