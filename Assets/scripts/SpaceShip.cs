using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
