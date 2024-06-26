using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Costumer : MonoBehaviour
{
    private bool _inFlipOffSequence;
    private bool _overwriteThoguhtBubble;

    [SerializeField] private MoneyMaker _omryParticles;
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
    public float _patience;
    
    [SerializeField] private Sprite _openShip;
    [SerializeField] private Sprite _closedShip;
    [SerializeField] private Sprite _happyShip;
    [SerializeField] private Sprite _sadShip;
    
    //Status indicators
    [SerializeField] private GameObject _flipOff;
    private SpriteRenderer _sr;
    [SerializeField] private GameObject _particles;
    [SerializeField] private GameObject _transfer;
    [SerializeField] private SpriteRenderer _haloSR;
    [SerializeField] private GameObject _infaltingBubble;
    private bool _transferSoundPlayed;


    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _cm = GetComponent<CostumerMovement>();
        _initialPatience = GameManager.Instance._customerPatience;
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
        Audio.AudioController.PlayCommand(Audio.AudioController._pizzaDeliveryFailed);
        _inFlipOffSequence = true;
        yield return new WaitForSeconds(0.3f);
        _sr.sprite = _sadShip;
        _flipOff.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        _sr.sprite = _closedShip;
        _flipOff.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        _inFlipOffSequence = false;
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
        yield return new WaitForSeconds(0.7f);
        _sr.sprite = _openShip;
        _alien.SetActive(true);
        Audio.AudioController.PlayCommand(Audio.AudioController._alienRequestsPizza);
        yield return new WaitForSeconds(1);
        _inEvent = true;
    }
    
    private IEnumerator PizzeDelivered()
    {
        _transfer.SetActive(false);
        _omryParticles.CreateSplash();
        _inEvent = false;
        GameManager.Instance.AddScore(Math.Max(_patience, 0));
        _patience = _initialPatience;
        _alien.SetActive(false);
        _sr.sprite = _happyShip;
        Audio.AudioController.PlayCommand(Audio.AudioController._pizzaDeliverySuccess);
        yield return new WaitForSeconds(1.5f);
        _sr.sprite = _closedShip;
        _cm.EndEvent();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pizza") && !Movement.isStunned)
        {
            _haloSR.enabled = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Pizza") & _inEvent & !Movement.isStunned)
        {
            StartTransferSound();
            _haloSR.enabled = true;
            _overwriteThoguhtBubble = true;
            _alienClass.HideThoughtBubble(); //hide indicator bubbles when doing a delivery
            _isInside = true;
            _transfer.SetActive(true);
            _timeInside += Time.deltaTime;
            if (_timeInside >= _requiredTime && _isInside)
            {
                _timeInside = 0;
                //TODO add pizza delivery animation
                StartCoroutine(PizzeDelivered());
                _isInside = false;
            }
        }
        if (Movement.isStunned)
        {
            //Audio.AudioController._as.Stop();
            _transfer.SetActive(false);
            _timeInside = 0;
        }
    }
    
    private void StartTransferSound()
    {
        if (!_transferSoundPlayed && !Movement.isStunned)
        {
            _transferSoundPlayed = true;
            Audio.AudioController.PlayCommand(Audio.AudioController._transfersound);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_overwriteThoguhtBubble && !_inFlipOffSequence)
        {
            _alienClass.thoughtBubble.SetActive(true);
        }
        
        if (isActiveAndEnabled)
        {
            if (GameManager.Instance.GetTip() < GameManager.Instance.GetTipGoal() && isActiveAndEnabled)
            {
                StartCoroutine(HaloCoolDown());
            }
        }
        _transferSoundPlayed = false;
        _isInside = false;
        _timeInside = 0;
        _transfer.SetActive(false);
        _deliveryTimeIndicator.fillAmount = _timeInside;
    }
    
    private IEnumerator HaloCoolDown()
    {
        yield return new WaitForSeconds(0.2f);
        _haloSR.enabled = false;
    }
}
