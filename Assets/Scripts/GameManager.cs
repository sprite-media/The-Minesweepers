using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//It will communicate with server and affect the game flow(player turn)
public class GameManager : MonoBehaviour
{
	public static GameManager instance { get; private set; }
	//will be used for other players
	private Dictionary<string, Player> players;
	public Player player { get; private set; }//own player

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this);

		player = new GameObject().AddComponent<Player>();
		players = new Dictionary<string, Player>();
		players.Add("testID", player);
		//TODO Add network players
	}

	
	private void Start()
	{
		GivePermission("testID");
	}

	private void GivePermission(string id)
	{
		players[id].Turn(true);
	}

//Receives cell index from the player (and communicate with server)-> future extension
//Will pass the cell index to the server and will receive result from the server then it can change the player turn after
	public void ClickRequest(Vector2Int index)
	{
		//TODO tell server click on grid[index]
		//player.Turn(!GridManager.instance.ClickAt(index));
		GridManager.instance.ClickAt(index);
		player.Turn(true);
		GridManager.instance.CheckGrid();
	}
	public void FlagRequest(Vector2Int index)
	{
		//TODO tell server flag on grid[index]
		//player.Turn(!GridManager.instance.FlagAt(index));
		GridManager.instance.FlagAt(index);
		player.Turn(true);
		GridManager.instance.CheckGrid();
	}
	public void NotifyTurnEnd()
	{
		//TODO Tell server turn end
	}
}
