using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Alien _alien;
    private float _tip = 0;
    private float _tipGoal = 20;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Too many game instances");
        }
    }
    
    void Start()
    {
        Debug.Log("GameManager Start called.");
        //TriggerPizzaRequest();
    }

    public void AddScore(float tip)
    {
        _tip += tip;
        Debug.Log(_tip);
        if (_tip >= _tipGoal)
        {
            Debug.Log("YOU WIN!");
        }
    }
    
    public void TriggerPizzaRequest()
    {
        _alien.RequestPizza();
    }
}

