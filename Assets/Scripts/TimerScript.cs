using UnityEngine;
using UnityEngine.UI;
public class TimerScript : MonoBehaviour
{
    public Text TimerText;

    void Start()
    {
        //Get Time Reference
    }
    void Update()
    {
        TimerText.text = "00:00";
        //TimerText.text = Time.ToString();
    }
}
