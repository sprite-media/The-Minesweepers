using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class LogoutScript : MonoBehaviour
{

    public static LogoutScript instance { get; private set; }

    string userid;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setUserId(string id) { userid = id; }
    public void Logout()
    {
        StartCoroutine(SentLogoutInfo());
    }

    IEnumerator SentLogoutInfo()
    {
        string jsonString = "{\"username\":\"" + userid + "\"}";

        byte[] myData = System.Text.Encoding.UTF8.GetBytes(jsonString);

        UnityWebRequest www = UnityWebRequest.Put("https://5shq8xfk2f.execute-api.us-east-1.amazonaws.com/default/MinesweeperUserLogout", myData);

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
