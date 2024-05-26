using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class particle : MonoBehaviour
{
    public Transform _target;
    private Vector3 _staticTarget;
    private Vector3 _movement;
    public TrailRenderer _trail;
    private float _speed;
    [SerializeField]private float _acc = 10;
    private float _maxSpeed = 15f;
    [SerializeField] private float _turnSpeed;
    public SpriteRenderer _coin;
    private bool _destroyed;
    private void Start()
    {
        _movement = new Vector3(Random.Range(-1,1f), Random.Range(0,1f), 0);
        _movement = _movement.normalized * Random.Range(5f,6.5f);
        _staticTarget = new Vector3(-8.6f, 4.45f, 0);
    }

    void Update()
    {
        if (_staticTarget != Vector3.zero && !_destroyed) 
        {
            _speed += _acc * Time.deltaTime;
            _speed = Mathf.Min(_speed, _maxSpeed);
            _turnSpeed += 3 * Time.deltaTime;
            
            Vector3 delta = _staticTarget - transform.position;
            Vector3 dir = delta.normalized;
            //Debug.Log($"{_target.position} - {transform.position} = {delta}");
            _movement = Vector3.Lerp(_movement, dir * _speed, _turnSpeed * Time.deltaTime);
            if (delta.magnitude < 0.5f)
            {
                _target = null;
                _movement = delta * 5f;
                _coin.transform.DOScale(Vector3.zero, 0.2f);
                // _trail.DOTime(0f, 0.2f).onComplete =
                //     delegate()
                //     {
                //         _destroyed = true;
                //         Destroy(gameObject);
                //     };
                _destroyed = true;
                StartCoroutine(DestroyParticle());
            }
        }

        if (!_destroyed)
        {
            transform.position += _movement * Time.deltaTime;
        }
    }

    IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
