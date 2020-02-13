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
    public enum Difficulty
    {
        BEGINNER,
        INTERMEDIATE,
        EXPERT
    }


    Cell[,] grid;
    public GameObject cellPrefab;

    int width;
    int height;
    int numOfMines;
    public Difficulty difficulty;

    // Start is called before the first frame update
    void Start()
    {
        cellPrefab = Resources.Load("Cell", typeof(GameObject)) as GameObject;
        
        SetDifficulty();
        SetGrid();
        SetMines();
       
        Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -50);
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CheckTheSurroundingCells(int x, int y)
    {


    
    }


    void SetDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.BEGINNER:
                numOfMines = 10;
                width = 10;
                height = 10;
                break;
            case Difficulty.INTERMEDIATE:
                numOfMines = 40;
                width = 15;
                height = 15;
                break;
            case Difficulty.EXPERT:
                numOfMines = 99;
                width = 20;
                height = 20;
                break;

        }
    }

    void SetGrid()
    {
        grid = new Cell[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                grid[i, j] = Instantiate(cellPrefab, new Vector3(i, j, 0), cellPrefab.transform.rotation).GetComponent<Cell>();
                grid[i, j].isMine = false;
            }
        }
    }

    void SetMines()
    {
        for (int i = 0; i < numOfMines; i++)
        {
            int randomPosX = 0;
            int randomPosY = 0;
            do
            {
                randomPosX = Random.Range(0, width);
                randomPosY = Random.Range(0, height);
            }
            while (grid[randomPosX, randomPosY].isMine);

            grid[randomPosX, randomPosY].isMine = true;
            Debug.Log(randomPosX + " " + randomPosY + ": This is a mine");
        }
    }
}
