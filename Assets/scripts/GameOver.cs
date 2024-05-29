using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject fadeInAnimator;
    void Start()
    {
        fadeInAnimator.SetActive(true); 
    }

}
