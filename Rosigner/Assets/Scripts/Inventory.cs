using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
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
   // public Measurement measurement;
    public InputField widthInput, heightInput, lengthInput;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0 , j=0, k = 0;
        //armchair.name = "armchair_01";
        //armchair_img.name =  armchair.name;  
        //Instantiate(armchair, new Vector3(0,0,0),Quaternion.identity); kanıtladık
       
        livingRoomPrefabs = Resources.LoadAll<GameObject>("living room prefab");
        bedroomPrefabs = Resources.LoadAll<GameObject>("bedroom prefab");
        studyRoomPrefabs = Resources.LoadAll<GameObject>("study room prefab");

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
    }
   /*
    public void GetMeasurement(){
        var value = int.Parse(inputField1.text);
        measurement
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}