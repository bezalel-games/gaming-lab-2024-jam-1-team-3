using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using Input = UnityEngine.Windows.Input;

public class GameManager : MonoBehaviour
{
    private const string HTML_ALPHA = "<color=#00000000>";
    public static GameManager Instance;
    public Alien _alien;
    private double _tip = 0;
    private float _tipGoal = 20;
    private float _levelTime = 90; 
    private float _currentTime;
    private bool _levelInProgress;
    [SerializeField] private GameObject _astronaut;
    [SerializeField] private GameObject _moons;
    [SerializeField] private GameObject _meteors;
    [SerializeField] private float _startTime = 6;
    [SerializeField] private TextMeshProUGUI _startText;
    [SerializeField] private GameObject _timer;
    private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject _quota;
    private TextMeshProUGUI _quotaText;
    [SerializeField] private GameObject _pause;
    //[SerializeField] private GameObject _pauseTextGO;
    private TextMeshProUGUI _pauseText;
    [SerializeField] private GameObject _unpauseTextGO;
    public GameObject _rope;
    private bool _isPaused;
    

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
        _pauseText = _pause.GetComponent<TextMeshProUGUI>();
        _timerText = _timer.GetComponent<TextMeshProUGUI>();
        _quotaText = _quota.GetComponent<TextMeshProUGUI>();
        //_startText = textobject.GetComponent<TextMeshProUGUI>();
        _startText = _astronaut.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    
    void Start()
    {
        Debug.Log("GameManager Start called.");
        
        //TriggerPizzaRequest();
        _quotaText.text = 0 + " / " + 0;
        StartCoroutine(StartAnimation());
        _currentTime = _levelTime;
        _levelInProgress = true;
        UpdateTimerUI();

        
    }

    void Update()
    {
        if (!_isPaused && _levelInProgress)
        {
            _currentTime -= Time.deltaTime;
            UpdateTimerUI();

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
        _timerText.text = $"{Mathf.FloorToInt(_currentTime / 60):00}:{Mathf.FloorToInt(_currentTime % 60):00}";
    }

    private IEnumerator StartAnimation() 
    {
        StartCoroutine(RollText(_startTime/2,_startText.text ));
        yield return new WaitForSeconds(_startTime);
        _astronaut.SetActive(false);
        SpawnLevelStartGame();
    }
    

    //Taken from this video: https://www.youtube.com/watch?v=jTPOCglHejE&ab_channel=SasquatchBStudios 
    private IEnumerator RollText(float animationTime, string p)
    {
        _startText.text = "";
        string originalText = p;
        Debug.Log("P is :" + p);
        string displayText = "";
        int alphaIndex = 0;
        float typeFraction = animationTime / p.Length;
        foreach (char c in p.ToCharArray())
        {
            alphaIndex++;
            _startText.text = originalText;
            displayText = _startText.text.Insert(alphaIndex, HTML_ALPHA);
            _startText.text = displayText;

            yield return new WaitForSeconds(typeFraction);
        }
    }

    public void AddScore(double tip)
    {
        _tip += System.Math.Round(tip, 1);
        Debug.Log(_tip);
        _quotaText.text = _tip + " / " + _tipGoal;
        if (_tip >= _tipGoal)
        {
            _levelInProgress = false;
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
        _isPaused = true;
        _pauseText.text = "Unpause";
        Time.timeScale = 0f; // Stop time to pause the game
        _pauseText.text = "Unpause";
        _alien.enabled = false;
        //add text "Press ;esc; to resume"
        _unpauseTextGO.SetActive(true);
    }

    public void Resume()
    {
        _isPaused = false;
        Time.timeScale = 1f; // Resume time to unpause the game
        _alien.enabled = true;
        _pauseText.text = "Pause";
        _unpauseTextGO.SetActive(false);
    }

    public void TogglePause()
    {
        if (_isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

}

