using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Rope : MonoBehaviour
{

    public Rigidbody2D _hook;
    public GameObject _segment;
    private Rigidbody2D _lastSegment;
    public GameObject _astronaut;
    public int _initalNumLinks = 2;
    public int _maxLinks = 12;
    private int _currLinks = 0;

   // public GameObject _TopGameObject;
    private HingeJoint2D _top;

    private void Start()
    {
        //_top = _TopGameObject.GetComponent<HingeJoint2D>();
        CreateRope();
    }

    void CreateRope()
    {
        Rigidbody2D prevBod = _hook;
        for (int i = 0; i < _initalNumLinks; i++)
        {
            _currLinks++;
            GameObject newSeg = Instantiate(_segment);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            prevBod = newSeg.GetComponent<Rigidbody2D>();
            if (i == 0)
            {
                _top = hj;
            }
            else if (i == _initalNumLinks - 1)
            {
                _lastSegment = newSeg.GetComponent<Rigidbody2D>();
                ConnectAstro();
            }
        }
    }

    private void ConnectAstro()
    {
        _astronaut.GetComponent<HingeJoint2D>().connectedBody = _lastSegment;
    }

    public void AddLink()
    {
        if(_maxLinks == _currLinks){return;}

        _currLinks++;
        GameObject newLink = Instantiate(_segment);
        newLink.transform.parent = transform;
        newLink.transform.position = transform.position;
        HingeJoint2D hj = newLink.GetComponent<HingeJoint2D>();
        hj.connectedBody = _hook;
        newLink.GetComponent<RopeLink>()._connectedBelow = _top.gameObject;
        _top.connectedBody = newLink.GetComponent<Rigidbody2D>();
        _top.GetComponent<RopeLink>().ResetAnchor();
        _top = hj;
    }
}
