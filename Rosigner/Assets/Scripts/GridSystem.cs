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
    public int continueSpawn = 0;


    public void Update()
    {
        if (TempScript.canGridSystemWillApplied == 1)
        {
            startGridSystem();
        }

        if (continueSpawn == 1)
        {
            int wallX = (int)(wallList[1].WallLength);
            int wallY = (int)(wallList[0].WallLength);
            createGrid(wallX, wallY);
        }
    }

    public void startGridSystem()
    {
        TempScript.canGridSystemWillApplied = 0;
        string[] allWalls = { "W1", "W2", "W3", "W4" };
        StartCoroutine(db.WallInformation(allWalls, fetchWallInformation));

    }

    public void createGrid(int a, int b)
    {//To create Grid in cm. depends on wall length

        continueSpawn = 0;
        int gridHeight = a * 100;
        int gridWidth = b * 100;
        float cellSize = 0.01f; //area of a square
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (GridXZ<GridObject> g, int x, int y) => new GridObject(g, x, y));

        int tempwall = (int)(wallList[1].WallLength + 0.1f) * 100;

        for (int i = 0; i < Inventory.array.Count; i++)
        {
            for (int k = 0; k < furnitureGeneticInformationList.Count; k++)
            {
                if (Inventory.array[i].name == furnitureGeneticInformationList[k].FurnitureName)
                {

                    int row = TempScript.furnitureLocationList[k].CenterY;
                    int col = TempScript.furnitureLocationList[k].CenterX;

                    float rotation = Inventory.array[i].transform.rotation.y;
                    Debug.Log("***********rotation: " + rotation);

                    Debug.Log("furniture info: " + grid.GetWorldPosition(row, col) + " genetic id: " + furnitureGeneticInformationList[k].GeneticLocationID + " furniture id: " + furnitureGeneticInformationList[k].FurnitureID);
                    Vector3 tempPosition = grid.GetWorldPosition(row, tempwall - col);  //dbdeb �ekilecek olarak de�i�ekecek
                    tempPrefab = Inventory.array[i];
                    tempPrefab.gameObject.transform.localScale = new Vector3(TempScript.FurniturList[i].Xdimension * 0.01f, TempScript.FurniturList[i].Ydimension * 0.01f, TempScript.FurniturList[i].Zdimension * 0.01f);
                    Instantiate(Inventory.array[i], new Vector3(tempPosition.x, (tempPrefab.gameObject.transform.localScale.y)/2, tempPosition.z), Quaternion.Euler(0, rotation, 0));
                }


            }


        }


        //Instantiate(fur, new Vector3(1, 0, 2), Quaternion.identity);

    }

    public void fetchFurnitureLocationInformation(List<FurnitureGeneticInformation> newfurnitureGeneticInformations)
    {
        for (int i = 0; i < newfurnitureGeneticInformations.Count; i++)
        {
            furnitureGeneticInformationList.Add(new FurnitureGeneticInformation() { FurnitureName = newfurnitureGeneticInformations[i].FurnitureName, FurnitureID = newfurnitureGeneticInformations[i].FurnitureID, GeneticLocationID = newfurnitureGeneticInformations[i].GeneticLocationID });
        }

        continueSpawn = 1;
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
        StartCoroutine(db.fetchAllFurnitureName(TempScript.furnitureLocationList, fetchFurnitureLocationInformation));
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
