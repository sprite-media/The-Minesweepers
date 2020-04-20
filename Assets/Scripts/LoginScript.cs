using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class LoginScript : MonoBehaviour
{
	public string idInput;
    public string pwInput;
    public bool login;
    string MsgText;
	public GameObject idInputField;
    public GameObject pwInputField;
    public GameObject OutputText;

	void Start()
	{
        //Define which player own this client here 
        login = false;
	}
    public void Login()
    {
        StartCoroutine(SentLoginInfo());
    }
	IEnumerator SentLoginInfo()
	{
        idInput = idInputField.GetComponent<TextMeshProUGUI>().text;
        pwInput = pwInputField.GetComponent<TextMeshProUGUI>().text;

        string jsonString = "{\"username\":\"" + idInput + "\",\"password\":\"" + pwInput + "\"}";   
        
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(jsonString);   

        UnityWebRequest www = UnityWebRequest.Put("https://a6op51ykj9.execute-api.us-east-1.amazonaws.com/default/MinesweeperUserLogin", myData);

        www.SetRequestHeader("Content-Type", "application/json");
        
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            MsgText = www.downloadHandler.text;
            if (MsgText == "\"Invalid password\"" || MsgText == "\"Invalid username\"") {
                OutputText.GetComponent<TextMeshProUGUI>().text = MsgText;
                OutputText.SetActive(true);
            }
        }



    }
}

