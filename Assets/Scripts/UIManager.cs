using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Sprite ThinkingImg;
    public Sprite SleepingImg;
    public GameObject Player1Object;
    public GameObject Player2Object;
    public GameObject P1TextObject;
    public GameObject P2TextObject;

    private Image P1Img;
    private Image P2Img;

    private void Awake()
    {
        P1Img = Player1Object.GetComponent<Image>();
        P2Img = Player1Object.GetComponent<Image>();
    }

    public void ChangeStatus(bool status)
    {
        if (status)
        {
            P1Img.sprite = ThinkingImg;
            P1TextObject.GetComponent<TextMeshProUGUI>().text = "Thinking...";
            P2Img.sprite = SleepingImg;
            P2TextObject.GetComponent<TextMeshProUGUI>().text = "Waiting...";
        }
        else
        {
            P1Img.sprite = SleepingImg;
            P1TextObject.GetComponent<TextMeshProUGUI>().text = "Waiting...";
            P2Img.sprite = ThinkingImg;
            P2TextObject.GetComponent<TextMeshProUGUI>().text = "Thinking...";
        }
    }
}
