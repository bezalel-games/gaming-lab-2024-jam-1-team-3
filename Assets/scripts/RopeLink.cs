using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RopeLink : MonoBehaviour
{
    public GameObject _connectedAbove;

    public GameObject _connectedBelow;

    // Start is called before the first frame update
    void Start()
    {
        ResetAnchor();
    }

    public void ResetAnchor()
    {
        _connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeLink aboveSegment = _connectedAbove.GetComponent<RopeLink>();
        if (aboveSegment != null)
        {
            aboveSegment._connectedBelow = gameObject;
            float spriteBottom = _connectedAbove.GetComponent<SpriteRenderer>().localBounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, -spriteBottom);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
    }

   
}
