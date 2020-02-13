using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Managing the grid objects, 
    1. Making the grid
    2. check the surrounding grids (3x3)
    3. manage when it's clicked or flagged
    4. Check if it's a mine cell
 */

public class GridManager : MonoBehaviour
{
    Cell[,] grid;
    public GameObject cellPrefab;

    public int width;
    public int height;


    // Start is called before the first frame update
    void Start()
    {
        cellPrefab = Resources.Load("Cell", typeof(GameObject)) as GameObject;

        grid = new Cell[width, height];

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                grid[i, j] = Instantiate(cellPrefab, new Vector3(i, j, 0), cellPrefab.transform.rotation).GetComponent<Cell>();
            }
        }

        Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
