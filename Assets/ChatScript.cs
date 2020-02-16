using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatScript : MonoBehaviour
{
    public string inputText;
    public GameObject inputField;
    public GameObject display;

    void Start()
    {
        //Define which player own this client here 

        display.GetComponent<Text>().text = "";
    }

    public void StoreText()
    {
        inputText = inputField.GetComponent<Text>().text;
        display.GetComponent<Text>().text = inputText;
    }
}
