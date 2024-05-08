using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    private Rope _rope;
    private int _numLinks;
    public int _maxLinks = 10;
    
    void Awake()
    {
        _rope = transform.parent.GetComponent<Rope>();
        //_numLinks = _rope._numLimks;
    }

    
}
