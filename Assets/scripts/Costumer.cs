using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Costumer : MonoBehaviour
{
    [SerializeField] private GameObject _thoughtBubble;
    private bool _isInside;
    private float _timeInside;
    [SerializeField] private float _requiredTime = 3f;
    [SerializeField] private GameObject _alien;
    private Alien _alienClass;
    private CostumerMovement _cm;
    
    //delivery time parameters
    [SerializeField] private GameObject _deliveryTimeGameObject;
    private Image _deliveryTimeIndicator;
    private bool _inEvent;
    private float _initialPatience;
    private float _patience;
    
    //Status indicators
    [SerializeField] private GameObject _flipOff;
    
    
    private void Awake()
    {
        _cm = GetComponent<CostumerMovement>();
        _initialPatience = 9 ;//TODO GameManager.Instance._customerPatience;
        _alienClass = _alien.GetComponent<Alien>();
        _deliveryTimeIndicator = _deliveryTimeGameObject.GetComponent<Image>();
    }

    void Start()
    {
        _patience = _initialPatience;
    }
    private void Update()
    {
        if (_inEvent)
        {
            if (!_isInside)
            {
                _patience -= Time.deltaTime; //when alien requests pizza his patiance goes down
            }
            if (_patience < 0)
            {
                FailedDelivery();
            }
        }
    }
    IEnumerator FlipOffSequence()
    {
        yield return new WaitForSeconds(0.3f);
        _flipOff.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        _flipOff.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        _alien.SetActive(false);
        _cm.EndEvent();
    }

    private void FailedDelivery()
    {
        _patience = _initialPatience;
        _inEvent = false;
        _alienClass.HideThoughtBubble();
        StartCoroutine(FlipOffSequence());
    }

    public IEnumerator StartPizzaEvent()
    {
        yield return new WaitForSeconds(1.5f);
        RequestPizza();
    }

    private void RequestPizza()
    {
        Debug.Log("Starting Costumer Pizza event");
        _inEvent = true;
        _alien.SetActive(true);
    }
    private void PizzeDelivered()
    {
        //TODO Add here a successful delivery indicator
        _inEvent = false;
        GameManager.Instance.AddScore(Math.Max(_patience, 0));
        _patience = _initialPatience;
        _alien.SetActive(false);
        _cm.EndEvent();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Pizza") & _inEvent)
        {
            _isInside = true;
            _timeInside += Time.deltaTime;
            _deliveryTimeIndicator.fillAmount = _timeInside / _requiredTime;
            
            if (_timeInside >= _requiredTime && _isInside)
            {
                //TODO add pizza delivery animation
                PizzeDelivered();
                _isInside = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isInside = false;
        _timeInside = 0;
        _deliveryTimeIndicator.fillAmount = _timeInside;
    }
}
