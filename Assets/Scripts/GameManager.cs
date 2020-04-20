using UnityEngine;

//It will communicate with server and affect the game flow(player turn)
public class GameManager : MonoBehaviour
{
	public static GameManager instance { get; private set; }
	//will be used for other players
	public Player player { get; private set; }//own player

	public UIManager ui;

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
		NetworkClient.instance.SendInput(index, 0);
		Turn(false);
	}
	public void FlagRequest(Vector2Int index)
	{
		NetworkClient.instance.SendInput(index, 1);
		Turn(false);
	}
	public void Turn(bool turn)// Will be called at Client OnData Result case / depend on result(valid click or not)
	{
		player.Turn(turn);
		ui.ChangeStatus(turn);
	}
}
