using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineCounterScript : MonoBehaviour
{
    public Text mineCounterText;
    private int mineNum;
    void Start()
    {
        mineNum = 99;
        //get initial mine number once game difficult selected
    }

    void Update()
    {
        mineCounterText.text = mineNum.ToString();   
    }

    public void IncreaseMineNum()
    {
        mineNum += 1;
    }

    public void DecreaseMineNum()
    {
        mineNum -= 1;
    }
}
