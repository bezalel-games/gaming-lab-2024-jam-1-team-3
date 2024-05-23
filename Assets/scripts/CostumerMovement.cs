using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Random = UnityEngine.Random;

public class CostumerMovement : MonoBehaviour
{
    [SerializeField] private GameObject _deliveryPoint;
    [SerializeField] private float _speed;
    private bool _inEvent;
    private bool _pizzaRequested;
    private List<Vector3> _returnPoints = new List<Vector3>();
    private Vector3 _gotoPoint;
    private Costumer _costumerController;
    private bool _isLeaving;


    private void Awake()
    {
        _costumerController = this.GetComponent<Costumer>();
    }

    private void Start()
    {
        _gotoPoint = _deliveryPoint.transform.position;
        goToPoint();
        _returnPoints.Add(new Vector3(0, 6,0));
        _returnPoints.Add(new Vector3(-12, 0,0));
        _returnPoints.Add(new Vector3(0, -6,0));
        _returnPoints.Add(new Vector3(-12, 0,0));
    }

    private void Update()
    {
        Vector2 v1 = transform.position;
        float distanceFromPoint = Vector2.Distance(v1, _gotoPoint);
        if (!_inEvent)
        {
            var step = _speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(v1, _gotoPoint, step);
        }

        if (distanceFromPoint < 0.01 && !_pizzaRequested)
        {
            if (PosOnScreen())
            {
                Debug.Log("In Pizza Point");
                _pizzaRequested = true;
                _inEvent = true;
                StartCoroutine(_costumerController.StartPizzaEvent());
            }
        }
        if (distanceFromPoint < 0.01 && _isLeaving)
        {
            Debug.Log("In Home Point");
            StartCoroutine(ResetMovementLoop());
        }
    }

    private IEnumerator ResetMovementLoop()
    {
        _isLeaving = false;
        _inEvent = true;
        _pizzaRequested = false;
        transform.position = _returnPoints[Random.Range(0,3)];
        //TODO change spawn point

        yield return new WaitForSeconds(Random.Range(5, 8));
        _gotoPoint = _deliveryPoint.transform.position;
        _inEvent = false;
    }

    private void goToPoint()
    {
        var dir = _gotoPoint - transform.position;
        var angle = Mathf.Atan2(dir.y, -dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void EndEvent()
    {
        _isLeaving = true;
        _gotoPoint = _returnPoints[Random.Range(0, 3)];
        goToPoint();
        _inEvent = false;
    }

    private bool PosOnScreen()
    {
        var position = transform.position;
        Vector2 v = new Vector2(position.x, position.y);
        if (Mathf.Abs(v.y) > 5.2 || Mathf.Abs(v.x) > 9.5)
        {
            //Debug.Log("false");
            return false;
        }
        return true;
    }
}
