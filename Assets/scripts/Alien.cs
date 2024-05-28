using System.Collections;
using UnityEngine;


public class Alien : MonoBehaviour
{
    public GameObject thoughtBubble; 
    public SpriteRenderer bubbleRenderer;
    [SerializeField] private Sprite _sprite1;
    [SerializeField] private Sprite _sprite2;
    [SerializeField] private Sprite _sprite3;
    [SerializeField] private GameObject _infaltingBubble;

    private void Awake()
    {
        bubbleRenderer = thoughtBubble.GetComponent<SpriteRenderer>();
        // thoughtBubble.SetActive(false); // Initially hide the thought bubble
    }

    private void OnEnable()
    {
        StartCoroutine(RequestPizza());
    }

    private void OnDisable()
    {
        thoughtBubble.SetActive(false);
    }

    private IEnumerator RequestPizza()
    {
        _infaltingBubble.SetActive(true);
        yield return new WaitForSeconds(1f);
        _infaltingBubble.SetActive(false);
        thoughtBubble.SetActive(true);
        StartCoroutine(ChangeBubbleColorOverTime(9)); //TODO GameManager.Instance._customerPatience)); 
    }

    private IEnumerator ChangeBubbleColorOverTime(float patience)
    {
        // Debug.Log("Thought bubble active state after hiding: " + thoughtBubble.activeSelf);  // This should log 'false'.
        
        bubbleRenderer.sprite = _sprite1;
        yield return new WaitForSeconds(patience*0.35f); 
        bubbleRenderer.sprite = _sprite2;
        yield return new WaitForSeconds(patience*0.3f); 
        bubbleRenderer.sprite = _sprite3;
        yield return new WaitForSeconds(patience*0.35f); 
    }

    public void HideThoughtBubble()
    {
        bubbleRenderer.sprite = null;
        thoughtBubble.SetActive(false);
    }
    
    
}


