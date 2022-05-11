using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Models;


public class GridSystem : MonoBehaviour
{
    List<Wall> wallList = new List<Wall>();
    RosignerContext db = new RosignerContext();
    private GridXZ<GridObject> grid;
    public int wallX, wallY;
    public GameObject fur;


    private void Awake()
    {
        string[] allWalls = { "W1", "W2", "W3", "W4" };
        StartCoroutine(db.WallInformation(allWalls, fetchWallInformation));
    }

    
    public void createGrid(int a, int b)
    {//To create Grid in cm. depends on wall length
        int gridHeight = a * 100;
        int gridWidth = b * 100;
        float cellSize = 0.01f; //area of a square
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (GridXZ<GridObject> g, int x, int y) => new GridObject(g, x, y));
        //Instantiate(fur, new Vector3(1, 0, 2), Quaternion.identity);

    }

    public void fetchWallInformation(List<Wall> newWall)
    { //To make a list for wall information .
        for (int i = 0; i < newWall.Count; i++)
        {
            wallList.Add(new Wall() { WallID = newWall[i].WallID, WallName = newWall[i].WallName, WallLength = newWall[i].WallLength, WallHeight = newWall[i].WallHeight, RoomID = newWall[i].RoomID });
        }
        GettingWallsInfo();
    }

    void GettingWallsInfo()
    {
        // This part of the code is used to set the length of the walls.
        int wallX = (int)(wallList[1].WallLength);
        int wallY = (int)(wallList[0].WallLength);
        createGrid(wallX, wallY);
    }
    public class GridObject
    { 

        private GridXZ<GridObject> grid;
        private int x, y;

        public GridObject(GridXZ<GridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return x + ", " + y;
        }
    }

}
