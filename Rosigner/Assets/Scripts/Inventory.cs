using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
       // widthInput = GameObject.Find("Canvas/RightSideLayout/MeasurementField/Border/WidthInputField");
        widthInput.interactable=false;
        heightInput.interactable=false;
        lengthInput.interactable=false;
        saveButton.interactable = false;

        int i = 0 , j=0, k = 0;
        //armchair.name = "armchair_01";
        //armchair_img.name =  armchair.name;  
        //Instantiate(armchair, new Vector3(0,0,0),Quaternion.identity); kanıtladık
       
        livingRoomPrefabs = Resources.LoadAll<GameObject>("living room prefab");
        bedroomPrefabs = Resources.LoadAll<GameObject>("bedroom prefab");
        studyRoomPrefabs = Resources.LoadAll<GameObject>("study room prefab");

        mergePrefabs = livingRoomPrefabs.Concat(bedroomPrefabs).Concat(studyRoomPrefabs).ToArray();

        Debug.Log( ""+mergePrefabs.Length);

        /*Debug.Log("Living Room:");
        while(i < livingRoomPrefabs.Length){
            Debug.Log(" "+livingRoomPrefabs[i]);
            i++;
        }
        Debug.Log("Bedroom:"+bedroomPrefabs.Length);
        while(j < bedroomPrefabs.Length){
            Debug.Log(" "+bedroomPrefabs[j]);
            j++;
        }
        Debug.Log("Study Room:"+studyRoomPrefabs.Length);
        while(k < studyRoomPrefabs.Length){
            Debug.Log(" "+studyRoomPrefabs[k]);
            k++;
        }
        */
    }

    public void Subscribe(Slots slot){
    //to make a list of slots
        if(slots == null){
            slots = new List<Slots>();
        }
        slots.Add(slot);
    }
   

    public void OnSlotSelected(Slots slot){
    //when the slot selected
        if(selectedSlot != null){
            selectedSlot.Deselect();
        }
        selectedSlot = slot;
        selectedSlot.Select();
        ShowFurnitureImage(selectedSlot);   
    
    }
     public void InstantiateCaller(GameObject prefab)
     {
         Instantiate(prefab);
     }
  
    public void ShowFurnitureImage(Slots selectedSlot){
        //to take selected image from furniture area inventory and locate it to right side
        furnitureBar = selectedSlot.transform.GetChild(0);
        furnitureImage  = furnitureBar.gameObject.GetComponent<Image>();
        selectedFurnitureImage.sprite = furnitureImage.sprite;
        for(int i=0; i<mergePrefabs.Length;i++){  
           if(selectedFurnitureImage.sprite.name == mergePrefabs[i].name){
               Debug.Log("FIND: "+mergePrefabs[i].name);
               break;
           } 
       }
  
        widthInput.interactable=true;
        heightInput.interactable=true;
        lengthInput.interactable=true;  
        resetMeasurement();
        
    }

    public void resetMeasurement(){

        widthInput.text="";
        heightInput.text="";
        lengthInput.text="";
    }
   
    public void GetMeasurement(){
      
        var widthvalue= int.Parse(widthInput.text);
        var heightvalue = int.Parse(heightInput.text);
        var lengthvalue = int.Parse(lengthInput.text);
    
        //burda db bilgileri çekip bunun için ayrı fonksiyon yap onclick te o fonksiyonu çağır resetlencek resetfunc
        Debug.Log("w"+widthvalue);
        Debug.Log("h"+heightvalue);
        Debug.Log("l"+lengthvalue);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public async void VerifyInputs()
    {
        if((widthInput.text.Length > 0) && (heightInput.text.Length > 0) && (lengthInput.text.Length > 0))
        {
            Debug.Log("girdi");
            saveButton.interactable = true;
        }
    }
}