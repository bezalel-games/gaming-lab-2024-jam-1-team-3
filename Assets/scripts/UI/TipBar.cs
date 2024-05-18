using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TipBar : MonoBehaviour
{
    private float _currentTip;
    private float _tipGoal = 1;
    private float _increment = 0.02f;
    private TextMeshProUGUI _goalText;
    private TextMeshProUGUI _currText;
    private Image _tipBar;
    private float _previousTip;

    public void UpdateTip(float tip)
    {
        _currentTip = tip;
        _currText.text = string.Format("{0:G}", _currentTip);
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
        _tipGoal = GameManager.Instance.getTipGoal();
        _goalText.text = "";
        _currText.text = "0";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_previousTip <= _currentTip)
        {
            _tipBar.fillAmount = Mathf.Clamp01(_previousTip/_tipGoal);
            _previousTip += _increment;
        }
    }
}
