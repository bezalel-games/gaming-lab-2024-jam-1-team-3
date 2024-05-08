using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    

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
