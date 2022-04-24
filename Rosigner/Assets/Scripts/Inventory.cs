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
    //[SerializeField] public GameObject armchair;
    GameObject[] livingRoomPrefabs;
    GameObject[] bedroomPrefabs;
    GameObject[] studyRoomPrefabs;
    //public Sprite armchair_img;
    public Image selectedFurnitureImage;
    public List<Slots> slots; 
    public Slots selectedSlot;
    public Transform furnitureBar;
    public Image furnitureImage; 
   //public Measurement measurement;
    public InputField widthInput, heightInput, lengthInput;
    GameObject[] mergePrefabs;
    public Button saveButton;
    public Text notificationTxt;
    public string selectedFurnitureImageName;
    public string prefabName;
    public string a;
    RosignerContext db = new RosignerContext();


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
            //notificationTxt.text = "Measures saved successfully"; contextte
            Measurement furnitureMeasurement = new Measurement();
            
            furnitureMeasurement.furniture_height = heightvalue;
            furnitureMeasurement.furniture_width = widthvalue;
            furnitureMeasurement.furniture_length = lengthvalue;
            furnitureMeasurement.furniture_name = selectedFurnitureImageName;
            Debug.Log("hello"+selectedFurnitureImageName);
            notificationTxt.gameObject.SetActive(true);
            StartCoroutine(db.Measurement(furnitureMeasurement)) ;
        }
       
        yield return new WaitForSeconds(1);
    
    
    
    }
    /*
     public void GetMeasurement(){
      
        int widthvalue= int.Parse(widthInput.text);
        int heightvalue = int.Parse(heightInput.text);
        int lengthvalue = int.Parse(lengthInput.text);
        VerifyMeasurement(widthvalue,heightvalue,lengthvalue);
        //burda db bilgileri çekip bunun için ayrı fonksiyon yap onclick te o fonksiyonu çağır resetlencek resetfunc
        Debug.Log("w"+widthvalue);
        Debug.Log("h"+heightvalue);
        Debug.Log("l"+lengthvalue);

    }*/
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
        Debug.Log("img"+selectedFurnitureImageName);
        return selectedFurnitureImageName;
    }
    public string ShowFurnitureImage(Slots selectedSlot){
        //to take selected image from furniture area inventory and locate it to right side
        furnitureBar = selectedSlot.transform.GetChild(0);
        furnitureImage  = furnitureBar.gameObject.GetComponent<Image>();
        selectedFurnitureImage.sprite = furnitureImage.sprite;
        selectedFurnitureImageName = selectedFurnitureImage.sprite.name; 
        Debug.Log("bura "+selectedFurnitureImageName);

        for(int i=0; i<mergePrefabs.Length;i++){  
           if(selectedFurnitureImage.sprite.name == mergePrefabs[i].name){
               Debug.Log("FIND: "+mergePrefabs[i].name);
               prefabName = mergePrefabs[i].name;
               break;
           } 
        }
        //a = "beyza";
        widthInput.interactable=true;
        heightInput.interactable=true;
        lengthInput.interactable=true; 
        saveButton.interactable = false; 
        notificationTxt.gameObject.SetActive(false);
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
        // to check user's measurement inputs and set save button active 
        if((widthInput.text.Length > 0) && (heightInput.text.Length > 0) && (lengthInput.text.Length > 0))
        {
            saveButton.interactable = true;
        }
        
        
    }
}