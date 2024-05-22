using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using WaitForSeconds = UnityEngine.WaitForSeconds;


public class DeliveryPoint : MonoBehaviour
{
    
    private bool _isInside;
    private float _timeInside;
    [SerializeField] private float _requiredTime = 3f;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private float _xDistance;
    [SerializeField] private float _yDistance;
    [SerializeField] private GameObject _alien;
    private Alien _alienClass;

    [SerializeField] private Sprite _openMoon;
    [SerializeField] private Sprite _closedMoon;
    [SerializeField] private Sprite _happyMoon;
    [SerializeField] private Sprite _sadMoon;
    
    //delivery time parameters
    [SerializeField] private GameObject _deliveryTimeGameObject;
    private Image _deliveryTimeIndicator;
    private bool _inEvent;
    private float _initialPatience;
    private float _patience;
    
    
    //Status indicators
    [SerializeField] private GameObject _particles;
    private ParticleSystem _ps;
    private SpriteRenderer _sr;
    [SerializeField] private GameObject _flipOff;
    [SerializeField] private GameObject _halo;
    private SpriteRenderer _haloSR;
    [SerializeField] private GameObject _transfer;
    private Animator _transferAnim;
    private SpriteRenderer _transferSR;
    private static readonly int Time1 = Animator.StringToHash("Time");


    private void Awake()
    {
        _transferSR = _transfer.GetComponent<SpriteRenderer>();
        _transferAnim = _transfer.GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _haloSR = _halo.GetComponent<SpriteRenderer>();
        _ps = _particles.GetComponent<ParticleSystem>();
        _alienClass = _alien.GetComponent<Alien>();
        _deliveryTimeIndicator = _deliveryTimeGameObject.GetComponent<Image>();
    }

    void Start()
    {
        _transferAnim.SetFloat(Time1, _requiredTime);
        _initialPatience = GameManager.Instance._customerPatience;
        StartCoroutine(StartPizzaEvent());
        _patience = _initialPatience;
        RepositionInRect();
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            RepositionInRect();
        }

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
        _sr.sprite = _sadMoon;
        _flipOff.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        _sr.sprite = _closedMoon;
        _flipOff.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        _alien.SetActive(false);
        StartCoroutine(StartPizzaEvent());
    }

    private void FailedDelivery()
    {
        _patience = _initialPatience;
        _inEvent = false;
        _alienClass.HideThoughtBubble();
        StartCoroutine(FlipOffSequence());
    }

    private IEnumerator StartPizzaEvent()
    {
        yield return new WaitForSeconds(Random.Range(2,6));
        _sr.sprite = _openMoon; 
        RequestPizza();
    }

    private void RequestPizza()
    {
        _inEvent = true;
        _alien.SetActive(true);
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
        if (other.CompareTag("Pizza") & _inEvent && !Movement.isStunned)
        {
            _transfer.SetActive(true);
            _isInside = true;
            _timeInside += Time.deltaTime;
            //_deliveryTimeIndicator.fillAmount = _timeInside / _requiredTime;
            if (_timeInside >= _requiredTime && _isInside)
            {
                _timeInside = 0;
                StartCoroutine(PizzeDelivered());
                _isInside = false;
            }
        }
        
        if (Movement.isStunned)
        {
            _timeInside = 0;
            _deliveryTimeIndicator.fillAmount = 0;
        }
    }

    private IEnumerator PizzeDelivered()
    {
        _transfer.SetActive(false);
        _ps.Play();
        _inEvent = false;
        GameManager.Instance.AddScore(Math.Max(_patience, 0));
        _patience = _initialPatience;
        _alien.SetActive(false);
        _sr.sprite = _happyMoon;
        yield return new WaitForSeconds(1.5f);
        _sr.sprite = _closedMoon;
        StartCoroutine(StartPizzaEvent());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _haloSR.enabled = false;
        _isInside = false;
        _timeInside = 0;
        _transfer.SetActive(false);
        _deliveryTimeIndicator.fillAmount = _timeInside;
    }

    private void RepositionInRect()
    {
        //Repositions object at random point around spawnPoint
        float xpos = _spawnPoint.transform.position.x + Random.Range(-_xDistance, _xDistance);
        float ypos = _spawnPoint.transform.position.y + Random.Range(-_yDistance, _yDistance);
        transform.position = new Vector3(xpos, ypos, 0);
    }
    
    IEnumerator Reposition()
    {
        transform.position = new Vector3(12, 0, 0);
        yield return new WaitForSeconds(Random.Range(1.5f, 2.5f));
        transform.position = new Vector3(Random.Range(-7.3f, 7.5f), Random.Range(-4f, 2f), 0);
    }
}
