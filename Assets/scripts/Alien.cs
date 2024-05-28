using System;
using System.Collections;
using UnityEngine;


public class Alien : MonoBehaviour
{
    public Costumer _cm;
    public DeliveryPoint _dv;
    public GameObject thoughtBubble; 
    public SpriteRenderer bubbleRenderer;
    [SerializeField] private Sprite _sprite1;
    [SerializeField] private Sprite _sprite2;
    [SerializeField] private Sprite _sprite3;
    [SerializeField] private GameObject _infaltingBubble;

    private bool _deliveryPointNull;
    private bool _costumerNull;


    private void Awake()
    {
        if (_dv == null)
        {
            _deliveryPointNull = true;
        }

        if (_cm == null)
        {
            _costumerNull = true;
        }
        bubbleRenderer = thoughtBubble.GetComponent<SpriteRenderer>();
        thoughtBubble.SetActive(false); // Initially hide the thought bubble
    }

    private float GetPatience()
    {
        if (!_deliveryPointNull)
        {
            return _dv._patience;
        }
        else if (!_costumerNull)
        {
            return _cm._patience;
        }
        return 0;
    }

    private void OnEnable()
    {
        StartCoroutine(RequestPizza());
    }

    // private void OnDisable()
    // {
    //     thoughtBubble.SetActive(false);
    // }
    
    private IEnumerator RequestPizza()
    {
        thoughtBubble.SetActive(false);
        _infaltingBubble.SetActive(true);
        yield return new WaitForSeconds(1f);
        _infaltingBubble.SetActive(false);
        thoughtBubble.SetActive(true);
        //StartCoroutine(ChangeBubbleColorOverTime(GameManager.Instance._customerPatience)); 
    }

    private void Update()
    {
        var currPatience = GetPatience();
        var customerPatience = GameManager.Instance._customerPatience;
        //Debug.Log(_dv._patience);
        if (currPatience > 0.65 * customerPatience)
        {
            bubbleRenderer.sprite = _sprite1;
        }
        else if (currPatience > 0.35 * customerPatience)
        {
            bubbleRenderer.sprite = _sprite2;
        }
        else
        {
            bubbleRenderer.sprite = _sprite3;
        }
    }

    // private IEnumerator ChangeBubbleColorOverTime(float patience)
    // {
    //     bubbleRenderer.sprite = _sprite1;
    //     yield return new WaitForSeconds(patience*0.35f); 
    //     bubbleRenderer.sprite = _sprite2;
    //     yield return new WaitForSeconds(patience*0.3f); 
    //     bubbleRenderer.sprite = _sprite3;
    //     yield return new WaitForSeconds(patience*0.35f); 
    // }

    public void HideThoughtBubble()
    {
        bubbleRenderer.sprite = null;
        thoughtBubble.SetActive(false);
    }
}


