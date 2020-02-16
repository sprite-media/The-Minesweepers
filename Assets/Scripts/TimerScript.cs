using UnityEngine;
using UnityEngine.UI;
public class TimerScript : MonoBehaviour
{
    public Text timerText;

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
