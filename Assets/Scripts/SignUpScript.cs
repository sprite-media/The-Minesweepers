using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class SignUpScript : MonoBehaviour
{
    public GameObject welcomeText;
    public GameObject checkPwInputField;
    public GameObject idInputField;
    public GameObject pwInputField;
    public GameObject loginUI;
    public GameObject signupUI;
    public GameObject MsgUI;

    string idInputText;
    string pwInputText;
    string checkPwInputText;
    string successText;
    bool signup = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SignUp()
    {
        loginUI.SetActive(false);
        signupUI.SetActive(true);
    }

    public void NewUser() 
    {
        Debug.Log("Signupfor user");
        StartCoroutine(SentUserInfo());
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
        Debug.Log("123");

        if (checkPwInputText == pwInputText)
        {
            Debug.Log("456");
            string jsonString = "{\"username\":\"" + idInputText + "\",\"password\":\"" + pwInputText + "\"}";

            byte[] myData = System.Text.Encoding.UTF8.GetBytes(jsonString);

            UnityWebRequest www = UnityWebRequest.Put("https://f3alb29tch.execute-api.us-east-1.amazonaws.com/default/MinesweeperUserSignUp", myData);

            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                successText = www.downloadHandler.text;
                Debug.Log(successText);
                if (successText == "\"success\"")
                    Debug.Log("Signup");
                DisplayWelcomeUI();
            }
        }
    }

    void DisplayWelcomeUI()
    {
        welcomeText.GetComponent<TextMeshProUGUI>().text = "Welcome " + idInputText + " to Minesweeper! \r\n Please login to play!";
        loginUI.SetActive(false);
        signupUI.SetActive(false);
        MsgUI.SetActive(true);
    }

    public void DisplayLoginUI() 
    {
        loginUI.SetActive(true);
        signupUI.SetActive(false);
        MsgUI.SetActive(false);

    }
}
