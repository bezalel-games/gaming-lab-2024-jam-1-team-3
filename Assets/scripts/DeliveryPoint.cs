using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryPoint : MonoBehaviour
{
    private int _score = 0;
    private bool _isInside = false;
    private float _timeInside = 0;
    private float _requiredTime = 3f;
    void Start()
    {
        transform.position = new Vector3(Random.Range(-7.3f, 7.5f), Random.Range(-4f, 2f), 0);
    }

    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Pizza"))
        {    
            _isInside = true;
            _timeInside += Time.deltaTime;
            if (_timeInside >= _requiredTime && _isInside)
            {
                Debug.Log(++_score);
                StartCoroutine(Reposition());
                _isInside = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isInside = false;
        _timeInside = 0;
    }

    IEnumerator Reposition()
    {
        transform.position = new Vector3(12, 0, 0);
        yield return new WaitForSeconds(Random.Range(1.5f, 2.5f));
        transform.position = new Vector3(Random.Range(-7.3f, 7.5f), Random.Range(-4f, 2f), 0);
    }
}
