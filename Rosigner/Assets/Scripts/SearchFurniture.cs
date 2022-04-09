using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SearchFurniture : MonoBehaviour
{
    public string theName;
    public Text furnitureCategory;
    //public List<Text> furnitureCategory;
    
    // Start is called before the first frame update
    void Start()
    {
          furnitureCategory=GameObject.Find("Canvas/CategoryLayout/FurnitureArea/Living Room Furniture/Inventory/Category").GetComponent<Text>();
          furnitureCategory.gameObject.SetActive(true);
    }
    public void SearchBar(string searchFurniture)
    {
       theName = searchFurniture;
        Debug.Log(""+theName);
       
        if(theName==furnitureCategory.text){
             Debug.Log("fu"+furnitureCategory.text); // Bir çok category için check edilmeli ve categoryi sonuna kadar doğru yazınca buluyor bulunca oraya scroll olmalı // 
            
        } 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
