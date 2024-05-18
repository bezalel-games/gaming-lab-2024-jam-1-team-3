using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerMovement : MonoBehaviour
{
    [SerializeField] private GameObject _deliveryPoint;
    [SerializeField] private float _speed;
    private bool _isGoingToPoint = true;
    private bool _notInEvent = true;
    private bool _pizzaRequested;

    private void Start()
    {
        goToPoint();
    }

    private void Update()
    {
        Vector2 v1 = transform.position;
        Vector2 v2 = _deliveryPoint.transform.position;
        if (_notInEvent)
        {
            var step = _speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(v1, v2, step);
        }

        if (Vector2.Distance(v1, v2) < 0.01f && !_pizzaRequested)
        {
            _pizzaRequested = true;
            _notInEvent = false;
            //start pizza event
            //when pizza event ends tp new point set both params to false and say that next point isnt a delivery point
        }
    }

    private void goToPoint()
    {
        var dir = _deliveryPoint.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, -dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
