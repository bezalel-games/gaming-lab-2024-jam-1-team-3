using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] private GameObject _deliveryTimeGameObject;
    private Image _deliveryTimeIndicator;
    private bool _inEvent;
    [SerializeField] private float _initialPatience = 9;
    private float _patience;
    
    public GameObject _rope;
    void Start()
    {
        _deliveryTimeIndicator = _deliveryTimeGameObject.GetComponent<Image>();
        StartCoroutine(StartPizzaEvent());
        _patience = _initialPatience;
        //transform.position = new Vector3(Random.Range(-7.3f, 7.5f), Random.Range(-4f, 2f), 0);
        RepositionInRect();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _rope.GetComponent<Rope>().AddLink();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RepositionInRect();
        }

        if (_inEvent)
        {
            _patience -= Time.deltaTime; //when alien requests pizza his patiance goes down
            if (_patience < 0)
            {
                PizzeDelivered();
            }
        }
    }

    private IEnumerator StartPizzaEvent()
    {
        yield return new WaitForSeconds(Random.Range(2,6));
        RequestPizza();
    }

    private void RequestPizza()
    {
        _inEvent = true;
        _alien.SetActive(true);
        //spawn alien
        //enable timer and collision
        //inevent = true
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
                //Rope rope = _rope.GetComponent<Rope>();
                //rope.AddLink(); //add rope extension to game manager
                //rope.AddLink();
                PizzeDelivered();
                //StartCoroutine(Reposition());
                _isInside = false;
            }
        }
    }

    private void PizzeDelivered()
    {
        _inEvent = false;
        GameManager.Instance.AddScore(Math.Max(_patience, 0) * 4);
        _patience = _initialPatience;
        _alien.SetActive(false);
        StartCoroutine(StartPizzaEvent());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isInside = false;
        _timeInside = 0;
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
