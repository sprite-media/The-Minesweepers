using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineCounterScript : MonoBehaviour
{
    public Text MineCounterText;
    void Start()
    {
        
    }

    void Update()
    {
        MineCounterText.text = "0";   
    }
}
