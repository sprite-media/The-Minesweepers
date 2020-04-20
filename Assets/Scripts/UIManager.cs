using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	public static UIManager instance { get; private set; }
	public Sprite ThinkingImg;
	public Sprite SleepingImg;
	public GameObject[] PlayerObject;
	public GameObject[] TextObject;

	private Image[] Img;
	public Image[] PlayerBackground;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}

		Img = new Image[2];
		Img[0] = PlayerObject[0].GetComponent<Image>();
		Img[1] = PlayerObject[1].GetComponent<Image>();
	}

	public void ChangeStatus(bool status)
	{
		if (status)
		{

			Img[NetworkClient.instance.ID].sprite = ThinkingImg;
			TextObject[NetworkClient.instance.ID].GetComponent<TextMeshProUGUI>().text = "Thinking...";
			
			Img[(NetworkClient.instance.ID+1)%2].sprite = SleepingImg;
			TextObject[(NetworkClient.instance.ID+1)%2].GetComponent<TextMeshProUGUI>().text = "Waiting...";
		}
		else
		{
			Img[(NetworkClient.instance.ID + 1) % 2].sprite = ThinkingImg;
			TextObject[(NetworkClient.instance.ID + 1) % 2].GetComponent<TextMeshProUGUI>().text = "Thinking...";

			Img[NetworkClient.instance.ID].sprite = SleepingImg;
			TextObject[NetworkClient.instance.ID].GetComponent<TextMeshProUGUI>().text = "Waiting...";
		}
	}
	public void ChangeBackground(int id)
	{
		Debug.Log("Changing");
		PlayerBackground[id].color = Color.cyan;
	}
}