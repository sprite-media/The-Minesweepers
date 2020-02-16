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
        ChangeTexture();
    }

    private void ChangeTexture()
    {
        GetComponent<Renderer>().material.mainTexture = GridManager.instance.cellTextures[(int)checkStatus];
    }
    public bool Clicked()
    {
        if (checkStatus == Status.HIDDEN)
        {
            Debug.Log("Clicked" + index);
            checkStatus = Status.CLICKED;
            ChangeTexture();
            //TODO show text
            if (surroundingArea == 0)
            {
                Debug.Log("Area");
                transform.GetChild(0).gameObject.SetActive(false);
                GridManager.instance.RevealAreaAt(index);
            }
            return true;
        }
        return false;
    }
    public bool Flagged()
    {
        if (checkStatus == Status.HIDDEN)
        {
            checkStatus = Status.FLAGGED;
            ChangeTexture();
            return true;
        }
        else if (checkStatus == Status.FLAGGED)
        {
            checkStatus = Status.HIDDEN;
            ChangeTexture();
            return true;
        }
        
        return false;
    }
    public void SetIndex(Vector2Int v)
    {
        index = v;
    }
}
