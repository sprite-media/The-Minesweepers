using UnityEngine;
using TMPro;
public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float time;

    void Start()
    {
        //Get Time Reference
        time = 0.0f;
    }
    void Update()
    {
        time += Time.deltaTime;
        timerText.text = FloatToTimeString(time);
        //TimerText.text = Time.ToString();
    }
    public static string FloatToTimeString(float t)
    {
        int min = (int)(t / 60);
        int sec = (int)(t % 60);
        string time = min.ToString() + ":" + sec.ToString("D2");
        return time;
    }
}