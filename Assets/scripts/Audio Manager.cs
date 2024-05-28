
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio AudioController;
    private AudioSource _as;
    
    public AudioClip _pizzaDeliveryFailed;
    public AudioClip _pizzaDeliverySuccess;
    public AudioClip _alienRequestsPizza;
    public AudioClip _10SecondsLeft;
    public AudioClip _winSound;
    public AudioClip _looseSound;
    public AudioClip _meteorHit;
    public AudioClip _astroBossTalking;
    public AudioClip _engineSounds;
    public AudioClip _buttonPress;
    public AudioClip _transitionLevel;
    
    private void Awake()
    {
        _as = GetComponent<AudioSource>();
        if (AudioController == null)
        {
            AudioController = this;
        }
        else
        {
            Debug.LogError("Too many Audio Controllers");
        }
    }

    public void PlayCommand(AudioClip sound)
    {
        if(sound != null)
        _as.PlayOneShot(sound);
    }
}