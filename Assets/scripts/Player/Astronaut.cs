using System.Collections;
using UnityEngine;


public class Astronaut : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool isStunned = false;
    private float stunDuration = 2f; // Duration of stun in seconds
    private float stunTimer = 0f;
    [SerializeField] private GameObject movement;
    private Movement _movement;
    private SpriteRenderer astronautSpriteRenderer;
    public SpriteRenderer spaceshipSpriteRenderer; 
    private CameraShake cameraShake;

     private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movement = movement.GetComponent<Movement>();
        astronautSpriteRenderer = GetComponent<SpriteRenderer>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }


    private void Update()
    {
        if (isStunned)
                {
                    stunTimer -= Time.deltaTime;
                    if (stunTimer <= 0)
                    {
                        isStunned = false;
                        astronautSpriteRenderer.color = Color.white; // Reset color to white
                        spaceshipSpriteRenderer.color = Color.white; // Reset the color
                    }
                    return;
                }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Meteor") && !Movement.isStunned)
        {
            Stun();
        }
            
    }

    public void Stun()
    {
        _movement.StunMovement();
        isStunned = true;
        stunTimer = stunDuration;
        cameraShake.TriggerShake();
       StartCoroutine(Blink()); // Start blinking coroutine for both astronaut and spaceship
    }

    private IEnumerator Blink()
    {
        Color originalColor = astronautSpriteRenderer.color;
        Color stunColor = Color.red;

        while (stunTimer > 0)
        {
            astronautSpriteRenderer.color = astronautSpriteRenderer.color == originalColor ? stunColor : originalColor;
            spaceshipSpriteRenderer.color = spaceshipSpriteRenderer.color == originalColor ? stunColor : originalColor;
            yield return new WaitForSeconds(0.25f); // the interval for blinking speed
        }

        astronautSpriteRenderer.color = originalColor; 
        spaceshipSpriteRenderer.color = originalColor; 
    }

}

