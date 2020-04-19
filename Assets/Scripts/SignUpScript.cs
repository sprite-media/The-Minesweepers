﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class SignUpScript : MonoBehaviour
{
    public GameObject loginBtn;
    public GameObject loginText;
    public GameObject checkPwInput;
    public GameObject checkPwInputField;
    public GameObject idInputField;
    public GameObject pwInputField;

    string idInputText;
    string pwInputText;
    string checkPwInputText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SignUp()
    {
        if (loginBtn.activeSelf)
        {
            loginBtn.SetActive(false);
            loginText.GetComponent<TextMeshProUGUI>().text = "Sign Up";
            checkPwInput.SetActive(true);
        }
    }

    public void NewUser() 
    {
        if (checkPwInput.activeSelf)
        {
            StartCoroutine(SentUserInfo());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SentUserInfo()
    {
        idInputText = idInputField.GetComponent<TextMeshProUGUI>().text;
        pwInputText = pwInputField.GetComponent<TextMeshProUGUI>().text;
        checkPwInputText = checkPwInputField.GetComponent<TextMeshProUGUI>().text;

        if (checkPwInputText == pwInputText)
        {
            string jsonString = "{\"username\":\"" + idInputText + "\",\"password\":\"" + pwInputText + "\"}";

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
            }


        }
    }

}
