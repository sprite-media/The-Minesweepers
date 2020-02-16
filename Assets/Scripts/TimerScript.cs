using UnityEngine;
using TMPro;
public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    void Start()
    {
        //Get Time Reference
    }
    void Update()
    {
        timerText.text = "00:00";
        //TimerText.text = Time.ToString();
    }
}