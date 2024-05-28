using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
    


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    //tip variables
    [SerializeField] private float _tipFactor = 1;
    [SerializeField] private float _tipGoal = 20;
    private float _tip;
    private TipBar _tipBar;

    //Level time variables
    [SerializeField] private float _levelTime = 90; 
    private float _currentTime;
    private bool _levelInProgress;
    private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject MenuPanel;
    private bool _tenSecondsFlag = true;
    
    //GameObjects we need in game manager
    // [SerializeField] private GameObject _moons;
    // [SerializeField] private GameObject _meteors;
    // [SerializeField] private GameObject _costumers;
    [SerializeField] private GameObject _LevelObjects;
    [SerializeField] private GameObject _timer;
    [SerializeField] private GameObject _tipGameObject;
    public GameObject _rope;
    
    [SerializeField] public float _startTime = 6;
   
    private bool _isPaused;
    private bool _gameStart;
    public float _customerPatience = 9;
    
    void Awake()
    {
        //_customerPatience = 9;
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
        MenuPanel.SetActive(false);
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
                //TODO add coroutine to loose and win game
                Audio.AudioController.PlayCommand(Audio.AudioController._looseSound);

                _currentTime = 0;
                _levelInProgress = false;
                SceneManager.LoadScene("GameOver");
                
                Debug.Log("Time's up! YOU LOSE!");
            }

            if (_currentTime <= 10 && _tenSecondsFlag)
            {
                _tenSecondsFlag = false;
                StartCoroutine(TenSecondsLeft());
            }
        }
    }

    private IEnumerator TenSecondsLeft()
    {
        var textColor = _timerText.color;
        Audio.AudioController.PlayCommand(Audio.AudioController._10SecondsLeft);
        for (int i = 0; i < 3; i++)
        {
            _timerText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            _timerText.color = textColor;
            yield return new WaitForSeconds(0.5f);
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
        _tip += (float)Math.Round(tip * _tipFactor, 0);
        Debug.Log(_tip);
        //add tip updater
        _tipBar.UpdateTip(_tip);
        if (_tip >= _tipGoal)
        {
            Audio.AudioController.PlayCommand(Audio.AudioController._winSound);
            _levelInProgress = false;
            Debug.Log("YOU WIN!");
              SceneManager.LoadScene("WIN");
        }
    }
    public void SpawnLevelStartGame() 
    {
        //TODO add countdown or something
        _gameStart = true;
        _LevelObjects.SetActive(true);
   
    }

    public float getTipGoal()
    {
        return _tipGoal;
    }
    public float getTip()
    {
        return _tip;
    }

    public float getCostumerPatience()
    {
        return _customerPatience;
    }
}


    

