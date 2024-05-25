using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour
{
    private Vector3 originalScale;
    public float scaleFactor = 1.2f; // Scale factor to enlarge the button

    private void Start()
    {
        // Save the original scale of the button
        originalScale = transform.localScale;
    }

    public void OnPlayButtonClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tutorial");
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("noa");
        transform.localScale = originalScale * scaleFactor;
    }

   
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}
