using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoScript : MonoBehaviour
{
    enum Playerstatus
    {
        Thinking,
        Waiting
    };

    struct Player
    {
        public string name;
        public Playerstatus status;
    }

    Player p1, p2 = new Player();

    //UI Elements
    public Sprite thinkingImg, waitingImg;
    public Image BG1, BG2, Img1, Img2;
    public TextMeshProUGUI name1, name2, statusText1, statusText2;

    Color thinkingColor, waitingColor;

    void Start()
    {
        //get info from game
        //temp info for now
        p1.name = "BK";
        p2.name = "KT";
        p1.status = Playerstatus.Waiting;
        p2.status = Playerstatus.Thinking;

        //setup UI
        thinkingColor = new Color(245, 205, 121, 255);
        waitingColor = new Color(127, 143, 166, 255);

        name1.text = p1.name;
        name2.text = p2.name;
        UpdateUI();
        
    }


    void Update()
    {
        
    }
    
    public void Swapstatus()
    {
        if(p1.status == Playerstatus.Thinking && p2.status == Playerstatus.Waiting)
        {
            p1.status = Playerstatus.Waiting;
            p2.status = Playerstatus.Thinking;
        }
        
        else if(p1.status == Playerstatus.Waiting && p2.status == Playerstatus.Thinking)
        {
            p1.status = Playerstatus.Thinking;
            p2.status = Playerstatus.Waiting;
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        if (p1.status == Playerstatus.Thinking)
        {
            BG1.color = thinkingColor;
            statusText1.text = "Thinking...";
            Img1.sprite = thinkingImg;
        }
        else
        {
            BG1.color = waitingColor;
            statusText1.text = "Waiting...";
            Img1.sprite = waitingImg;
        }

        if (p2.status == Playerstatus.Thinking)
        {
            BG2.color = thinkingColor;
            statusText2.text = "Thinking...";
            Img2.sprite = thinkingImg;
        }
        else
        {
            BG2.color = waitingColor;
            statusText2.text = "Waiting...";
            Img2.sprite = waitingImg;
        }
    }
}
