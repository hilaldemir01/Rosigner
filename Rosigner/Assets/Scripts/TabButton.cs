using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler{
    
    public TabAreaGroup tabAreaGroup;
    public Image background;
    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    // Start is called before the first frame update
     void Start()
    {
        background = GetComponent<Image>();
        tabAreaGroup.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData){
        //To detect if a click occurs
        tabAreaGroup.OnTabSelected(this);
    }
    public void OnPointerEnter(PointerEventData eventData){
        //To do this when the cursor enters the rect area of this selectable UI object
         tabAreaGroup.OnTabEnter(this);
    }
    public void OnPointerExit(PointerEventData eventData){
        //Do this when the cursor exits the rect area of this selectable UI object.
        tabAreaGroup.OnTabExit(this);
    }
  
    public void Select(){
         //this function is called from TabAreaGroup
        if(onTabSelected != null){
            onTabSelected.Invoke();
        }
    }

    public void Deselect(){
         //this function is called from TabAreaGroup
        if(onTabDeselected != null){
            onTabDeselected.Invoke();
        }
    }
}