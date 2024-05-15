using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour
{
    public GameObject thoughtBubble; 
    public SpriteRenderer bubbleRenderer;
    [SerializeField] private Sprite _sprite1;
    [SerializeField] private Sprite _sprite2;
    [SerializeField] private Sprite _sprite3;

    private void Start()
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
        HideThoughtBubble();
    }

    public void RequestPizza()
    {
        thoughtBubble.SetActive(true);
        StartCoroutine(ChangeBubbleColorOverTime());
    }

    private IEnumerator ChangeBubbleColorOverTime()
    {
        // Change color over time
        // Debug.Log("Thought bubble active state after hiding: " + thoughtBubble.activeSelf);  // This should log 'false'.
        //bubbleRenderer.color = Color.green; // Start with green
        bubbleRenderer.sprite = _sprite1;
        yield return new WaitForSeconds(3); // Wait for 2 seconds
        bubbleRenderer.sprite = _sprite2;
        //bubbleRenderer.color = new Color(1, 0.64f, 0, 1); // Change to orange
        yield return new WaitForSeconds(3); // Wait for another 2 seconds
        bubbleRenderer.sprite = _sprite3;
        //bubbleRenderer.color = Color.red; // Finally change to red
        yield return new WaitForSeconds(3); // Wait before hiding the bubble
        HideThoughtBubble();
    }

    private void HideThoughtBubble()
    {
        thoughtBubble.SetActive(false);
    }
}


