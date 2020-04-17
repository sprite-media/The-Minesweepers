using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
	//Number of Status enum contents and number of textures should match
	//Name should match too
	public enum Status
	{
		HIDDEN = 0,
		CLICKED,
		FLAGGED,
		MINE,
		COUNT
	}

	public bool isMine;
	public int surroundingArea = -1;
	public Status checkStatus { get; private set; }
	public Vector2Int index { get; private set; }
	private TextMeshPro text;

	private void Awake()
	{
		checkStatus = Status.HIDDEN;
		text = transform.GetChild(0).GetComponent<TextMeshPro>();
		text.gameObject.SetActive(false);
		ChangeTexture();
	}
	private void ChangeTexture()
	{
		GetComponent<Renderer>().material.mainTexture = GridManager.instance.cellTextures[(int)checkStatus];
	}
	public bool Clicked(ref NetworkMessage.Result result) //Try to reveal the cell and return whether it was valid click or not
	{
		if (checkStatus == Status.HIDDEN)
		{
			checkStatus = Status.CLICKED;
			if (surroundingArea == 0)
			{
				if (isMine)
				{
					//Game over
					Debug.Log("Game Over");
					checkStatus = Status.MINE;
				}
				else
				{
					//Debug.Log("Area");
					GridManager.instance.RevealAreaAt(index, ref result);
				}
			}
			NetworkMessage.CellResult cr = new NetworkMessage.CellResult();
			cr.index = index;
			cr.status = checkStatus;
			cr.surrounding = surroundingArea;
			result.result.Add(cr);
			return true;
		}
		return false;
	}
	public bool Flagged(ref NetworkMessage.Result result) //Try to put/remove falg on the cell and return whether it was valid click or not
	{
		//TODO mine number should be managed my grid manager
		if (checkStatus == Status.HIDDEN)
		{
			checkStatus = Status.FLAGGED;
			goto Clickable;
		}
		else if (checkStatus == Status.FLAGGED)
		{
			checkStatus = Status.HIDDEN;
			goto Clickable;
		}
		return false;

		Clickable:
		NetworkMessage.CellResult cr = new NetworkMessage.CellResult();
		cr.index = index;
		cr.status = checkStatus;
		result.result.Add(cr);
		return true;

	}
	public void SetIndex(Vector2Int v)
	{
		index = v;
	}
	public void Reflect(NetworkMessage.CellResult cr)
	{
		checkStatus = cr.status;
		ChangeTexture();
		switch (checkStatus)
		{
			case Status.HIDDEN:
				MineCounterScript.instance.IncreaseMineNum();
				break;
			case Status.CLICKED:
			{
				SetText(cr.surrounding.ToString());
				text.gameObject.SetActive(true);
				if (cr.surrounding == 0)
					text.gameObject.SetActive(false);
			}
				break;
			case Status.FLAGGED:
				MineCounterScript.instance.DecreaseMineNum();
				break;
			case Status.MINE:
				GridManager.instance.isLose = true;
				return;
			default:
				break;
		}
	}

	//Text is set when the number surrounding mines is received from the server.
	public void SetText(string msg)
	{
		text.text = msg;
	}
}
