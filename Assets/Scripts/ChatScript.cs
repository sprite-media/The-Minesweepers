using UnityEngine;
using TMPro;

public class ChatScript : MonoBehaviour
{
	public static ChatScript instance;
	public GameObject inputField;
	public Transform content;
	public GameObject chatObject;
	private string inputText;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this);
	}

	public void StoreText()
	{
		inputText = inputField.GetComponent<TextMeshProUGUI>().text;
		//display.GetComponent<TextMeshProUGUI>().text = inputText;
		NetworkClient.instance.SendChatting(inputText);
		inputText = "";
	}
	public void AddText(string chatMessage)
	{
		TextMeshProUGUI chat = Instantiate(chatObject, content).GetComponent<TextMeshProUGUI>();
		chat.text = chatMessage;
	}
}
