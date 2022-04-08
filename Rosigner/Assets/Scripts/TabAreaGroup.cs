using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TabAreaGroup : MonoBehaviour
{
    public List<TabButton> tabAreaButtons;
    public Sprite tabIdle;
    public Sprite tabHover; //bu
    public Sprite tabActive;
    public TabButton selectedTab;
    public Text title;

    void Start() {
       
        title=GameObject.Find("Canvas/CategoryLayout/FurnitureArea/RoomTitle").GetComponent<Text>();
        title.gameObject.SetActive(true);
        title.text = "Living Room Furniture";
    } 
    public List<GameObject> objectsToSwap;
    public void Subscribe(TabButton button){
        if(tabAreaButtons == null){
            tabAreaButtons = new List<TabButton>();
        }
        tabAreaButtons.Add(button);
    }

    public void OnTabEnter(TabButton button){
        ResetTabs();
        if(selectedTab == null || button!= selectedTab){
            button.background.sprite = tabHover;
        }
        
    }

    public void OnTabExit(TabButton button){
        ResetTabs();
    }

    public void OnTabSelected(TabButton button){
        if(selectedTab != null){
            selectedTab.Deselect();
        }

        selectedTab = button;
        
        selectedTab.Select();
        ResetTabs();
        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for(int i=0; i<objectsToSwap.Count; i++){
            if(i == index){
                objectsToSwap[i].SetActive(true);
                title.text = ""+ objectsToSwap[i];
            }
            else{
                objectsToSwap[i].SetActive(false);
            }
        }
        
    }
   public void ResetTabs(){
       foreach(TabButton button in tabAreaButtons){
           if(selectedTab != null && button == selectedTab){continue;}
           button.background.sprite = tabIdle;
       }
   }
}