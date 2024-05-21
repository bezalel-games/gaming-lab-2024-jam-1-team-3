using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
    


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
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject _tipGameObject;
    private TipBar _tipBar;
    public GameObject _rope;
    private bool _isPaused;
    private bool _gameStart;
    public float _customerPatience;
    [SerializeField] private float _tipFactor = 1;

    

    void Awake()
    {
        _customerPatience = 9;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Too many game instances");
        }
        PauseGame._isPaused = false;
        _tipBar = _tipGameObject.GetComponent<TipBar>();
        _timerText = _timer.GetComponent<TextMeshProUGUI>();
    }
    
    void Start()
    {
        _currentTime = _levelTime;
        _levelInProgress = true;
         MenuPanel.SetActive(false);
         

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
                SceneManager.LoadScene("GameOver");
                
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
        _tip += (float)Math.Round(_tip * _tipFactor, 1);
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

    public float getTipGoal()
    {
        return _tipGoal;
    }

    public float getCostumerPatience()
    {
        return _customerPatience;
    }

}


    

