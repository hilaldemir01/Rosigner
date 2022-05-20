using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Models;


public class GridSystem : MonoBehaviour
{
    List<Wall> wallList = new List<Wall>();
    
    RosignerContext db = new RosignerContext();
    public List<FurnitureGeneticInformation> furnitureGeneticInformationList = new List<FurnitureGeneticInformation>();
    private GridXZ<GridObject> grid;
    public int wallX, wallY;
    public GameObject fur;
    public GameObject tempPrefab;



    public void Update()
    {
        if (TempScript.canGridSystemWillApplied == 1)
        {
            startGridSystem();
        }
    }

    public void startGridSystem()
    {
        TempScript.canGridSystemWillApplied = 0;
        string[] allWalls = { "W1", "W2", "W3", "W4" };
        StartCoroutine(db.WallInformation(allWalls, fetchWallInformation));
        
    }

    

    /*
    private void Awake()
    {
        string[] allWalls = { "W1", "W2", "W3", "W4" };
        StartCoroutine(db.WallInformation(allWalls, fetchWallInformation));  
    }
    */
    public void createGrid(int a, int b)
    {//To create Grid in cm. depends on wall length
        int gridHeight = a * 100;
        int gridWidth = b * 100;
        float cellSize = 0.01f; //area of a square
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (GridXZ<GridObject> g, int x, int y) => new GridObject(g, x, y));
      
        int tempwall =(int)(wallList[1].WallLength+0.1f) * 100;
     
        for (int i = 0; i < Inventory.array.Count; i++)
        {
            for(int k=0; k< furnitureGeneticInformationList.Count; k++)
            {
                if(Inventory.array[i].name == furnitureGeneticInformationList[k].FurnitureName)
                {
                    int row = TempScript.furnitureLocationList[k].CenterX;
                    int col = TempScript.furnitureLocationList[k].CenterY;
                    Vector3 tempPosition = grid.GetWorldPosition(row, col);  //dbdeb çekilecek olarak deðiþekecek
                    tempPrefab = Inventory.array[i];
                    tempPrefab.gameObject.transform.localScale = new Vector3(TempScript.FurniturList[i].Xdimension * 0.01f, TempScript.FurniturList[i].Ydimension * 0.01f, TempScript.FurniturList[i].Zdimension * 0.01f);
                    Instantiate(Inventory.array[i], new Vector3(tempPosition.x,0,tempPosition.z), Quaternion.identity);
                }

                    
            }

            
        }
       
        
        //Instantiate(fur, new Vector3(1, 0, 2), Quaternion.identity);

    }

    public void fetchFurnitureLocationInformation( List<FurnitureGeneticInformation> newfurnitureGeneticInformations)
    {
        for (int i = 0; i < newfurnitureGeneticInformations.Count; i++)
        {
            furnitureGeneticInformationList.Add(new FurnitureGeneticInformation() { FurnitureName = newfurnitureGeneticInformations[i].FurnitureName, FurnitureID = newfurnitureGeneticInformations[i].FurnitureID, GeneticLocationID = newfurnitureGeneticInformations[i].GeneticLocationID });
        }

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
    {Debug.Log("AAAAAAAAAA");
        // This part of the code is used to set the length of the walls.
        int wallX = (int)(wallList[1].WallLength);
        Debug.Log("BBBBBBB");
        int wallY = (int)(wallList[0].WallLength);

        Debug.Log("CCCCCCC");
        StartCoroutine(db.fetchAllFurnitureName(TempScript.furnitureLocationList, fetchFurnitureLocationInformation));
        Debug.Log("DDDDDDDDD");
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
