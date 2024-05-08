using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using TMPro;


public class DeliveryPoint : MonoBehaviour
{
    private int _score = 0;
    private bool _isInside = false;
    private float _timeInside = 0;
    [SerializeField] private float _requiredTime = 3f;
    [SerializeField] private float _ropeGrow = 0.8f;
    public TextMeshProUGUI timeInsideText;

    public GameObject _rope;
    public GameObject _rope2;
    private Vector3 _ropeScale;
    void Start()
    {
        //transform.position = new Vector3(Random.Range(-7.3f, 7.5f), Random.Range(-4f, 2f), 0);
        //_ropeScale = _rope.transform.localScale;
        _rope2.GetComponent<Rope>().AddLink();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //ExtendRope();
            _rope2.GetComponent<Rope>().AddLink();

        }
    }

    private void ExtendRope()
    {
        Vector3 newScale = _rope.transform.localScale;
        newScale.Set(newScale.x + _ropeGrow, newScale.y, newScale.z);
        _ropeScale = newScale;
        Debug.Log("Rope scale is: " + _ropeScale.x + ", " + _ropeScale.y + ", " + _ropeScale.z );
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
                //ExtendRope();
                //Messenger.Default.Publish(new DeliveryEvent());
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
