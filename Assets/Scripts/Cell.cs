using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum Status
    {
        FLAGGED,
        CLICKED,
        HIDDEN
    }

    public bool isMine;
    public int surroundingArea = -1;
    public Status checkStatus { get; private set; }
    public Vector2 index { get; private set; }

    public void SetIndex(Vector2 v)
    {
        index = v;
    }

    
}
