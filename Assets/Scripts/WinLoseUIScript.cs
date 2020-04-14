using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseUIScript : MonoBehaviour
{
	public GameObject WinLoseUI;
	public GameObject GameOverText;
	public GameObject WinText;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// set the WinLoseCamera to active when player clicked on the mine
		if (GridManager.instance.isLose)
		{
			GameManager.instance.player.Turn(false);
			Time.timeScale = 0.0f;
			GameOverText.SetActive(true);
			WinText.SetActive(false);
			WinLoseUI.SetActive(true);
		}
		if (GridManager.instance.isWin)
		{
			GameManager.instance.player.Turn(false);
			Time.timeScale = 0.0f;
			GameOverText.SetActive(false);
			WinText.SetActive(true);
			WinLoseUI.SetActive(true);
		}

	}

	public void PlayAgain()
	{
	}

	public void NotPlay()
	{
		Application.Quit();
	}
}
