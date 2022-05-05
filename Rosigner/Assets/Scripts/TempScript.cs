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

    float wall1inp = 3.0f, wall2inp = 4.0f, heightinp = 3.0f;
    void Start(){
       // StartCoroutine(db.FurnitureInfo(furniture, fetchFurnitureInformation));
      //  deneme();
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
    void Update()
    {

        CreatingWalls();
    }

    void CreatingWalls()
    {


        // This part of the code is used to set the length and width of the walls.
        wallobj1.gameObject.transform.localScale = new Vector3(wall1inp, heightinp, 0.2f);
        wallobj2.gameObject.transform.localScale = new Vector3(wall2inp, heightinp, 0.2f);
        wallobj3.gameObject.transform.localScale = new Vector3(wall1inp, heightinp, 0.2f);
        wallobj4.gameObject.transform.localScale = new Vector3(wall2inp, heightinp, 0.2f);
        floor.gameObject.transform.localScale = new Vector3(wall1inp, 0.1f, wall2inp);


        // This part of the code is used to place walls in a way that they create an enclosed rectangular shape.
        wallobj1.gameObject.transform.position = new Vector3(0, 0, 0);
        wallobj2.gameObject.transform.position = new Vector3(wall1inp + 0.1f, 0, 0.1f);
        wallobj3.gameObject.transform.position = new Vector3(wall1inp, 0, wall2inp + 0.2f);
        wallobj4.gameObject.transform.position = new Vector3(-0.1f, 0, wall2inp + 0.1f);
        floor.gameObject.transform.position = new Vector3(wall1inp / 2.0f, -0.05f, (wall2inp / 2.0f) + 0.1f);
        

    }
}
