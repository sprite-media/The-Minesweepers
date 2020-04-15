﻿using UnityEngine;

//It will communicate with server and affect the game flow(player turn)
public class GameManager : MonoBehaviour
{
	public static GameManager instance { get; private set; }
	//will be used for other players
	public Player player { get; private set; }//own player

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this);
	}

	public void CreatePlayer()
	{
		player = new GameObject().AddComponent<Player>();
	}

	//Receives cell index from the player (and communicate with server)-> future extension
	//Will pass the cell index to the server and will receive result from the server then it can change the player turn after
	public void ClickRequest(Vector2Int index)
	{
		Turn(false);
		//TODO tell server click on grid[index]
		//TODO at server side GridManager.instance.ClickAt(index);
		//TODO at server side GridManager.instance.CheckGrid();
	}
	public void FlagRequest(Vector2Int index)
	{
		Turn(false);
		//TODO tell server flag on grid[index]
		//TODO at server side GridManager.instance.FlagAt(index);
		//TODO at server side GridManager.instance.CheckGrid();
	}
	public void Turn(bool turn)// Will be called at Client OnData Result case / depend on result(valid click or not)
	{
		player.Turn(turn);
	}
}
