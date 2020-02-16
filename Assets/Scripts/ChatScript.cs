using UnityEngine;
using TMPro;

public class ChatScript : MonoBehaviour
{
    public string inputText;
    public GameObject inputField;
    public GameObject display;

    void Start()
    {
        //Define which player own this client here 

        display.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void StoreText()
    {
        inputText = inputField.GetComponent<TextMeshProUGUI>().text;
        display.GetComponent<TextMeshProUGUI>().text = inputText;
    }
}
