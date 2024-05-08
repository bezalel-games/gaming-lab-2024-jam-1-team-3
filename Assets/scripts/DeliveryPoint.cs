using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class DeliveryPoint : MonoBehaviour
{
    private int _score = 0;
    private bool _isInside = false;
    private float _timeInside = 0;
    [SerializeField] private float _requiredTime = 3f;
    public TextMeshProUGUI timeInsideText;

    public GameObject _rope;
    private Vector3 _ropeScale;
    void Start()
    {
        transform.position = new Vector3(Random.Range(-7.3f, 7.5f), Random.Range(-4f, 2f), 0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _rope.GetComponent<Rope>().AddLink();
        }
    }

   

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Pizza"))
        {    
            _isInside = true;
            _timeInside += Time.deltaTime;
            timeInsideText.text = _timeInside.ToString("F2");;
            if (_timeInside >= _requiredTime && _isInside)
            {
                //Messenger.Default.Publish(new DeliveryEvent());
                Rope rope = _rope.GetComponent<Rope>();
                rope.AddLink();
                rope.AddLink();
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
        timeInsideText.text = ""; 
    }

    IEnumerator Reposition()
    {
        transform.position = new Vector3(12, 0, 0);
        yield return new WaitForSeconds(Random.Range(1.5f, 2.5f));
        transform.position = new Vector3(Random.Range(-7.3f, 7.5f), Random.Range(-4f, 2f), 0);
    }
}
