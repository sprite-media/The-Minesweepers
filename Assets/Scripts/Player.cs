using UnityEngine;

public class Player : MonoBehaviour
{
	private bool turn;
	private string networkID;
	private Cell pressed;

	private void Awake()
	{
		turn = true;
	}

	private void Update()
	{
		//Player can affect the grid only when it's his turn
		if (!turn || this != GameManager.instance.player)
			return;

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//Ray hits something(there are cells only)
		if (Physics.Raycast(ray, out hit, 1000))
		{
			Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);
			SelectCell(0, hit);//left click
			SelectCell(1, hit);//right click
		}
		else
		{
			Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
		}
	}

	//Cursor should be on the same cell when mouse is clicked and released
	private void SelectCell(int mouseButton, RaycastHit hit)
	{
		if (Input.GetMouseButtonDown(mouseButton))
		{
			pressed = hit.collider.GetComponent<Cell>();
			//Debug.Log(pressed);
		}
		else if (Input.GetMouseButtonUp(mouseButton))
		{
			Cell hitCell = hit.collider.GetComponent<Cell>();
			//Debug.Log(hitCell);
			if (hitCell != null)
			{
				if (pressed == hitCell)
				{
					Debug.Log("Mouse");
					//Player doesn't interact with grid directly.
					//It passes the cell index to GameManager for future extension(Server)
					switch (mouseButton)
					{
						case 0:
							GameManager.instance.ClickRequest(hitCell.index);
							break;
						case 1:
							GameManager.instance.FlagRequest(hitCell.index);
							break;
						default:
							Debug.Log("Nothing happens");
							break;
					}
				}
			}
			pressed = null;
		}
	}
	public void Turn(bool t)
	{
        
	}
}
