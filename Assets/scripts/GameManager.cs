using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Alien _alien;
    private float _tip = 0;
    private float _tipGoal = 20;
    [SerializeField] private GameObject _astronaut;
    [SerializeField] private GameObject _moons;
    [SerializeField] private GameObject _meteors;
    [SerializeField] private float _startTime = 3;

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

    private IEnumerator StartAnimation() 
    {
        RollText(_startTime / 2);
        yield return new WaitForSeconds(_startTime);
        _astronaut.SetActive(false);
        SpawnLevelStartGame();
    }

    private void RollText(float animationTime) { }

    public void AddScore(float tip)
    {
        _tip += tip;
        Debug.Log(_tip);
        if (_tip >= _tipGoal)
        {
            Debug.Log("YOU WIN!");
        }
    }

    private void SpawnLevelStartGame() 
    {
        _moons.SetActive(true);
        _meteors.SetActive(true);
    }
    
    public void TriggerPizzaRequest()
    {
        _alien.RequestPizza();
    }

    public void Pause()
    {
        Time.timeScale = 0f; // Stop time to pause the game
        _alien.enabled = false;
    }

    public void Resume()
    {
        Time.timeScale = 1f; // Resume time to unpause the game
        _alien.enabled = true;
    }
    
}

