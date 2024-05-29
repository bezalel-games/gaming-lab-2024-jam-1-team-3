using System.Collections;
using TMPro;
using UnityEngine;

public class StartAstronaut : MonoBehaviour
{
    private const string HTML_ALPHA = "<color=#00000000>";
    [SerializeField] private float _startTime;
    private TextMeshProUGUI _startText;
    private GameObject _astronautStart;
    public GameObject fadeInAnimator;
    public GameObject fadeOutAnimator;


    void Awake()
    {
        _astronautStart = transform.GetChild(0).gameObject;
        _startText = transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        fadeInAnimator.SetActive(true); 
        fadeOutAnimator.SetActive(false);
        _startTime = GameManager.Instance._startTime;
        _startText.text = "Your tip goal for today is: " + GameManager.Instance.GetTipGoal() + "$. Also, we made your rope longer. Good luck!";
        StartCoroutine(DelayStartAnimation());
    }

    private IEnumerator DelayStartAnimation()
    {
        // Wait for 2 seconds before starting the animation
        yield return new WaitForSeconds(1);

        // Now start the actual animation coroutine
        StartCoroutine(StartAnimation());
    }

    
    private IEnumerator StartAnimation() 
    {
        StartCoroutine(RollText(_startTime/2,_startText.text ));
        yield return new WaitForSeconds(_startTime);
        _astronautStart.SetActive(false);
       // yield return new WaitForSeconds(3);
        GameManager.Instance.SpawnLevelStartGame();
    }
    private IEnumerator RollText(float animationTime, string p)
    {
        //Taken from this video: https://www.youtube.com/watch?v=jTPOCglHejE&ab_channel=SasquatchBStudios 
        Audio.AudioController.PlayCommand(Audio.AudioController._astroBossTalking);
        _startText.text = "";
        string originalText = p;
        string displayText;
        int alphaIndex = 0;
        float typeFraction = animationTime / p.Length;
        foreach (char unused in p.ToCharArray())
        {
            alphaIndex++;
            _startText.text = originalText;
            displayText = _startText.text.Insert(alphaIndex, HTML_ALPHA);
            _startText.text = displayText;

            yield return new WaitForSeconds(typeFraction);
        }
    }
}
