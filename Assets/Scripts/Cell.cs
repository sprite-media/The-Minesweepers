using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum Status
    {
        HIDDEN = 0,
        CLICKED,
        FLAGGED,
        COONT
    }

    public bool isMine;
    public int surroundingArea = -1;
    public Status checkStatus { get; private set; }
    public Vector2Int index { get; private set; }

    private void Awake()
    {
        checkStatus = Status.HIDDEN;
    }

    public bool Clicked()
    {
        if (checkStatus == Status.HIDDEN)
        {
            checkStatus = Status.CLICKED;
            Debug.Log("Clicked");
            Reveal();
            return true;
        }
        return false;
    }
    public bool Flagged()
    {
        if (checkStatus == Status.HIDDEN)
        {
            checkStatus = Status.FLAGGED;
            //TODO change texture to make it flagged
            return true;
        }
        return false;
    }
    public void Reveal()
    {
        Debug.Log("Reveal");
        //TODO change texture to make it looks like it's revealed
        //TODO show text
        if (surroundingArea == 0)
        {
            Debug.Log("Area");
            GridManager.instance.RevealAreaAt(index);
        }
    }
    public void SetIndex(Vector2Int v)
    {
        index = v;
    }
}
