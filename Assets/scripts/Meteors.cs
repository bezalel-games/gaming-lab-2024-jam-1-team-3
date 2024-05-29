using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Meteors : MonoBehaviour
{
    public enum MovementDirection { TopToBottom, BottomToTop, LeftToRight, RightToLeft }
    public MovementDirection movementDirection;

    private Vector2 startPoint;
    private Vector2 endPoint;
    public float speed = 5.0f;

    [SerializeField] private float minDelay = 4;
    [SerializeField] private float maxDelay = 12;
    private float minScale = 0.3f;
    private float maxScale = 0.8f;
    private float rotationSpeed;
    public float startXMin;
    public float startXMax;
    public float startYMin;
    public float startYMax;
    public float endXMin;
    public float endXMax;
    public float endYMin;
    public float endYMax;
    

  void Start()
    {
        StartCoroutine(InitializeMeteorWithDelay());
    }

    IEnumerator InitializeMeteorWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            SetMeteorPath();
            yield return MoveMeteor();
        }
    }

   
    void SetMeteorPath()
    {
        float randomScale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        // rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

        switch (movementDirection)
        {
            case MovementDirection.TopToBottom:
                startPoint = new Vector2(Random.Range(startXMin, startXMax), startYMax);
                endPoint = new Vector2(Random.Range(endXMin, endXMax), endYMin);
                break;
            case MovementDirection.BottomToTop:
                startPoint = new Vector2(Random.Range(startXMin, startXMax), startYMax);
                endPoint = new Vector2(Random.Range(endXMin, endXMax), endYMin);
                break;
            case MovementDirection.LeftToRight:
                startPoint = new Vector2(startXMin, Random.Range(startYMin, startYMax));
                endPoint = new Vector2(endXMax, Random.Range(endYMin, endYMax));
                break;
            case MovementDirection.RightToLeft:
                startPoint = new Vector2(startXMin, Random.Range(startYMin, startYMax));
                endPoint = new Vector2(endXMax, Random.Range(endYMin, endYMax));
                break;
        }
        transform.position = startPoint;
        // Adjust the direction the meteor is facing
        Vector2 direction = endPoint - startPoint;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    IEnumerator MoveMeteor()
    {
        while (Vector2.Distance(transform.position, endPoint) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
            yield return null;
        }
    }
}  