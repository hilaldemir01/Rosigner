using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TabAreaGroup : MonoBehaviour
{
    public List<TabButton> tabAreaButtons; //List of tabbuttons
    public Sprite tabIdle; //when a tab is idle 
    public Sprite tabHover; //when a tab is hovered
    public Sprite tabActive; //when a tab is selected
    public TabButton selectedTab;
    public Text title;

    void Start() {
       
        title=GameObject.Find("Canvas/CategoryLayout/FurnitureArea/RoomTitle").GetComponent<Text>();
        title.gameObject.SetActive(true);
        title.text = "Living Room Furniture";
    } 
    public List<GameObject> objectsToSwap;
    //Subscribe takes tab button
    public void Subscribe(TabButton button){
        if(tabAreaButtons == null){
            tabAreaButtons = new List<TabButton>();
        }
        tabAreaButtons.Add(button);
    }

    public void OnTabEnter(TabButton button){
    //use hover sprite
        ResetTabs();
        if(selectedTab == null || button!= selectedTab){
            button.background.sprite = tabHover;
        }
        
    }

    public void OnTabExit(TabButton button){
        ResetTabs();
    }

    public void OnTabSelected(TabButton button){
    //use selected sprite
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
    //to set all of tabs to idle
       foreach(TabButton button in tabAreaButtons){
           if(selectedTab != null && button == selectedTab){continue;}
           button.background.sprite = tabIdle;
       }
   }
}