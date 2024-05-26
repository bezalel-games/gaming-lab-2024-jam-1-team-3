
using System.Collections;
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
        StartCoroutine(IncreaseBar(tip));
    }

    private IEnumerator IncreaseBar(float tip)
    {
        yield return new WaitForSeconds(1f);
        _currentTip = tip;
        _currText.text = string.Format("{0:G}", _currentTip);
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

    void FixedUpdate()
    {
        if (_previousTip <= _currentTip)
        {
            _tipBar.fillAmount = Mathf.Clamp01(_previousTip/_tipGoal);
            _previousTip += _increment;
        }
    }
}
