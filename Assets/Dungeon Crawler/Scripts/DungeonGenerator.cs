using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];

    }

    public Vector2 size;
    public int startPos = 0;
    public GameObject room;
    public Vector2 offset;
    List<Cell> board;
    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];
                if (currentCell.visited)
                {
                    var newRoom = Instantiate(room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(currentCell.status);

                    newRoom.name += " " + i + "-" + j;
                }
               
            }
        }
    }
    void MazeGenerator()
    {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while(k<1000)
        {
            k++;

            board[currentCell].visited = true;

            // If in the last cell break out of while loop
            if (currentCell == board.Count - 1){
                break;
            }
            //Chekk the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0) // no available neighbors
            {
                if(path.Count == 0) //last cell in path
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                // Update room doors based on cell positions
                if (newCell > currentCell)
                {
                    // Down or Right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true; // Right
                        currentCell = newCell;
                        board[currentCell].status[3] = true; // Left
                    }
                    else{
                        board[currentCell].status[1] = true; // Down
                        currentCell = newCell;
                        board[currentCell].status[0] = true; // Up
                    }
                }
                else
                {
                    // Up or Left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true; // Left
                        currentCell = newCell;
                        board[currentCell].status[2] = true; // Right
                    }
                    else{
                        board[currentCell].status[0] = true; // Up
                        currentCell = newCell;
                        board[currentCell].status[1] = true; // Down
                    }
                }
            }
            // // Additional check: If this is the last iteration, make sure there's an available neighbor
            // if (k == 1000 && neighbors.Count == 0 && path.Count == 0)
            // {
            //     currentCell = path.Pop();
            // }
        }
        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();
        
        //check north neighbor
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }
        //check south neighbor
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }
        //check east neighbor
        if ((cell+1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }
        //check west neighbor
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbors;
    }
}
