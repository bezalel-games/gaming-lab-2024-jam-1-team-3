using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.down * 3;
        //_rb.SetRotation(Quaternion.RotateTowards());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -7)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f),7, 0);
            _rb.velocity = Vector2.zero;
            StartCoroutine(Rest());
        }
    }

    IEnumerator Rest()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        _rb.velocity = Vector2.down * Random.Range(1f, 2.3f);
    }
}
