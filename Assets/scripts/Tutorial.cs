using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private TextMeshProUGUI _tutorialText;
    private const string HTML_ALPHA = "<color=#00000000>";
    [SerializeField] private GameObject _moon;
    private int _phase;

    private void Awake()
    {
        _tutorialText = transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        _tutorialText.text = "Welcome! Use the WASD keys or the Arrow keys to move. Use Space to break.";
        StartCoroutine(RollText(3, _tutorialText.text));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RollText(float animationTime, string p)
    {
        //Taken from this video: https://www.youtube.com/watch?v=jTPOCglHejE&ab_channel=SasquatchBStudios 
        _tutorialText.text = "";
        string originalText = p;
        string displayText;
        int alphaIndex = 0;
        float typeFraction = animationTime / p.Length;
        foreach (char unused in p.ToCharArray())
        {
            alphaIndex++;
            _tutorialText.text = originalText;
            displayText = _tutorialText.text.Insert(alphaIndex, HTML_ALPHA);
            _tutorialText.text = displayText;

            yield return new WaitForSeconds(typeFraction);
        }

        yield return new WaitForSeconds(3);
        _phase++;
        NextPhase();
    }

    private void NextPhase()
    {
        if (_phase == 1)
        {
            _tutorialText.text = "";
            string p = "Very good! Your objective is to deliver pizzas to aliens. Lets try it out.";
            StartCoroutine(RollText(4, p));
            _moon.SetActive(true);
            StartCoroutine(CheckTip());
        }

        if (_phase == 2)
        {
            _moon.SetActive(false);
            _tutorialText.text = "";
            string p = "You got a tip, Nice! Reach the tip goal in every level to advance.";
            StartCoroutine(RollText(4, p));
            //Arrows pinting at UI
        }

        if (_phase == 3)
        {
            //stop arrows
            //WATCH OUT! meteors!!!!!
            // meteors phase
        }

        if (_phase == 4)
        {
            //thats it good luck
            //goes to level 1
        }

    }

    private IEnumerator CheckTip()
    {
        while (GameManager.Instance.getTip() == 0)
        {
            continue;
        }
        NextPhase();
    }
    

}
