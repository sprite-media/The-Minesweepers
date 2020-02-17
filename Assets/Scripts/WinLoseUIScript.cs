using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseUIScript : MonoBehaviour
{
    public GameObject WinLoseUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GridManager.instance.isLose)
            WinLoseUI.SetActive(true);

    }
}
