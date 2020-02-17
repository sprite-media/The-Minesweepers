using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public static GridManager instance;
    private delegate void ForAllCell(int x, int y); //Delegate to pass function that controlls cell
    private Cell[,] grid;
    private GameObject cellPrefab;//Original prefab. Once it's assigned, it should not be modified.
    public List<Texture2D> cellTextures { get; private set; }
    //public Color[] textColors;
    public bool isLose = false;
    public bool isWin = false;
    public Difficulty difficulty;
    //Determined by difficulty
    private int width;
    private int height;
    public int numOfMines { get; private set; }
    private int numOfHiddenOrFlag;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        //Loading from Resources folder during runtime
        cellPrefab = Resources.Load("Cell", typeof(GameObject)) as GameObject;
        cellTextures = new List<Texture2D>();
        for (int i = 0; i < (int)Cell.Status.COONT; i++)
        {
            cellTextures.Add(Resources.Load("Textures/" + ((Cell.Status)i).ToString(), typeof(Texture2D)) as Texture2D);
        }

        SetDifficulty();
        SetGrid();
        SetMines();
        ForEachCell(CheckTheSurroundingCells);
    }

    //Loops through all the cells and do something with the cell
    private void ForEachCell(ForAllCell func)
    {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                func(i, j);
    }
    private bool WithinBoundary(int x, int y)
    {
        return !(x < 0 || x > width - 1 || y < 0 || y > height - 1);
    }

    private void SetDifficulty()
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
        CameraView.AdjustCameraPosition(width, height);
    }
    private void SetGrid()
    {
        //Initialize grid and create actual game object
        grid = new Cell[width, height];

        //Running ForEachCell function passing C# lambda expression
        ForEachCell((i, j) => 
        {
            grid[i, j] = Instantiate(cellPrefab, new Vector3(i, j, 0), cellPrefab.transform.rotation).GetComponent<Cell>();
            grid[i, j].SetIndex(new Vector2Int(i, j));
            grid[i, j].isMine = false;
        });
    }
    private void SetMines()
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
            Debug.Log(randomPosX + ", " + randomPosY + ": This is a mine");
        }
    }

    private void CheckTheSurroundingCells(int x, int y)
    {
        CheckTheSurroundingCells(grid[x, y]);
    }
    private void CheckTheSurroundingCells(Cell checkCell)
    {
        if (!checkCell.isMine)
        {
            for (int i = (int)checkCell.index.x - 1; i <= checkCell.index.x + 1; i++)
            {
                for (int j = (int)checkCell.index.y - 1; j <= checkCell.index.y + 1; j++)
                {
                    if (WithinBoundary(i, j) && grid[i, j].isMine)
                    {
                        checkCell.surroundingArea++;
                    }
                }
            }
        }
        checkCell.SetText(checkCell.surroundingArea.ToString());
    }

    /*
    GridManager is the only function that has access to the cells.
    Other classes don't have any reference to the cells(grid)
    These functions will be called by GameManager which is triggered by Player class.
    //*/
    public bool ClickAt(int x, int y)
    {
        if (grid[x, y].isMine && grid[x,y].checkStatus != Cell.Status.FLAGGED)
        {
            isLose = true;
        }
        return grid[x, y].Clicked();
    }
    public bool ClickAt(Vector2Int index)
    {
        return ClickAt(index.x, index.y);
    }

    public bool FlagAt(int x, int y)
    {
        return grid[x, y].Flagged();
    }
    public bool FlagAt(Vector2Int index)
    {
        return FlagAt(index.x, index.y);
    }

    //This function is called in cell's Reveal function.
    //Cell class can't affect other cells so a cell calls this function to reveal other cells
    public void RevealAreaAt(int x, int y)
    {
        Debug.Log(x);
        Debug.Log(y);
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i < 0 || i > width - 1 || j < 0 || j > height - 1 || (i == x && j == y))
                    continue;
                grid[i, j].Clicked();
            }
        }
    }
    public void RevealAreaAt(Vector2Int index)
    {
        RevealAreaAt(index.x, index.y);
    }

    public void CheckGrid() 
    {
        int numOfHiddenOrFlag = 0;
        ForEachCell((i, j) =>
        {
            if (grid[i, j].checkStatus == Cell.Status.HIDDEN || grid[i, j].checkStatus == Cell.Status.FLAGGED)
            {
                numOfHiddenOrFlag++;
                Debug.Log(numOfHiddenOrFlag);
            }
        });
        if (numOfHiddenOrFlag == numOfMines)
        {
            Debug.Log("WIN");
            isWin = true;
        }       
    }

}
