using UnityEngine;
using TMPro;
public class TimerScript : MonoBehaviour
{
	public static TimerScript instance { get; private set; }
	public TextMeshProUGUI timerText;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
	}
	public void UpdateTimer(float elapsed)
	{
		timerText.text = FloatToTimeString(elapsed);
	}
	public static string FloatToTimeString(float t)
	{
		int min = (int)(t / 60);
		int sec = (int)(t % 60);
		string time = min.ToString() + ":" + sec.ToString("D2");
		return time;
	}
}