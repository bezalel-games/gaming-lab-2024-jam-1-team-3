using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TipBar : MonoBehaviour
{
    private float _currentTip;
    private float _tipGoal;
    private float _increment = 0.1f;
    private TextMeshProUGUI _goalText;
    private TextMeshProUGUI _currText;
    private Image _tipBar;

    public void UpdateTip(float tip)
    {
        _currentTip = tip;
        //_currText.text = string.Format(_currentTip);
    }

    private void Awake()
    {
        _tipBar = transform.GetChild(0).gameObject.GetComponent<Image>();
        _goalText = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        _currText = transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        _goalText.text = "";
        _currText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
