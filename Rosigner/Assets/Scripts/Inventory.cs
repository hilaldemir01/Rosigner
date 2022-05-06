using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Linq;
public class Inventory : MonoBehaviour
{
    GameObject[] livingRoomPrefabs;
    GameObject[] bedroomPrefabs;
    GameObject[] studyRoomPrefabs;
    public Image selectedFurnitureImage, furnitureImage;
    public List<Slots> slots; 
    public Slots selectedSlot;
    public Transform furnitureBar;
    public InputField widthInput, heightInput, lengthInput; //x_dimension input, y_dimension input, z_dimension input
    public static GameObject[] mergePrefabs;
    public Button saveButton, applyButton;
    public Text notificationTxt;
    public string selectedFurnitureImageName, prefabName;
    int count=0;
    public static GameObject prefabDeneme;
    TempScript prefab = new TempScript();
    RosignerContext db = new RosignerContext();
    Room newRoom = new Room(); 
    List<Furniture> furnitureList = new List<Furniture>();


    // Start is called before the first frame update
    void Start()
    {
        widthInput.characterValidation=InputField.CharacterValidation.Integer;
        heightInput.characterValidation=InputField.CharacterValidation.Integer;
        lengthInput.characterValidation=InputField.CharacterValidation.Integer;
        
        widthInput.interactable=false;
        heightInput.interactable=false;
        lengthInput.interactable=false;
        saveButton.interactable = false;
        applyButton.interactable = false;
        notificationTxt.gameObject.SetActive(false);
       
        livingRoomPrefabs = Resources.LoadAll<GameObject>("living room prefab");
        bedroomPrefabs = Resources.LoadAll<GameObject>("bedroom prefab");
        studyRoomPrefabs = Resources.LoadAll<GameObject>("study room prefab");

        mergePrefabs = livingRoomPrefabs.Concat(bedroomPrefabs).Concat(studyRoomPrefabs).ToArray();
    }
    public void CallInventory()
    {
        StartCoroutine(InventoryFunc());
    }

    IEnumerator InventoryFunc(){
        int widthvalue= int.Parse(widthInput.text);
        int heightvalue = int.Parse(heightInput.text);
        int lengthvalue = int.Parse(lengthInput.text);
       
        selectedFurnitureImageName = selectedFurnitureImage.sprite.name; 
        
        //to check user's measurement inputs 
        if(widthvalue <= 0 || heightvalue <= 0 || lengthvalue <= 0 )
        {
            notificationTxt.gameObject.SetActive(true);
            notificationTxt.text = "Please enter positive measure";
        }
        else{
            Furniture furnitureMeasurement = new Furniture();
            furnitureMeasurement.Ydimension = heightvalue;
            furnitureMeasurement.Xdimension = widthvalue;
            furnitureMeasurement.Zdimension = lengthvalue;

            notificationTxt.gameObject.SetActive(true);
            StartCoroutine(db.Furniture(furnitureMeasurement,selectedFurnitureImageName)) ;
        }
       
        yield return new WaitForSeconds(1);
    
    }

    public void ApplyButton(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("TempDesign");
    }

    public void Subscribe(Slots slot){
    //to make a list of slots
        if(slots == null){
            slots = new List<Slots>();
        }
        slots.Add(slot);
    }
   
    public string OnSlotSelected(Slots slot){
    //when the slot selected
        if(selectedSlot != null){
            selectedSlot.Deselect();
        }
        selectedSlot = slot;
        selectedSlot.Select();

        selectedFurnitureImageName = ShowFurnitureImage(selectedSlot); 
        return selectedFurnitureImageName;
    }
    public string ShowFurnitureImage(Slots selectedSlot){
        //to take selected image from furniture area inventory and locate it to right side
        furnitureBar = selectedSlot.transform.GetChild(0);
        furnitureImage  = furnitureBar.gameObject.GetComponent<Image>();
        selectedFurnitureImage.sprite = furnitureImage.sprite;
        selectedFurnitureImageName = selectedFurnitureImage.sprite.name; 
        Debug.Log("selected furniture name: "+selectedFurnitureImageName);
        for(int i=0; i<mergePrefabs.Length;i++){  
           if(selectedFurnitureImage.sprite.name == mergePrefabs[i].name){
               prefabName = mergePrefabs[i].name;
               prefabDeneme = mergePrefabs[i];
               prefab.SettingFurniture(mergePrefabs[i]);
               break;
           } 
        }
        widthInput.interactable=true;
        heightInput.interactable=true;
        lengthInput.interactable=true; 
        saveButton.interactable = false; 
        notificationTxt.gameObject.SetActive(false);
        
        //newRoom.RoomID = int.Parse(roomID);

        /*furnitureList.Add(new Furniture() { Xdimension = "W1", Ydimension = wall1inp, Zdimension = heightinp, RoomID = newRoom.RoomID });
        Debug.Log("RoomID:" + newRoom.RoomID);
        for (int i = 0; i < 4; i++)
        {
            StartCoroutine(db.Wall(wallList[i], getWallID)); // generate new wall ids and insert those to the db
        }*/



        resetMeasurement();
        return selectedFurnitureImage.sprite.name;
    }

    public void resetMeasurement(){
        //to reset inputFields
        widthInput.text="";
        heightInput.text="";
        lengthInput.text="";
    }
   
    
     public async void VerifyInputs(){

         //if(count>0){
           // Debug.Log("count"+count);
            applyButton.interactable = true;
        //}
        // to check user's measurement inputs and set save button active 
        if((widthInput.text.Length > 0) && (heightInput.text.Length > 0) && (lengthInput.text.Length > 0))
        {
            saveButton.interactable = true;
            count++;
        }
       
       
    }
}