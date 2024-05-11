using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using UnityEngine;

public class Meteors : MonoBehaviour
{
    public enum MovementDirection { TopToBottom, BottomToTop, LeftToRight, RightToLeft }
    public MovementDirection movementDirection;

    private Vector2 startPoint;
    private Vector2 endPoint;
    public float speed = 5.0f;

    private float minDelay = 3;
    private float maxDelay = 8;


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
        transform.right = endPoint - startPoint;
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