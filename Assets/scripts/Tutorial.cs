using System;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private TextMeshProUGUI _tutorialText;
    private const string HTML_ALPHA = "<color=#00000000>";
    [SerializeField] private GameObject _moon;
    private int _phase;
    private bool _gotTip;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private GameObject _meteors;

    private void Awake()
    {
        _tutorialText = transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0)
            .GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        _tutorialText.text = "Welcome! Use the Arrow keys to move. Use Space to break.";
        StartCoroutine(RollText(3, _tutorialText.text));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TransitionToNextLevel();
        }
        if (!_gotTip && _phase == 2 && GameManager.Instance.getTip() > 0)
        {
            _gotTip = true;
            StartCoroutine(GotTip());
        }
    }

    private IEnumerator RollText(float animationTime, string p)
    {
        _phase++;

        //Taken from this video: https://www.youtube.com/watch?v=jTPOCglHejE&ab_channel=SasquatchBStudios 
        Audio.AudioController.PlayCommand(Audio.AudioController._astroBossTalking);
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

        if (_phase == 1)
        {
            StartCoroutine(SpawnMoon());
        }
        else if (_phase == 2)
        {
            //checking for tip in update
            _moon.SetActive(true);
        }
        else if (_phase == 3)
        {
            StartCoroutine(StartAsteroidsPhase());
        }
    }

    private IEnumerator SpawnMoon()
    {
        yield return new WaitForSeconds(4.5f);
        _tutorialText.text = "";
        string p = "Your purpose is to deliver pizzas to aliens. Let's try it out.";
        StartCoroutine(RollText(4, p));
    }

    private IEnumerator GotTip()
    {
        yield return new WaitForSeconds(2);
        _moon.SetActive(false);
        _tutorialText.text = "";
        string p = "You got a tip, Nice! Reach the tip goal in every level to advance.";
        StartCoroutine(RollText(4, p));
        yield return new WaitForSeconds(0.5f); //make arrow apear
        _arrow.SetActive(true);
        yield return new WaitForSeconds(3);
        _arrow.SetActive(false);
    }

    private IEnumerator StartAsteroidsPhase()
    {
        yield return new WaitForSeconds(2);
        _tutorialText.text = "";
        string s = "Oh, I forgot to tell you, there are ASTROIDS!";
        StartCoroutine(RollText(4, s));
        yield return new WaitForSeconds(2);
        _meteors.SetActive(true);
        yield return new WaitForSeconds(6);
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        _tutorialText.text = "";
        string s = "GOOD LUCK!";
        StartCoroutine(RollText(1, s));
        yield return new WaitForSeconds(3);
        TransitionToNextLevel();
    }
    
    public void TransitionToNextLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("LastScene", currentScene.name); 
        PlayerPrefs.Save(); 
        
        SceneManager.LoadScene("Transition");
    } 


}
