using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Inventory : MonoBehaviour
{
     [SerializeField] public GameObject armchair;
    public Sprite armchair_img;
    public Image selectedFurnitureImage;
    public List<Slots> slots; 
    public Slots selectedSlot;
    public Transform furnitureBar;
    public Image furnitureImage; 
 
    //public string [] slotName = {"armchair_01", "armchair_02"};
    
    // Start is called before the first frame update
    void Start()
    {
        int i = 0 ;
        armchair.name = "armchair_01";
        armchair_img.name =  armchair.name;  
       //Instantiate(armchair, new Vector3(0,0,0),Quaternion.identity); kanıtladık
       /*
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Assets/Prefabs/living room prefab");
        while(i <= prefabs.Length){
            Debug.Log(" "+prefabs[i]);
            i++;
        }*/
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
   

    // Update is called once per frame
    void Update()
    {
        
    }
}