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
    [SerializeField] public GameObject doorSpawn;
    [SerializeField] public GameObject windowSpawn;
    [SerializeField] public GameObject objectToBeSpawned;
    [SerializeField] public Transform parent;
    List<Wall> wallList = new List<Wall>();
    List<RoomStructure> roomStructuresList;

    void Start(){

        string[] allWalls = { "W1", "W2", "W3", "W4" };
        StartCoroutine(db.WallInformation(allWalls, fetchWallInformation));
        //   StartCoroutine(db.FurnitureInfo(furniture, fetchFurnitureInformation));


        deneme();
        
    }

    public void SettingFurniture(GameObject prefab){
        tempPrefab = prefab;
        Debug.Log("temp "+tempPrefab);
    }
    public void deneme(){
        tempPrefab = Inventory.prefabDeneme;
        Debug.Log("TEMP2 "+tempPrefab);
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
    public void fetchRoomStructureInformation(List<RoomStructure> newroomstructures)
    {
        for(int i = 0; i < newroomstructures.Count; i++)
        {
            roomStructuresList.Add(new RoomStructure()
            {
                RoomStructureID = newroomstructures[i].RoomStructureID,
                StrructureLength = newroomstructures[i].StrructureLength,
                StrructureWidth = newroomstructures[i].StrructureWidth,
                RedDotDistance = newroomstructures[i].RedDotDistance,
                GroundDistance = newroomstructures[i].GroundDistance,
                FurnitureTypeID = newroomstructures[i].FurnitureTypeID,
                WallID = newroomstructures[i].WallID
            });
            for(int j=0; j < wallList.Count; j++)
            {
                if(roomStructuresList[i].WallID == wallList[j].WallID)
                {
                    RoomStructures(roomStructuresList[j].RedDotDistance, roomStructuresList[j].GroundDistance,wallList[j].WallName);
                }
            }
            
        }

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

        for(int i = 0; i < wallList.Count; i++)
        {
            StartCoroutine(db.RoomStructuresInformation(wallList[0].WallID, fetchRoomStructureInformation));

        }

    }

    bool RoomStructures(float inputDistanceFromWall,float inputDistanceFromGround,string  wallName)
    {
        float wallDistance, groundDistance, TempGroundDistance;
        float tempScaleWidth, tempScaleHeight;

        wallDistance = inputDistanceFromWall / 100.0f;
        TempGroundDistance = inputDistanceFromGround / 100.0f;

        // This part assigns the position values of the selected wall to the position1
        Vector3 position1 = selectedObject.transform.parent.position;

        // Tag of the parents of the selectedObject is compared, and if one of the walls is clicked and  
        // a distance value is entered, then the door/window will be placed on that wall in the given distance

        if (selectedObject.transform.parent.name == "W1")
        {
            tempScaleWidth = selectedObject.transform.parent.localScale.x;
            tempScaleHeight = selectedObject.transform.parent.localScale.y;
            Vector3 position_distance = new Vector3(position1.x + wallDistance, groundDistance, position1.z);
            if (RoomStructureSizing(tempScaleHeight, tempScaleWidth, wallDistance, groundDistance, selectedObject.transform.parent.name) == true)
            {
                Instantiate(tempAsset, position_distance, Quaternion.Euler(new Vector3(0, 0, 0)), parent);
            }
            else
            {
                Debug.Log("Asti Boyu w1");
                return false;
            }
        }
        else if (selectedObject.transform.parent.name == "W2")
        {
            tempScaleWidth = selectedObject.transform.parent.localScale.x;
            tempScaleHeight = selectedObject.transform.parent.localScale.y;
            Vector3 position_distance = new Vector3(position1.x, groundDistance, position1.z + wallDistance);

            if (RoomStructureSizing(tempScaleHeight, tempScaleWidth, wallDistance, groundDistance, selectedObject.transform.parent.name) == true)
            {
                Instantiate(tempAsset, position_distance, Quaternion.Euler(new Vector3(0, 270, 0)), parent);
            }
            else
            {
                Debug.Log("Asti Boyu w2");
                return false;
            }
        }
        else if (selectedObject.transform.parent.name == "W3")
        {
            tempScaleWidth = selectedObject.transform.parent.localScale.x;
            tempScaleHeight = selectedObject.transform.parent.localScale.y;
            Vector3 position_distance = new Vector3(position1.x - wallDistance, groundDistance, position1.z);
            if (RoomStructureSizing(tempScaleHeight, tempScaleWidth, wallDistance, groundDistance, selectedObject.transform.parent.name) == true)
            {
                Instantiate(tempAsset, position_distance, Quaternion.Euler(new Vector3(0, 180, 0)), parent);
            }
            else
            {
                Debug.Log("Asti Boyu w3");
                return false;
            }


        }
        else if (selectedObject.transform.parent.name == "W4")
        {
            tempScaleWidth = selectedObject.transform.parent.localScale.x;
            tempScaleHeight = selectedObject.transform.parent.localScale.y;
            Vector3 position_distance = new Vector3(position1.x, groundDistance, position1.z - wallDistance);

            if (RoomStructureSizing(tempScaleHeight, tempScaleWidth, wallDistance, groundDistance, selectedObject.transform.parent.name) == true)
            {
                Instantiate(tempAsset, position_distance, Quaternion.Euler(new Vector3(0, 90, 0)), parent);
            }
            else
            {
                Debug.Log("Asti Boyu w4");
                return false;
            }
        }
        return true;
    }

    bool RoomStructureSizing(float tempScaleHeight, float tempScaleWidth, float wallDistance, float groundDistance, string wallName)
    {
        float.TryParse(inputHeight.text, out float result1);
        height = result1 / 100.0f;

        float.TryParse(inputWidth.text, out float result2);
        width = result2 / 100.0f;

 

        StartCoroutine(db.RoomStructure(wallName, tempAsset.name.ToString(), newRoomStructure));
        tempAsset.transform.localScale = new Vector3(width, height, 0.3f);
        return true;
       

    }
}
