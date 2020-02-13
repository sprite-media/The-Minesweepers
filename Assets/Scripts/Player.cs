using UnityEngine;

public class Player : MonoBehaviour
{
	public bool turn { get; set; }
	private Cell pressed;

	private void Awake()
	{
		turn = true;
	}

	private void Update()
	{
		if (!turn)
			return;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, 1000))
		{
			Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);
			if (Input.GetMouseButtonDown(0))
			{
				pressed = hit.collider.GetComponent<Cell>();
			}
			else if (Input.GetMouseButtonUp(0))
			{
				Cell hitCell = hit.collider.GetComponent<Cell>();
				if (hitCell != null)
				{
					if (pressed == hitCell)
					{
						Debug.Log("Clicked");
						//Call click function of pressed
					}
				}
			}
			else if (Input.GetMouseButtonDown(1))
			{
				pressed = hit.collider.GetComponent<Cell>();
			}
			else if (Input.GetMouseButtonUp(1))
			{
				Cell hitCell = hit.collider.GetComponent<Cell>();
				if (hitCell != null)
				{
					if (pressed == hitCell)
					{
						Debug.Log("Flagged");
						//Call flag function of pressed
					}
				}
			}
		}
		else
		{
			Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
		}
	}
}
