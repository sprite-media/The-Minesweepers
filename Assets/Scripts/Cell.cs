using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    //Number of Status enum contents and number of textures should match
    //Name should match too
    public enum Status
    {
        HIDDEN = 0,
        CLICKED,
        FLAGGED,
        MINE,
        COONT
    }

    public bool isMine;
    public int surroundingArea = -1;
    public Status checkStatus { get; private set; }
    public Vector2Int index { get; private set; }
    private TextMeshPro text;

    private void Awake()
    {
        checkStatus = Status.HIDDEN;
        text = transform.GetChild(0).GetComponent<TextMeshPro>();
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

            text.gameObject.SetActive(true);
            if (surroundingArea == 0)
            {
                text.gameObject.SetActive(false);
                if (isMine)
                {
                    //Game over
                    Debug.Log("Game Over");
                    checkStatus = Status.MINE;
                }
                else
                {
                    Debug.Log("Area");
                    GridManager.instance.RevealAreaAt(index);
                }
            }
            ChangeTexture();
            return true;
        }
        return false;
    }
    public bool Flagged()
    {
        if (checkStatus == Status.HIDDEN)
        {
            checkStatus = Status.FLAGGED;
            goto Clickable;
        }
        else if (checkStatus == Status.FLAGGED)
        {
            checkStatus = Status.HIDDEN;
            goto Clickable;
        }
        return false;
        
        Clickable:
            ChangeTexture();
            return true;

    }
    public void SetIndex(Vector2Int v)
    {
        index = v;
    }

    //Text is set when the number surrounding mines is calculated which only happens during initialization.
    //Therefore it hides the text right after setting the text
    public void SetText(string msg)
    {
        text.text = msg;
        text.gameObject.SetActive(false);
    }
}
