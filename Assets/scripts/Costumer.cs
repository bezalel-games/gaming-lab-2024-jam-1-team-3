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
    private float _patience;
    
    [SerializeField] private Sprite _openShip;
    [SerializeField] private Sprite _closedShip;
    [SerializeField] private Sprite _happyShip;
    [SerializeField] private Sprite _sadShip;
    
    //Status indicators
    [SerializeField] private GameObject _flipOff;
    private SpriteRenderer _sr;
    private ParticleSystem _ps;
    [SerializeField] private GameObject _particles;
    [SerializeField] private GameObject _transfer;
    private Animator _transferAnim;
    private static readonly int Time1 = Animator.StringToHash("Time");
    [SerializeField] private SpriteRenderer _haloSR;
    [SerializeField] private GameObject _infaltingBubble;

    
    private void Awake()
    {
        _transferAnim = _transfer.GetComponent<Animator>();
        _ps = _particles.GetComponent<ParticleSystem>();
        _sr = GetComponent<SpriteRenderer>();
        _cm = GetComponent<CostumerMovement>();
        _initialPatience = GameManager.Instance._customerPatience;
        _alienClass = _alien.GetComponent<Alien>();
        _deliveryTimeIndicator = _deliveryTimeGameObject.GetComponent<Image>();
    }

    void Start()
    {
        _transferAnim.SetFloat(Time1, _requiredTime);
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
            _haloSR.enabled = true;
            _overwriteThoguhtBubble = true;
            _alienClass.HideThoughtBubble(); //hide indicator bubbles when doing a delivery
            _isInside = true;
            _transfer.SetActive(true);
            _timeInside += Time.deltaTime;
            //_deliveryTimeIndicator.fillAmount = _timeInside / _requiredTime;
            
            if (_timeInside >= _requiredTime && _isInside)
            {
                _timeInside = 0;
                //TODO add pizza delivery animation
                StartCoroutine(PizzeDelivered());
                _isInside = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // _isInside = false;
        // _timeInside = 0;
        // _deliveryTimeIndicator.fillAmount = _timeInside;
        
        if (_overwriteThoguhtBubble && !_inFlipOffSequence)
        {
            _alienClass.thoughtBubble.SetActive(true);
        }
        
        if (isActiveAndEnabled)
        {
            if (GameManager.Instance.getTip() < GameManager.Instance.getTipGoal())
            {
                StartCoroutine(HaloCoolDown());
            }
        }
        
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
