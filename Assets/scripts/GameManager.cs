using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Input = UnityEngine.Windows.Input;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Alien _alien;
    private float _tip = 0;
    private float _tipGoal = 20;
    private float _levelTime = 90; 
    private float _currentTime;
    private bool _levelInProgress;
    [SerializeField] private GameObject _moons;
    [SerializeField] private GameObject _meteors;
    [SerializeField] private float _startTime = 6;
    [SerializeField] private GameObject _timer;
    private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject _tipGameObject;
    private TipBar _tipBar;
    public GameObject _rope;
    private bool _isPaused;
    private bool _gameStart;
    

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

        _tipBar = _tipGameObject.GetComponent<TipBar>();
        _timerText = _timer.GetComponent<TextMeshProUGUI>();
    }
    
    void Start()
    {
        Debug.Log("GameManager Start called.");
        _currentTime = _levelTime;
        _levelInProgress = true;
    }

    void Update()
    {
        if (!_isPaused && _levelInProgress)
        {
            if (_gameStart)
            {
                _currentTime -= Time.deltaTime;
                UpdateTimerUI();
            }
            if (_currentTime <= 0)
            {
                _currentTime = 0;
                _levelInProgress = false;
                Debug.Log("Time's up! YOU LOSE!");
                
            }
        }
    }

   private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(_currentTime / 60);
        int seconds = Mathf.FloorToInt(_currentTime % 60);
        string formattedTime = string.Format("{0}:{1:00}", minutes, seconds);
        _timerText.text = formattedTime;
    }

    public void AddScore(double tip)
    {
        _tip += (float)Math.Round(tip, 1);
        Debug.Log(_tip);
        //add tip updater
        _tipBar.UpdateTip(_tip);
        if (_tip >= _tipGoal)
        {
            _levelInProgress = false;
            Debug.Log("YOU WIN!");
        }
    }
    public void SpawnLevelStartGame() 
    {
        //TODO add countdown or something
        _gameStart = true;
        _moons.SetActive(true);
        _meteors.SetActive(true);
    }
 
}

