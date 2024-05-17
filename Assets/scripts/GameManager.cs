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
    private const string HTML_ALPHA = "<color=#00000000>";
    public static GameManager Instance;
    public Alien _alien;
    private double _tip = 0;
    private float _tipGoal = 20;
    private float _levelTime = 90; 
    private float _currentTime;
    private bool _levelInProgress;
    [SerializeField] private GameObject _astronautStart;
    [SerializeField] private GameObject _moons;
    [SerializeField] private GameObject _meteors;
    [SerializeField] private float _startTime = 6;
    [SerializeField] private TextMeshProUGUI _startText;
    [SerializeField] private GameObject _timer;
    private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject _quota;
    private TextMeshProUGUI _quotaText;
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
        _timerText = _timer.GetComponent<TextMeshProUGUI>();
        _quotaText = _quota.GetComponent<TextMeshProUGUI>();
        _startText = _astronautStart.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
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
        int minutes = Mathf.FloorToInt(_currentTime / 60);
        int seconds = Mathf.FloorToInt(_currentTime % 60);
        string formattedTime = string.Format("{0}:{1:00}", minutes, seconds);
        _timerText.text = formattedTime;
    }

    private IEnumerator StartAnimation() 
    {
        StartCoroutine(RollText(_startTime/2,_startText.text ));
        yield return new WaitForSeconds(_startTime);
        _astronautStart.SetActive(false);
        yield return new WaitForSeconds(3);
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
}

