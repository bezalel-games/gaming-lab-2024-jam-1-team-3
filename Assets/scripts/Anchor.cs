using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    public GameObject _ship;
    

    void Update()
    {

        transform.position = _ship.transform.position;
        transform.rotation = _ship.transform.rotation;

    }
}
