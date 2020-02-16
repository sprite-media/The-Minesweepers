using UnityEngine;
using TMPro;

public class MineCounterScript : MonoBehaviour
{
    public TextMeshProUGUI mineCounterText;
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
