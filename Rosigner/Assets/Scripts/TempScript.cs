using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Models;
using UnityEngine.SceneManagement;
//using static GridSystem;

public class TempScript : MonoBehaviour
{
    public GameObject wallobj1;
    public GameObject wallobj2;
    public GameObject wallobj3;
    public GameObject wallobj4;
    [SerializeField] public Transform parent;
    public GameObject floor;
    RosignerContext db = new RosignerContext();
    public Furniture furniture;
    public GameObject tempPrefab;
    List<Wall> wallList = new List<Wall>();
    public static List<Furniture> FurniturList = new List<Furniture>();
    private float x, z;
    public float range;
    public int k = 0;
    [SerializeField] public GameObject doorSpawn;
    [SerializeField] public GameObject windowSpawn;
    public string tempassetName = "";
    List<RoomStructureName> roomStructureNames = new List<RoomStructureName>();
    List<RoomStructure> roomStructuresList = new List<RoomStructure>();
    RoomStructureLocation roomStructureLocation = new RoomStructureLocation();
    public static List<FurnitureGeneticLocation> furnitureLocationList = new List<FurnitureGeneticLocation>();
    public static List<FurnitureGeneticLocation> furnitureGeneticLocations = new List<FurnitureGeneticLocation>();
    GameObject tempasset;

    public static int canGridSystemWillApplied = 0;
    int canGeneticBeApplied = 0;
    void Start()
    {
        string[] allWalls = { "W1", "W2", "W3", "W4" };
        StartCoroutine(db.WallInformation(allWalls, fetchWallInformation));
        StartCoroutine(db.FurnitureInfo(furniture, fetchFurnitureInformation));

    }
    private void Update()
    {
        if (canGeneticBeApplied == 1)
        {
            GeneticConnection();
        }


    }
    public void fetchFurnitureInformation(Furniture newFurniture)
    {
        FurniturList.Add(new Furniture()
        {
            FurnitureID = newFurniture.FurnitureID,
            FurnitureTypeID = newFurniture.FurnitureTypeID,
            Xdimension = newFurniture.Xdimension,
            Ydimension = newFurniture.Ydimension,
            Zdimension = newFurniture.Zdimension,
            RoomID = newFurniture.RoomID
        });

        
        k = k + 1;
    }
    public void fetchWallInformation(List<Wall> newWall)
    {
        for (int i = 0; i < newWall.Count; i++)
        {
            wallList.Add(new Wall() { WallID = newWall[i].WallID, WallName = newWall[i].WallName, WallLength = newWall[i].WallLength, WallHeight = newWall[i].WallHeight, RoomID = newWall[i].RoomID });

        }

        CreatingWalls();

    }

    void CreatingWalls()
    {
        // This part of the code is used to set the length and width of the walls.
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

        StartCoroutine(db.RoomStructuresInformation(wallList, fetchRoomStructureInformation));

    }
    public void fetchRoomStructureInformation(List<RoomStructure> newroomstructures)
    {
        Debug.Log("newroomstructures.Count" + newroomstructures.Count);

        for (int i = 0; i < newroomstructures.Count; i++)
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
        }
        StartCoroutine(db.getFurnitureName(roomStructuresList, getStructureName));

    }

    public void getStructureName(List<RoomStructureName> newroomstructuresNames)
    {

        roomStructureNames = newroomstructuresNames;

        setpositions();

    }


    public void setpositions()
    {
        var wallName = "";
        //Vector3 position= new Vector3(roomStructureLocation.LocationX/100, roomStructureLocation.LocationY/100, roomStructureLocation.LocationZ/100);
        Vector3 position;
        Vector3 position_distance;
        // GameObject tempasset;
        for (int i = 0; i < wallList.Count; i++)
        {
            for (int j = 0; j < roomStructuresList.Count; j++)
            {
                if (wallList[i].WallID == roomStructuresList[j].WallID)
                {

                    wallName = wallList[i].WallName;


                    if (roomStructureNames[j].RoomStrucuteName == "Door(Brown)")
                    {
                        tempasset = doorSpawn;
                    }
                    else if (roomStructureNames[j].RoomStrucuteName == "window1(single)")
                    {
                        tempasset = windowSpawn;
                    }
                    /* else
                     {

                         tempasset = null;
                     }*/

                    if (wallName == "W1" && tempasset != null)
                    {
                        position = wallobj1.gameObject.transform.position;
                        position_distance = new Vector3(position.x + roomStructuresList[j].RedDotDistance, roomStructuresList[j].GroundDistance, position.z);
                        GameObject Go = Instantiate(tempasset, position_distance, Quaternion.Euler(new Vector3(0, 0, 0)));
                        //tempasset.transform.localScale = new Vector3(roomStructuresList[j].StrructureWidth, roomStructuresList[j].StrructureLength, 0.3f);
                        Go.transform.localScale = new Vector3(roomStructuresList[j].StrructureWidth, roomStructuresList[j].StrructureLength, 0.3f);
                    }
                    else if (wallName == "W2" && tempasset != null)
                    {
                        position = wallobj2.gameObject.transform.position;
                        position_distance = new Vector3(position.x, roomStructuresList[j].GroundDistance, position.z + roomStructuresList[j].RedDotDistance);
                        GameObject Go = Instantiate(tempasset, position_distance, Quaternion.Euler(new Vector3(0, 270, 0)));
                        //tempasset.transform.localScale = new Vector3(roomStructuresList[j].StrructureWidth, roomStructuresList[j].StrructureLength, 0.3f);
                        Go.transform.localScale = new Vector3(roomStructuresList[j].StrructureWidth, roomStructuresList[j].StrructureLength, 0.3f);

                    }
                    else if (wallName == "W3" && tempasset != null)
                    {
                        position = wallobj3.gameObject.transform.position;
                        position_distance = new Vector3(position.x - roomStructuresList[j].RedDotDistance, roomStructuresList[j].GroundDistance, position.z);
                        GameObject Go = Instantiate(tempasset, position_distance, Quaternion.Euler(new Vector3(0, 180, 0)));
                        //tempasset.transform.localScale = new Vector3(roomStructuresList[j].StrructureWidth, roomStructuresList[j].StrructureLength, 0.3f);
                        Go.transform.localScale = new Vector3(roomStructuresList[j].StrructureWidth, roomStructuresList[j].StrructureLength, 0.3f);

                    }
                    else if (wallName == "W4" && tempasset != null)
                    {
                        position = wallobj4.gameObject.transform.position;
                        position_distance = new Vector3(position.x, roomStructuresList[j].GroundDistance, position.z - roomStructuresList[j].RedDotDistance);
                        GameObject Go = Instantiate(tempasset, position_distance, Quaternion.Euler(new Vector3(0, 90, 0)));
                        Go.transform.localScale = new Vector3(roomStructuresList[j].StrructureWidth, roomStructuresList[j].StrructureLength, 0.3f);
                        //Go.transform.localScale = new Vector3(roomStructuresList[j].StrructureWidth, roomStructuresList[j].StrructureLength, 0.3f);
                    }

                }

            }
        }
        canGeneticBeApplied = 1;
    }
    public void GeneticConnection()
    {
        //since we don't want to enter this function more than once, we change this value
        canGeneticBeApplied = 0;
        Genome newOne = new Genome();
        int furnituresFits = CheckFurnitureAreaFit();
        string[,] floorPlan = new string[(int)wallList[1].WallLength * 100, (int)wallList[0].WallLength * 100];

        Debug.Log("furnituresFits" + furnituresFits);
        if (furnituresFits == 1)
        {
            //     furnitureGeneticLocations = newOne.GenomeInit((int)wallList[1].WallLength * 100, (int)wallList[0].WallLength * 100, floorPlan, roomStructuresList, FurniturList, wallList);
            GeneticAlgorithm genetic = new GeneticAlgorithm();
            furnitureGeneticLocations = genetic.CreateStartPopulation((int)wallList[1].WallLength*100, (int)wallList[0].WallLength*100, floorPlan, roomStructuresList, FurniturList, wallList);
            StartCoroutine(db.TempFurnitureLocation(furnitureGeneticLocations));
            fetchFurnitureLocationInformation(furnitureGeneticLocations);
            //StartCoroutine(db.FurnitureLocationsFetch(furnitureGeneticLocations, fetchFurnitureLocationInformation));
        }
        else
        {
            SceneManager.LoadScene("PreviousDesigns");
        }
      
    }



    int CheckFurnitureAreaFit()
    {
        float floorArea = (wallList[1].WallLength * 100) * (wallList[0].WallLength * 100);
        float furnitureArea;
        float sumFurnituresArea;

        sumFurnituresArea = 0;

        for (int i=0; i < FurniturList.Count; i++)
        {
            furnitureArea = (FurniturList[i].Xdimension) * (FurniturList[i].Zdimension) + (10* FurniturList[i].Zdimension);
            sumFurnituresArea = sumFurnituresArea + furnitureArea;
        }
        
        if(sumFurnituresArea < floorArea)
        {
            return 1;
        }
        else
        {
            //FurniturList.Clear();
           // wallList.Clear();
           // roomStructuresList.Clear();
           // return 0;
        }

        return 1;
    }



    public void fetchFurnitureLocationInformation(List<FurnitureGeneticLocation> newFurnitureLocation)
    { //To make a list for wall information .
        for (int i = 0; i < newFurnitureLocation.Count; i++)
        {
            furnitureLocationList.Add(new FurnitureGeneticLocation() { GeneticLocationID = newFurnitureLocation[i].GeneticLocationID, FurnitureID = newFurnitureLocation[i].FurnitureID, StartX = newFurnitureLocation[i].StartX, FinishX = newFurnitureLocation[i].FinishX, CenterX = newFurnitureLocation[i].CenterX, StartY = newFurnitureLocation[i].StartY, FinishY = newFurnitureLocation[i].FinishY, CenterY = newFurnitureLocation[i].CenterY });
        }
        canGridSystemWillApplied = 1;
      
    }


}
