using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Vector3 leftThrust ;
    public Vector3 rightThrust ;
    public Vector3 downThrust;
    public Vector3 upThrust;
    void FixedUpdate () {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (Input.GetKey(KeyCode.RightShift))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
           
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
            Controls();
        }
    }

    private void Controls()
    {
        Vector3 force = Vector3.zero;
        if(Input.GetKey(KeyCode.K))
        {
            force+=leftThrust;
        }
        if(Input.GetKey(KeyCode.Semicolon))
        {
            force+=rightThrust;
        }
        if(Input.GetKey(KeyCode.O))
        {
            force+=upThrust;
        }
        if(Input.GetKey(KeyCode.L))
        {
            force+=downThrust;
        }
        GetComponent<Rigidbody2D>().AddForce(force);
    }
}