using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Slots : MonoBehaviour, IPointerClickHandler
{
    public Inventory inventory, selectedFurniture;
    public Sprite furnitureActive;
    public UnityEvent onSlotSelected, onSlotDeselected;

    
    void Start(){
        // Start is called before the first frame update
        inventory.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData){
        //when mouse click in the slot this method calleds
        inventory.OnSlotSelected(this);
    }
   
    public void Select(){
        //this function is called from inventory
        if(onSlotSelected != null){
            onSlotSelected.Invoke();
        }
    }

    public void Deselect(){
         //this function is called from inventory
        if(onSlotDeselected != null){
            onSlotDeselected.Invoke();
        }
    }
}
