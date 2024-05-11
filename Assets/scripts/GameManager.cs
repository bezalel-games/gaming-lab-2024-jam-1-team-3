using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Alien alien;

    void Start()
    {
        Debug.Log("GameManager Start called.");
        TriggerPizzaRequest();
    }


    // void Update()
    // {
        
    // }


    public void TriggerPizzaRequest()
    {
        alien.RequestPizza();
    }
}

