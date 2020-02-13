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
		if (!turn)
			return;

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//Ray hits something(there are cells only)
		if (Physics.Raycast(ray, out hit, 1000))
		{
			Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);
			SelectCell(0, hit);
			SelectCell(1, hit);
		}
		else
		{
			Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
		}
	}

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
					//Call function based on mouseButton
					FinishTurn();
				}
			}
			pressed = null;
		}
	}
	public void Turn()
	{
		turn = true;
	}
	private void FinishTurn()
	{
		turn = false;
		GameManager.instance.NotifyTurnEnd();
	}
}
