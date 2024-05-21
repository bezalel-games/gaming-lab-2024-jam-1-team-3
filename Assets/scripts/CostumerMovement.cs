using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (!_inEvent)
        {
            var step = _speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(v1, _gotoPoint, step);
        }

        if (Vector2.Distance(v1, _gotoPoint) < 0.1 && !_pizzaRequested)
        {
            Debug.Log("In Pizza Point");
            _pizzaRequested = true;
            _inEvent = true;
            StartCoroutine(_costumerController.StartPizzaEvent());
        }
        if (Vector2.Distance(v1, _gotoPoint) < 0.1 && _isLeaving)
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
}
