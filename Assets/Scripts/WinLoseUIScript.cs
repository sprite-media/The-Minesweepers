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
            //Time.timeScale = 0f;
            GameOverText.SetActive(true);
            WinText.SetActive(false);
            WinLoseUI.SetActive(true);
        }
        else if(GridManager.instance.isWin)
        {
            //Time.timeScale = 0f;
            GameOverText.SetActive(true);
            WinText.SetActive(true);
            WinLoseUI.SetActive(false);
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
