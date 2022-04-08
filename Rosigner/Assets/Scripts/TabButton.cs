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
    

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData){
        tabAreaGroup.OnTabSelected(this);
    }
    public void OnPointerEnter(PointerEventData eventData){
         tabAreaGroup.OnTabEnter(this);
    }
    public void OnPointerExit(PointerEventData eventData){
        tabAreaGroup.OnTabExit(this);
    }
  
    public void Select(){
        if(onTabSelected != null){
            onTabSelected.Invoke();
        }
    }

    public void Deselect(){
        if(onTabDeselected != null){
            onTabDeselected.Invoke();
        }
    }
}