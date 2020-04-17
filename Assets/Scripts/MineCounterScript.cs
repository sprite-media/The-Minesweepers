using UnityEngine;
using TMPro;

public class MineCounterScript : MonoBehaviour
{
	//TODO make grid manger to have reference of this
	//Not singleton
	public static MineCounterScript instance { get; private set; }
	public TextMeshProUGUI mineCounterText;
	private int mineNum;
	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this);
	}
	private void Start()
	{
		//get initial mine number once game difficult selected
		mineNum = GridManager.instance.numOfMines;
	}

	void Update()
	{
		mineCounterText.text = mineNum.ToString();
	}

	public void IncreaseMineNum()
	{
		mineNum += 1;
	}

	public void DecreaseMineNum()
	{
		mineNum -= 1;
	}
}
