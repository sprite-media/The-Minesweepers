using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite ThinkingImg;
    public Sprite SleepingImg;
    public GameObject Player1;
    public GameObject Player2;
    public Text P1Text;
    public Text P2Text;

    private Image P1Img;
    private Image P2Img;

    private void Awake()
    {
        P1Img = Player1.GetComponent<Image>();
        P2Img = Player2.GetComponent<Image>();
    }

    public void ChangeStatus(bool status)
    {
        if (status)
        {
            P1Img.sprite = ThinkingImg;
            P1Text.text = "Thinking...";
            P2Img.sprite = SleepingImg;
            P2Text.text = "Waiting...";
        }
        else
        {
            P1Img.sprite = SleepingImg;
            P1Text.text = "Waiting...";
            P2Img.sprite = ThinkingImg;
            P2Text.text = "Thinking...";
        }
    }
}
