using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinLoseUIScript : MonoBehaviour
{
	public GameObject WinLoseUI;
	public GameObject GameOverText;
	public GameObject WinText;

	void Awake()
	{
		GameObject grid = GameObject.Find("GridManager");
		LogoutScript logoutScript = grid.GetComponent<LogoutScript>();
	}
	// Update is called once per frame
	void Update()
	{
		// set the WinLoseCamera to active when player clicked on the mine
		if (GridManager.instance.isLose)
		{
			GameManager.instance.player.Turn(false);
			GameOverText.SetActive(true);
			WinText.SetActive(false);
			GameOverText.transform.parent.gameObject.SetActive(true);
			WinLoseUI.SetActive(true);
		}
		if (GridManager.instance.isWin)
		{
			GameManager.instance.player.Turn(false);
			GameOverText.SetActive(false);
			WinText.SetActive(true);
			WinText.transform.parent.gameObject.SetActive(true);
			WinLoseUI.SetActive(true);
		}

	}

	public void PlayAgain()
	{
	}

	public void NotPlay()
	{
		LogoutScript.instance.Logout();
		Application.Quit();
	}

    
}
