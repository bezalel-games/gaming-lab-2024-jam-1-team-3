using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Meteor"))
        {
            Debug.Log("YOU LOOSE!");
            Debug.Log("EXPLOSION!");
            Destroy(transform.parent.gameObject);
        }
    }
}
