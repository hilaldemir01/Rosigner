using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Models;

public class TempScript : MonoBehaviour
{
    public GameObject wallobj1;
    public GameObject wallobj2;
    public GameObject wallobj3;
    public GameObject wallobj4;
    public GameObject floor;
    RosignerContext db = new RosignerContext();
    public Furniture furniture;
    public GameObject tempPrefab;
    List<Wall> wallList = new List<Wall>();

    void Start(){

        string[] allWalls = { "W1", "W2", "W3", "W4" };
        StartCoroutine(db.WallInformation(allWalls, fetchWallInformation));
        StartCoroutine(db.FurnitureInfo(furniture, fetchFurnitureInformation));
        gettingPrefab();
        
    }

    public void gettingPrefab(){
        tempPrefab = Inventory.sendingPrefab;
        Debug.Log("prefab: "+tempPrefab);
        Instantiate(tempPrefab, new Vector3(2, 1, 1), Quaternion.identity);

    }
    public void fetchFurnitureInformation(Furniture newFurniture)
    {
        furniture.FurnitureID=newFurniture.FurnitureID;
        furniture.FurnitureTypeID = newFurniture.FurnitureTypeID;
        furniture.Xdimension = newFurniture.Xdimension;
        furniture.Ydimension = newFurniture.Ydimension;
        furniture.Zdimension = newFurniture.Zdimension;
        furniture.RoomID=newFurniture.RoomID;

    
    }
    public void fetchWallInformation(List<Wall> newWall)
    {
        for(int i = 0; i < newWall.Count; i++)
        {
            wallList.Add(new Wall() { WallID = newWall[i].WallID, WallName = newWall[i].WallName, WallLength = newWall[i].WallLength, WallHeight = newWall[i].WallHeight, RoomID = newWall[i].RoomID });

        }

        CreatingWalls();

    }

    void CreatingWalls()
    {
        // This part of the code is used to set the length and width of the walls.
        wallobj1.gameObject.transform.localScale = new Vector3(wallList[0].WallLength, wallList[0].WallHeight, 0.2f);
        wallobj2.gameObject.transform.localScale = new Vector3(wallList[1].WallLength, wallList[1].WallHeight, 0.2f);
        wallobj3.gameObject.transform.localScale = new Vector3(wallList[2].WallLength, wallList[2].WallHeight, 0.2f);
        wallobj4.gameObject.transform.localScale = new Vector3(wallList[3].WallLength, wallList[3].WallHeight, 0.2f);
        floor.gameObject.transform.localScale = new Vector3(wallList[0].WallLength, 0.1f, wallList[1].WallLength);


        // This part of the code is used to place walls in a way that they create an enclosed rectangular shape.
        wallobj1.gameObject.transform.position = new Vector3(0, 0, 0);
        wallobj2.gameObject.transform.position = new Vector3(wallList[0].WallLength + 0.1f, 0, 0.1f);
        wallobj3.gameObject.transform.position = new Vector3(wallList[0].WallLength, 0, wallList[1].WallLength + 0.2f);
        wallobj4.gameObject.transform.position = new Vector3(-0.1f, 0, wallList[1].WallLength + 0.1f);
        floor.gameObject.transform.position = new Vector3(wallList[0].WallLength / 2.0f, -0.05f, (wallList[1].WallLength / 2.0f) + 0.1f);

    }
}
