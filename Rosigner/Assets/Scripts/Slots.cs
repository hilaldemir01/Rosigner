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

    // Start is called before the first frame update
    void Start()
    {
        inventory.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData){
        inventory.OnSlotSelected(this);
    }
   
 
  
    public void Select(){
        if(onSlotSelected != null){
            onSlotSelected.Invoke();
        }
    }

    public void Deselect(){
        if(onSlotDeselected != null){
            onSlotDeselected.Invoke();
        }
    }
}
