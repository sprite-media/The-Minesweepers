using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    enum Status
    {
        FLAGGED,
        CLICKED,
        HIDDEN
    }

    public bool isMine;
    int surroundingArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
