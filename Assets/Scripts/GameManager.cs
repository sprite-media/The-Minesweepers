using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//It will communicate with server and affect the game flow(player turn)
public class GameManager : MonoBehaviour
{
	public static GameManager instance { get; private set; }
	//will be used for other players
	Dictionary<string, Player> players;
	Player player;//own player

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

	private void GivePermission(string id)
	{
		players[id].Turn();
	}
	public void NotifyTurnEnd()
	{
		//TODO Tell server turn end
	}
}
