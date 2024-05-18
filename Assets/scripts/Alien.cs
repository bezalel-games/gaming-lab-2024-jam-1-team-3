using System.Collections;
using UnityEngine;


public class Alien : MonoBehaviour
{
    public GameObject thoughtBubble; 
    public SpriteRenderer bubbleRenderer;
    [SerializeField] private Sprite _sprite1;
    [SerializeField] private Sprite _sprite2;
    [SerializeField] private Sprite _sprite3;

    private void Awake()
    {
        bubbleRenderer = thoughtBubble.GetComponent<SpriteRenderer>();
        // thoughtBubble.SetActive(false); // Initially hide the thought bubble
    }

    private void OnEnable()
    {
        RequestPizza();
    }

    private void OnDisable()
    {
        thoughtBubble.SetActive(false);
    }

    private void RequestPizza()
    {
        thoughtBubble.SetActive(true);
        StartCoroutine(ChangeBubbleColorOverTime(9)); //TODO GameManager.Instance._customerPatience;
    }

    private IEnumerator ChangeBubbleColorOverTime(float patience)
    {
        // Debug.Log("Thought bubble active state after hiding: " + thoughtBubble.activeSelf);  // This should log 'false'.
        bubbleRenderer.sprite = _sprite1;
        yield return new WaitForSeconds(patience / 3); // Wait for 2 seconds
        bubbleRenderer.sprite = _sprite2;
        yield return new WaitForSeconds(patience / 3); // Wait for another 2 seconds
        bubbleRenderer.sprite = _sprite3;
        yield return new WaitForSeconds(patience / 3); // Wait before hiding the bubble
    }

    public void HideThoughtBubble()
    {
        thoughtBubble.SetActive(false);
    }
}


