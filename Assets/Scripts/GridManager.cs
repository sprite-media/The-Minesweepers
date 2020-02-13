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


    // Start is called before the first frame update
    void Start()
    {
        cellPrefab = Resources.Load("Cell", typeof(GameObject)) as GameObject;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
